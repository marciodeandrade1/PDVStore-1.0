using PDV_LojaCompleto.Services;
using PDVStore.Models;
using System.Collections.ObjectModel;

namespace PDVStore.ViewModels
{
    public class DashboardViewModel
    {
        private readonly RelatorioService _service;
        public ObservableCollection<ItemRelatorio> ItensMaisVendidos { get; } = new ObservableCollection<ItemRelatorio>();
        public ObservableCollection<Produto> AlertasEstoque { get; } = new ObservableCollection<Produto>();

        public DashboardViewModel(RelatorioService service)
        {
            _service = service;
        }

        public void CarregarDados()
        {
            ItensMaisVendidos.Clear();
            var itens = _service.GerarRelatorioItensMaisVendidos(DateTime.Now.AddMonths(-1), DateTime.Now);
            foreach (var item in itens) ItensMaisVendidos.Add(item);

            AlertasEstoque.Clear();
            var alertas = _service.GerarRelatorioEstoqueMinimo();
            foreach (var p in alertas) AlertasEstoque.Add(p);
        }
    }
}
