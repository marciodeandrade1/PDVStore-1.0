using System;
using System.Collections.Generic;
using System.Text;

namespace PDVStore.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CodigoBarras { get; set; }
        public decimal Preco { get; set; }
        public int QuantidadeEstoque { get; set; }
        public string Fornecedor { get; set; }
        public int EstoqueMinimo { get; set; } = 10; // Padrão para alertas

        public void AtualizarEstoque(int delta)
        {
            QuantidadeEstoque += delta;
            if (QuantidadeEstoque < 0) throw new Exception("Estoque insuficiente");
        }
    }
}
