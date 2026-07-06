using Microsoft.EntityFrameworkCore;
using PDVStore.Data;
using PDVStore.Integrations;
using PDVStore.Models;
using PDVStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PDVLoja.Services
{
    public class VendaService
    {
        private readonly PDVContext _context;
        private readonly EstoqueService _estoqueService;
        private readonly PagamentoIntegrator _pagamentoIntegrator;

        public VendaService(PDVContext context, EstoqueService estoqueService, PagamentoIntegrator pagamentoIntegrator)
        {
            _context = context;
            _estoqueService = estoqueService;
            _pagamentoIntegrator = pagamentoIntegrator;
        }

        /// <summary>
        /// Registra uma venda completa com controle de estoque e pagamento
        /// </summary>
        public async Task<Venda> RegistrarVendaAsync(Venda venda)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                if (venda.Itens == null || !venda.Itens.Any())
                    throw new InvalidOperationException("A venda deve conter pelo menos um item.");

                venda.DataVenda = DateTime.UtcNow;
                venda.ValorTotal = venda.Itens.Sum(i => i.Subtotal) - venda.Desconto;

                // 1. Validar e dar baixa no estoque
                foreach (var item in venda.Itens)
                {
                    bool baixado = await _estoqueService.BaixarEstoqueAsync(item.ProdutoId, item.Quantidade);
                    if (!baixado)
                        throw new InvalidOperationException($"Estoque insuficiente para o produto ID {item.ProdutoId}");
                }

                // 2. Processar pagamento
                bool pagamentoOk = await _pagamentoIntegrator.ProcessarPagamentoAsync(venda);
                if (!pagamentoOk)
                    throw new InvalidOperationException("Falha ao processar pagamento.");

                // 3. Salvar venda
                _context.Vendas.Add(venda);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return venda;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Venda?> ObterPorIdAsync(int id)
        {
            return await _context.Vendas
                .Include(v => v.Itens)
                .ThenInclude(i => i.Produto)
                .Include(v => v.UsuarioCaixa)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<IEnumerable<Venda>> ListarPorPeriodoAsync(DateTime inicio, DateTime fim, int? caixaId = null)
        {
            var query = _context.Vendas
                .Include(v => v.Itens)
                .Include(v => v.UsuarioCaixa)
                .Where(v => v.DataVenda >= inicio && v.DataVenda <= fim && v.Status == "Concluida");

            if (caixaId.HasValue)
                query = query.Where(v => v.CaixaId == caixaId.Value);

            return await query.OrderByDescending(v => v.DataVenda).ToListAsync();
        }

        public async Task<decimal> CalcularTotalVendasAsync(DateTime inicio, DateTime fim, int? caixaId = null)
        {
            var query = _context.Vendas
                .Where(v => v.DataVenda >= inicio && v.DataVenda <= fim && v.Status == "Concluida");

            if (caixaId.HasValue)
                query = query.Where(v => v.CaixaId == caixaId.Value);

            return await query.SumAsync(v => v.ValorTotal);
        }

        public async Task<bool> CancelarVendaAsync(int vendaId, string motivo)
        {
            var venda = await _context.Vendas
                .Include(v => v.Itens)
                .FirstOrDefaultAsync(v => v.Id == vendaId);

            if (venda == null || venda.Status == "Cancelada")
                return false;

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Devolver ao estoque
                foreach (var item in venda.Itens)
                {
                    await _estoqueService.AdicionarEstoqueAsync(item.ProdutoId, item.Quantidade);
                }

                venda.Status = "Cancelada";
                // Opcional: registrar motivo em uma tabela de log

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        // Relatórios rápidos
        public async Task<IEnumerable<object>> RelatorioVendasPorFormaPagamentoAsync(DateTime inicio, DateTime fim)
        {
            return await _context.Vendas
                .Where(v => v.DataVenda >= inicio && v.DataVenda <= fim && v.Status == "Concluida")
                .GroupBy(v => v.FormaPagamento)
                .Select(g => new
                {
                    FormaPagamento = g.Key,
                    QuantidadeVendas = g.Count(),
                    ValorTotal = g.Sum(v => v.ValorTotal)
                })
                .ToListAsync();
        }
    }
}