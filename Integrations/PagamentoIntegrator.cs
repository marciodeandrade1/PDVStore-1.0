using PDVStore.Models;

namespace PDVStore.Integrations
{
    public class PagamentoIntegrator
    {
        // Mock para integração bancária; substitua por SDK real (ex: PagSeguro)
        public bool ProcessarPagamento(decimal valor, FormaPagamento forma)
        {
            Console.WriteLine($"Processando {forma} de {valor}...");
            // Simular API bancária
            IntegrarContaBancaria(valor);
            return true; // Sempre aprova em mock
        }

        internal async Task<bool> ProcessarPagamentoAsync(Venda venda)
        {
            throw new NotImplementedException();
        }

        private void IntegrarContaBancaria(decimal valor)
        {
            // Mock depósito em conta bancária
            Console.WriteLine($"Depósito de {valor} na conta bancária.");
        }
    }
}
