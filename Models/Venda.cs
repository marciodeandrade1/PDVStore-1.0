namespace PDVStore.Models
{
    public class Venda
    {
        public int Id { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
        public decimal Total { get; set; }
        public FormaPagamento FormaPagamento { get; set; }
        public List<ItemVenda> Itens { get; set; } = new List<ItemVenda>();

        public decimal CalcularTotal()
        {
            return Itens.Sum(i => i.Quantidade * i.PrecoUnitario);
        }
    }
}
