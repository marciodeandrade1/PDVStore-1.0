using iText.StyledXmlParser.Jsoup.Nodes;
using PDVStore.Data;
using PDVStore.Models;
using PDVStore.Services;
using System;
using System.Linq;
using System.Windows.Forms;

namespace PDVStore.Forms
{
    public class frmGerenciarProdutos : Form
    {
        private readonly EstoqueService _estoqueService; // ou ProdutoService futuro
        private Produto? _produtoSelecionado;


        private readonly PDVContext _context;
        private DataGridView dgvProdutos;
        private Button btnSalvar;
        private Label lblCodigo;
        private Label lblNomeProduto;
        private Label lblPreco;
        private Label lblEstoque;
        private Label lblCategoria;
        private Label lblDescricao;
        private TextBox txtCodigo;
        private TextBox textBox2;
        private TextBox txtPreco;
        private TextBox txtEstoque;
        private TextBox txtCategoria;
        private TextBox txtDescricao;
        private Button btnNovo;
        private Button button3;
        private Button btnAtualizarEstoque;
        private TextBox txtNomeProduto;
        private Button btnExcluir;
        private Button btnRefresh;

        public frmGerenciarProdutos(PDVContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            InitializeComponent();
            ConfigurarGrid();
            CarregarProdutos();
        }

        private void ConfigurarGrid()
        {
            dgvProdutos.AutoGenerateColumns = false;
            dgvProdutos.SelectionChanged += dgvProdutos_SelectionChanged;

            // Colunas
            dgvProdutos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", HeaderText = "ID", DataPropertyName = "Id", Width = 60 });
            dgvProdutos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Codigo", HeaderText = "Código", DataPropertyName = "CodigoBarras", Width = 120 });
            dgvProdutos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nome", HeaderText = "Nome do Produto", DataPropertyName = "Nome", Width = 300 });
            dgvProdutos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Preco", HeaderText = "Preço", DataPropertyName = "Preco", DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" } });
            dgvProdutos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Estoque", HeaderText = "Estoque", DataPropertyName = "EstoqueAtual" });
            dgvProdutos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Categoria", HeaderText = "Categoria", DataPropertyName = "Categoria" });
        }

        private void InitializeComponent()
        {
            dgvProdutos = new DataGridView();
            btnRefresh = new Button();
            btnSalvar = new Button();
            lblCodigo = new Label();
            lblNomeProduto = new Label();
            lblPreco = new Label();
            lblEstoque = new Label();
            lblCategoria = new Label();
            lblDescricao = new Label();
            txtPreco = new TextBox();
            txtEstoque = new TextBox();
            txtCategoria = new TextBox();
            txtDescricao = new TextBox();
            btnNovo = new Button();
            btnAtualizarEstoque = new Button();
            txtCodigo = new TextBox();
            txtNomeProduto = new TextBox();
            btnExcluir = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvProdutos).BeginInit();
            SuspendLayout();
            // 
            // dgvProdutos
            // 
            dgvProdutos.ColumnHeadersHeight = 29;
            dgvProdutos.Location = new Point(488, 34);
            dgvProdutos.Name = "dgvProdutos";
            dgvProdutos.RowHeadersWidth = 51;
            dgvProdutos.Size = new Size(445, 205);
            dgvProdutos.TabIndex = 0;
            dgvProdutos.SelectionChanged += dgvProdutos_SelectionChanged;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(1026, 246);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(75, 31);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "Refresh";
            btnRefresh.Click += BtnRefresh_Click;
            // 
            // btnSalvar
            // 
            btnSalvar.Location = new Point(93, 405);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(87, 27);
            btnSalvar.TabIndex = 2;
            btnSalvar.Text = "Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // lblCodigo
            // 
            lblCodigo.AutoSize = true;
            lblCodigo.Location = new Point(12, 9);
            lblCodigo.Name = "lblCodigo";
            lblCodigo.Size = new Size(61, 20);
            lblCodigo.TabIndex = 3;
            lblCodigo.Text = "Codigo:";
            // 
            // lblNomeProduto
            // 
            lblNomeProduto.AutoSize = true;
            lblNomeProduto.Location = new Point(12, 56);
            lblNomeProduto.Name = "lblNomeProduto";
            lblNomeProduto.Size = new Size(65, 20);
            lblNomeProduto.TabIndex = 4;
            lblNomeProduto.Text = "Produto:";
            // 
            // lblPreco
            // 
            lblPreco.AutoSize = true;
            lblPreco.Location = new Point(12, 103);
            lblPreco.Name = "lblPreco";
            lblPreco.Size = new Size(49, 20);
            lblPreco.TabIndex = 5;
            lblPreco.Text = "Preço:";
            // 
            // lblEstoque
            // 
            lblEstoque.AutoSize = true;
            lblEstoque.Location = new Point(12, 150);
            lblEstoque.Name = "lblEstoque";
            lblEstoque.Size = new Size(65, 20);
            lblEstoque.TabIndex = 6;
            lblEstoque.Text = "Estoque:";
            // 
            // lblCategoria
            // 
            lblCategoria.AutoSize = true;
            lblCategoria.Location = new Point(12, 197);
            lblCategoria.Name = "lblCategoria";
            lblCategoria.Size = new Size(77, 20);
            lblCategoria.TabIndex = 7;
            lblCategoria.Text = "Categoria:";
            // 
            // lblDescricao
            // 
            lblDescricao.AutoSize = true;
            lblDescricao.Location = new Point(12, 244);
            lblDescricao.Name = "lblDescricao";
            lblDescricao.Size = new Size(77, 20);
            lblDescricao.TabIndex = 8;
            lblDescricao.Text = "Descrição:";
            // 
            // txtPreco
            // 
            txtPreco.Location = new Point(93, 103);
            txtPreco.Name = "txtPreco";
            txtPreco.Size = new Size(113, 27);
            txtPreco.TabIndex = 9;
            // 
            // txtEstoque
            // 
            txtEstoque.Location = new Point(93, 150);
            txtEstoque.Name = "txtEstoque";
            txtEstoque.Size = new Size(113, 27);
            txtEstoque.TabIndex = 9;
            // 
            // txtCategoria
            // 
            txtCategoria.Location = new Point(93, 194);
            txtCategoria.Name = "txtCategoria";
            txtCategoria.Size = new Size(293, 27);
            txtCategoria.TabIndex = 9;
            // 
            // txtDescricao
            // 
            txtDescricao.Location = new Point(93, 244);
            txtDescricao.Multiline = true;
            txtDescricao.Name = "txtDescricao";
            txtDescricao.Size = new Size(386, 127);
            txtDescricao.TabIndex = 9;
            // 
            // btnNovo
            // 
            btnNovo.Location = new Point(197, 405);
            btnNovo.Name = "btnNovo";
            btnNovo.Size = new Size(87, 27);
            btnNovo.TabIndex = 2;
            btnNovo.Text = "Novo";
            btnNovo.UseVisualStyleBackColor = true;
            btnNovo.Click += btnNovo_Click;
            // 
            // btnAtualizarEstoque
            // 
            btnAtualizarEstoque.Location = new Point(392, 405);
            btnAtualizarEstoque.Name = "btnAtualizarEstoque";
            btnAtualizarEstoque.Size = new Size(87, 27);
            btnAtualizarEstoque.TabIndex = 2;
            btnAtualizarEstoque.Text = "Atualizar Estoque";
            btnAtualizarEstoque.UseVisualStyleBackColor = true;
            btnAtualizarEstoque.Click += btnAtualizarEstoque_Click;
            // 
            // txtCodigo
            // 
            txtCodigo.Location = new Point(94, 16);
            txtCodigo.Name = "txtCodigo";
            txtCodigo.Size = new Size(292, 27);
            txtCodigo.TabIndex = 10;
            // 
            // txtNomeProduto
            // 
            txtNomeProduto.Location = new Point(93, 56);
            txtNomeProduto.Name = "txtNomeProduto";
            txtNomeProduto.Size = new Size(293, 27);
            txtNomeProduto.TabIndex = 11;
            // 
            // btnExcluir
            // 
            btnExcluir.Location = new Point(302, 405);
            btnExcluir.Name = "btnExcluir";
            btnExcluir.Size = new Size(84, 27);
            btnExcluir.TabIndex = 12;
            btnExcluir.Text = "Excluir";
            btnExcluir.UseVisualStyleBackColor = true;
            btnExcluir.Click += btnExcluir_Click;
            // 
            // frmGerenciarProdutos
            // 
            ClientSize = new Size(1113, 480);
            Controls.Add(btnExcluir);
            Controls.Add(txtNomeProduto);
            Controls.Add(txtCodigo);
            Controls.Add(txtDescricao);
            Controls.Add(txtCategoria);
            Controls.Add(txtEstoque);
            Controls.Add(txtPreco);
            Controls.Add(lblDescricao);
            Controls.Add(lblCategoria);
            Controls.Add(lblEstoque);
            Controls.Add(lblPreco);
            Controls.Add(lblNomeProduto);
            Controls.Add(lblCodigo);
            Controls.Add(btnAtualizarEstoque);
            Controls.Add(btnNovo);
            Controls.Add(btnSalvar);
            Controls.Add(dgvProdutos);
            Controls.Add(btnRefresh);
            Name = "frmGerenciarProdutos";
            Text = "Gerenciar Produtos";
            ((System.ComponentModel.ISupportInitialize)dgvProdutos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            CarregarProdutos();
        }

        private async Task CarregarProdutos()
        {
            try
            {
                // var produtos = await _estoqueService.ListarProdutosAsync();
                // dgvProdutos.DataSource = produtos.ToList();

                // Simulação temporária
                dgvProdutos.DataSource = new List<Produto>
                {
                    new Produto { Id = 1, CodigoBarras = "789123456", Nome = "Arroz 5kg", Preco = 24.90m, EstoqueAtual = 45, Categoria = "Alimentos" },
                    new Produto { Id = 2, CodigoBarras = "789654321", Nome = "Feijão 1kg", Preco = 8.50m, EstoqueAtual = 120, Categoria = "Alimentos" }
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha carregando produtos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAtualizarEstoque_Click(object sender, EventArgs e)
        {
            // Abrir formulário de ajuste de estoque
            MessageBox.Show("Funcionalidade de Ajuste de Estoque em desenvolvimento.");
        }

        private void dgvProdutos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProdutos.CurrentRow?.DataBoundItem is Produto produto)
            {
                _produtoSelecionado = produto;
                PreencherCampos(produto);
            }
        }

        private void PreencherCampos(Produto produto)
        {
            txtCodigo.Text = produto.CodigoBarras;
            txtNomeProduto.Text = produto.Nome;
            txtPreco.Text = produto.Preco.ToString("0.00");
            txtEstoque.Text = produto.EstoqueAtual.ToString();
            txtCategoria.Text = produto.Categoria;
            txtDescricao.Text = produto.Descricao;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNomeProduto.Text) || string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("Código e Nome são obrigatórios!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var produto = _produtoSelecionado ?? new Produto();

                produto.CodigoBarras = txtCodigo.Text.Trim();
                produto.Nome = txtNomeProduto.Text.Trim();
                produto.Preco = decimal.Parse(txtPreco.Text);
                produto.EstoqueAtual = int.Parse(txtEstoque.Text);
                produto.Categoria = txtCategoria.Text.Trim();
                produto.Descricao = txtDescricao.Text.Trim();
                produto.Ativo = true;

                // await _estoqueService.SalvarProdutoAsync(produto);

                MessageBox.Show("Produto salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CarregarProdutos();
                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar: {ex.Message}");
            }
        }

        private void LimparCampos()
        {
            txtCodigo.Clear();
            txtNomeProduto.Clear();
            txtPreco.Clear();
            txtEstoque.Clear();
            txtCategoria.Clear();
            txtDescricao.Clear();
            _produtoSelecionado = null;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (_produtoSelecionado == null)
            {
                MessageBox.Show("Selecione um produto para excluir.");
                return;
            }

            if (MessageBox.Show($"Deseja realmente excluir o produto '{_produtoSelecionado.Nome}'?",
                "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // await _estoqueService.ExcluirProdutoAsync(_produtoSelecionado.Id);
                MessageBox.Show("Produto excluído com sucesso!");
                CarregarProdutos();
                LimparCampos();
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimparCampos();
            _produtoSelecionado = null;
            txtCodigo.Focus();
        }
    }
}
