using System;

namespace PDVStore.Models
{
    public class ItemVenda : IHasId
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public int PrecoUnitario { get; internal set; }
        public object VendaId { get; internal set; }
    }
}
