using iText.IO.Font.Constants;
using PDVLoja.Services;
using PDVStore.Models;
using System;
using PDVStore.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDVStore.Forms
{
    public partial class frmPDV : Form
    {
        // propriedades da classe FrmPDV
        private decimal _valorRecebido = 0;
        private decimal _troco = 0;
        private PDVViewModel _viewModel = new PDVViewModel();
        private readonly int _caixaId;
        private readonly VendaService _vendaService;

        public frmPDV(VendaService vendaService)
        {
            InitializeComponent();
            _vendaService = vendaService ?? throw new ArgumentNullException(nameof(vendaService));
            _caixaId = 1; // default caixa id; adjust as needed
        }
        private void ConfigurarTroco()
        {
            // GroupBox Troco
            grpTroco = new GroupBox
            {
                Text = "Pagamento em Dinheiro",
                Location = new Point(850, 430),
                Size = new Size(350, 180)
            };

            lblValorRecebido = new Label { Text = "Valor Recebido:", Location = new Point(20, 30), Font = new Font("Segoe UI", 11) };
            txtValorRecebido = new TextBox { Location = new Point(20, 55), Size = new Size(200, 30), Font = new Font("Segoe UI", 14) };
            txtValorRecebido.TextChanged += TxtValorRecebido_TextChanged;

            lblTroco = new Label { Text = "Troco:", Location = new Point(20, 100), Font = new Font("Segoe UI", 11) };
            lblValorTroco = new Label { Text = "R$ 0,00", Location = new Point(20, 125), Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.DarkRed };

            grpTroco.Controls.AddRange(new Control[] { lblValorRecebido, txtValorRecebido, lblTroco, lblValorTroco });
            this.Controls.Add(grpTroco);
        }

        // Evento de cálculo
        private void TxtValorRecebido_TextChanged(object? sender, EventArgs e)
        {
            if (decimal.TryParse(txtValorRecebido.Text, out decimal valorRecebido))
            {
                _valorRecebido = valorRecebido;
                _troco = valorRecebido - _viewModel.Total;

                if (_troco >= 0)
                    lblValorTroco.Text = _troco.ToString("C2");
                else
                    lblValorTroco.Text = "Valor insuficiente";
            }
            else
            {
                lblValorTroco.Text = "R$ 0,00";
            }
        }

        private async void btnFinalizar_Click(object sender, EventArgs e)
        {
            if (_viewModel.Itens.Count == 0)
            {
                MessageBox.Show("Adicione itens à venda.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validação de troco para pagamento em dinheiro
            if (cmbFormaPagamento.Text == "Dinheiro" && _troco < 0)
            {
                MessageBox.Show("Valor recebido é insuficiente para completar a venda.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var usuario = Session.CurrentUser;
            if (usuario == null)
            {
                MessageBox.Show("Usuário não autenticado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var venda = new Venda
            {
                UsuarioCaixaId = usuario.Id,
                CaixaId = _caixaId,
                Itens = new List<ItemVenda>(_viewModel.Itens),
                Desconto = _viewModel.Desconto,
                FormaPagamento = cmbFormaPagamento.Text,
                ValorTotal = _viewModel.Total
            };

            btnFinalizar.Enabled = false;
            var previousCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                var vendaRegistrada = await _vendaService.RegistrarVendaAsync(venda);

                if (vendaRegistrada == null)
                    throw new InvalidOperationException("Resposta inválida do serviço de vendas.");

                string mensagem = $"Venda #{vendaRegistrada.Id} registrada com sucesso!\n\n";
                if (cmbFormaPagamento.Text == "Dinheiro" && _troco > 0)
                    mensagem += $"Troco: {_troco:C2}";

                MessageBox.Show(mensagem, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _viewModel.Limpar();
                txtValorRecebido.Clear();
                AtualizarTotais();
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Operação cancelada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (InvalidOperationException invEx)
            {
                MessageBox.Show($"Erro: {invEx.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnFinalizar.Enabled = true;
                Cursor.Current = previousCursor;
            }
        }

        private void AtualizarTotais()
        {
            throw new NotImplementedException();
        }
    }
}
