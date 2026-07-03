using System;
using System.Linq;
using System.Windows.Forms;
using PDVStore.Data;
using PDVStore.Models;

namespace PDVStore.Forms
{
    public class frmGerenciarProdutos : Form
    {
        private readonly PDVContext _context;
        private DataGridView dgv;
        private Button btnRefresh;

        public frmGerenciarProdutos(PDVContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            InitializeComponent();
            LoadProducts();
        }

        private void InitializeComponent()
        {
            this.Text = "Gerenciar Produtos";
            this.Width = 800;
            this.Height = 450;

            dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AutoGenerateColumns = true,
                AllowUserToAddRows = false
            };

            btnRefresh = new Button
            {
                Text = "Refresh",
                Dock = DockStyle.Top,
                Height = 30
            };
            btnRefresh.Click += BtnRefresh_Click;

            this.Controls.Add(dgv);
            this.Controls.Add(btnRefresh);
        }

        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            LoadProducts();
        }

        private void LoadProducts()
        {
            try
            {
                var list = _context.Produtos.ToList();
                dgv.DataSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load products: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
