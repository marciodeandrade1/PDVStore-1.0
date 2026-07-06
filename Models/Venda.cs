using System;
using System.Collections.Generic;
using System.Linq;

namespace PDVStore.Models
{
    public class Venda : IHasId
    {
        public int Id { get; set; }
        public int UsuarioCaixaId { get; set; }
        public DateTime DataVenda { get; set; } = DateTime.UtcNow;
        public decimal ValorTotal { get; set; }
        public decimal Desconto { get; set; } = 0;
        public string FormaPagamento { get; set; } = "Dinheiro"; // PIX, CartaoCredito, CartaoDebito, Dinheiro
        public string? PixTxId { get; set; } // Para integração PIX
        public string? Status { get; set; } = "Concluida"; // Concluida, Cancelada, Pendente
        public int CaixaId { get; set; } = 1; // Suporte multi-caixa

        public UsuarioCaixa? UsuarioCaixa { get; set; }
        public ICollection<ItemVenda> Itens { get; set; } = new List<ItemVenda>();
    }
}
