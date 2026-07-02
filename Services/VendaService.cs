using PDVStore.Data;
using PDVStore.Integrations;
using PDVStore.Models;

namespace PDVStore.Services
{
    public class VendaService
    {
        private readonly PDVContext _context;
        private readonly PagamentoIntegrator _pagIntegrator;

        public VendaService(PDVContext context, PagamentoIntegrator pagIntegrator)
        {
            _context = context;
            _pagIntegrator = pagIntegrator;
        }

        public Produto BuscarProduto(string codigo)
        {
            return _context.Produtos.FirstOrDefault(p => p.CodigoBarras == codigo);
        }

        public bool FinalizarVenda(Venda venda, FormaPagamento forma)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                venda.Total = venda.CalcularTotal();
                bool pagamentoOk = _pagIntegrator.ProcessarPagamento(venda.Total, forma);

                if (!pagamentoOk) return false;

                foreach (var item in venda.Itens)
                {
                    item.Produto.AtualizarEstoque(-item.Quantidade);
                    _context.Update(item.Produto);
                }

                _context.Vendas.Add(venda);
                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                transaction.Rollback();
                return false;
            }
        }
    }
}
