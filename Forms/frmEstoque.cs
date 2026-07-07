using PDVStore.Models;
using PDVStore.Services;
using System;
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
    public partial class frmEstoque : Form
    {
        private readonly EstoqueService _estoqueService;
        private Produto? _produtoSelecionado;
        public frmEstoque()
        {
            InitializeComponent();
            ConfigurarFormulario();
            CarregarProdutos();
        }

        private void CarregarProdutos()
        {
            throw new NotImplementedException();
        }

        private void ConfigurarFormulario()
        {
            this.Text = "Gestão de Estoque - Entrada / Saída";
            this.WindowState = FormWindowState.Maximized;

            cmbTipoMovimento.Items.AddRange(new string[] { "Entrada", "Saída" });
            cmbTipoMovimento.SelectedIndex = 0;

            dgvProdutos.AutoGenerateColumns = false;
            dgvProdutos.SelectionChanged += dgvProdutos_SelectionChanged;

            // Configurar colunas do Grid
            dgvProdutos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", HeaderText = "ID", DataPropertyName = "Id", Width = 70 });
            dgvProdutos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Codigo", HeaderText = "Código", DataPropertyName = "CodigoBarras", Width = 130 });
            dgvProdutos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nome", HeaderText = "Produto", DataPropertyName = "Nome", Width = 350 });
            dgvProdutos.Columns.Add(new DataGridViewTextBoxColumn { Name = "EstoqueAtual", HeaderText = "Estoque Atual", DataPropertyName = "EstoqueAtual", Width = 120 });
            dgvProdutos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Preco", HeaderText = "Preço", DataPropertyName = "Preco", DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" } });
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Implementar filtro
            string filtro = txtBuscar.Text.Trim().ToUpper();
            // Filtrar grid (lógica futura)
            MessageBox.Show("Filtro aplicado: " + filtro);
        }

        private void cmbTipoMovimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnConfirmarMovimento.BackColor = cmbTipoMovimento.Text == "Entrada"
                ? System.Drawing.Color.DarkGreen
                : System.Drawing.Color.DarkRed;
        }

        private void dgvProdutos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProdutos.CurrentRow?.DataBoundItem is Produto produto)
            {
                _produtoSelecionado = produto;
                lblProdutoSelecionado.Text = $"Produto Selecionado: {produto.Nome} (Estoque: {produto.EstoqueAtual})";
            }
        }

        private async void btnConfirmarMovimento_ClickAsync(object sender, EventArgs e)
        {
            if (_produtoSelecionado == null)
            {
                MessageBox.Show("Selecione um produto na lista.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtQuantidade.Text, out int quantidade) || quantidade <= 0)
            {
                MessageBox.Show("Informe uma quantidade válida.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string tipo = cmbTipoMovimento.Text;
            string motivo = txtMotivo.Text.Trim();

            try
            {
                bool sucesso;

                if (tipo == "Entrada")
                {
                    sucesso = await _estoqueService.AdicionarEstoqueAsync(_produtoSelecionado.Id, quantidade);
                    MessageBox.Show($"{quantidade} unidades adicionadas ao estoque com sucesso!", "Entrada Confirmada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else // Saída
                {
                    sucesso = await _estoqueService.BaixarEstoqueAsync(_produtoSelecionado.Id, quantidade);
                    MessageBox.Show($"{quantidade} unidades removidas do estoque com sucesso!", "Saída Confirmada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (sucesso)
                {
                    CarregarProdutos();
                    txtQuantidade.Clear();
                    txtMotivo.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao realizar movimento: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
