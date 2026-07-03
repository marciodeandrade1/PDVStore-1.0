using Microsoft.Extensions.DependencyInjection;
using PDVStore.Data;
using PDVStore.Models;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDVStore.Forms
{
    public partial class frmGerenciarUsuarios : Form
    {
        private readonly PDVContext _context;
        private readonly IServiceProvider _serviceProvider;
        private readonly EventHandler TxtBusca_TextChanged;

        public frmGerenciarUsuarios(PDVContext context, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _context = context;
            _serviceProvider = serviceProvider;

            ConfigurarGrid();
            CarregarUsuarios();
        }

        private void CarregarUsuarios()
        {
            var usuarios = _context.UsuarioCaixa
                .Where(u => u.GetAtivo())
                .Select(u => new
                {
                    u.Id,
                    u.Nome,
                    Status = u.Ativo ? "Ativo" : "Inativo",
                    Foto = !string.IsNullOrEmpty(u.FotoPath) && File.Exists(u.FotoPath)
                           ? Image.FromFile(u.FotoPath)
                           : Properties.Resources.user_default
                })
                .ToList();

            dgvUsuarios.DataSource = usuarios;
        }

        private void ConfigurarGrid()
        {
            dgvUsuarios.AutoGenerateColumns = false;

            dgvUsuarios.Columns.Add("Id", "ID");
            dgvUsuarios.Columns.Add("Nome", "Nome de Usuário");
            dgvUsuarios.Columns.Add("Permissao", "Permissão");
            dgvUsuarios.Columns.Add("Status", "Status");

            var fotoColumn = new DataGridViewImageColumn
            {
                Name = "Foto",
                HeaderText = "Foto",
                Width = 70,
                ImageLayout = DataGridViewImageCellLayout.Zoom
            };
            dgvUsuarios.Columns.Add(fotoColumn);
        }

        private void CarregarUsuarios(string filtro = "")
        {
            var query = _context.UsuarioCaixa.Where(u => u.Ativo);

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                query = query.Where(u => u.Nome.Contains(filtro));
            }

            var usuarios = query.Select(u => new
            {
                u.Id,
                u.Nome,
                Permissao = u.Permissao == TipoPermissao.Administrador ? "Administrador" : "Operador",
                Status = u.Ativo ? "Ativo" : "Inativo",
                Foto = !string.IsNullOrEmpty(u.FotoPath) && File.Exists(u.FotoPath)
                       ? Image.FromFile(u.FotoPath)
                       : Properties.Resources.user_default
            }).ToList();

            dgvUsuarios.DataSource = usuarios;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            var frmCadastro = new frmCadastroUsuario(_context);
            frmCadastro.ShowDialog();
            CarregarUsuarios();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null)
            {
                MessageBox.Show("Selecione um usuário para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idUsuario = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["Id"].Value);

            var usuario = _context.UsuarioCaixa.Find(idUsuario);

            if (usuario != null)
            {
                var frmEdicao = new frmCadastroUsuario(_context, usuario);
                frmEdicao.ShowDialog();
                CarregarUsuarios();

                // Configurar evento de filtro em tempo real
                txtBusca.TextChanged += TxtBusca_TextChanged;
            }
        }

        private async void btnExcluir_ClickAsync(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null)
            {
                MessageBox.Show("Selecione um usuário para excluir.", "Aviso");
                return;
            }

            if (MessageBox.Show("Deseja realmente desativar este usuário?\n\nEsta ação não pode ser desfeita facilmente.",
                "Confirmar Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                int idUsuario = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["Id"].Value);
                var usuario = await _context.UsuarioCaixa.FindAsync(idUsuario);

                if (usuario != null)
                {
                    usuario.SetAtivo(false); // Soft Delete
                    await _context.SaveChangesAsync();

                    Log.Information("Usuário desativado: {Nome} (ID: {Id})", usuario.Nome, usuario.Id);

                    MessageBox.Show("Usuário desativado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CarregarUsuarios();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erro ao desativar usuário");
                MessageBox.Show("Erro ao desativar usuário.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            CarregarUsuarios(txtBusca.Text.Trim());
        }

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnEditar.PerformClick();
            }
        }
    }
}
