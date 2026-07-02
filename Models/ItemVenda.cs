using System;
using System.Collections.Generic;
using System.Text;

namespace PDVStore.Models
{
    public class ItemVenda
    {
        public int Id { get; set; }
        public int IdVenda { get; set; }
        public Venda Venda { get; set; }
        public int IdProduto { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}
