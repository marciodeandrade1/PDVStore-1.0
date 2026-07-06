using System;
using System.Collections.Generic;
using System.Linq;

namespace PDVStore.Models
{
    public class Venda : IHasId
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ItemVenda> Items { get; set; } = new List<ItemVenda>();

        public decimal CalcularTotal()
        {
            return Items.Sum(i => i.Quantidade * i.PrecoUnitario);
        }
    }
}
