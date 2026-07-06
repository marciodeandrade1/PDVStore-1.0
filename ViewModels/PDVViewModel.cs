using PDVStore.Models;
using System.Collections.Generic;
using System.Linq;

namespace PDVStore.ViewModels
{
    public class PDVViewModel
    {
        public List<ItemVenda> Itens { get; } = new List<ItemVenda>();
        public decimal Desconto { get; set; }

        public decimal Total => Itens.Sum(i => i.Subtotal) - Desconto;

        public void Limpar()
        {
            Itens.Clear();
            Desconto = 0;
        }
    }
}
