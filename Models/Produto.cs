namespace PDVStore.Models
{
    public class Produto : IHasId
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string? CodigoBarras { get; set; }
        public decimal Preco { get; set; }

        // Backing field kept for compatibility with services that reference 'Estoque'
        public int Estoque { get; set; }

        // New convenience property used by some forms
        public int EstoqueAtual
        {
            get => Estoque;
            set => Estoque = value;
        }

        public string? Categoria { get; set; }
        public string? Descricao { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
