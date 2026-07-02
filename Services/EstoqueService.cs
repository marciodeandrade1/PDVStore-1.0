using PDVStore.Data;
using PDVStore.Models;

namespace PDVStore.Services
{
    public class EstoqueService
    {
        private readonly PDVContext _context;

        public EstoqueService(PDVContext context)
        {
            _context = context;
        }

        public void AdicionarProduto(Produto produto)
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();
        }

        public List<Produto> GetProdutosBaixoEstoque()
        {
            return _context.Produtos.Where(p => p.QuantidadeEstoque < p.EstoqueMinimo).ToList();
        }

        // Outros métodos: Editar, Remover, etc.
    }
}
