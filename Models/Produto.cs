using System;

namespace PDVStore.Models
{
    public class Produto : IHasId
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; internal set; }
    }
}
