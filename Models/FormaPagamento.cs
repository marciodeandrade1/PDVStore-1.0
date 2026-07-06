using System;

namespace PDVStore.Models
{
    public class FormaPagamento : IHasId
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
