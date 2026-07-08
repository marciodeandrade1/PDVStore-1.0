using PDVStore.Data;
using PDVStore.Models;
using Serilog;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PDVStore.Forms
{
    public partial class frmCadastroUsuario : Form
    {
        private readonly PDVContext _context;
        private readonly ErrorProvider _errorProvider = new();
        private string? _caminhoFotoSelecionada;
        private UsuarioCaixa? _usuarioEmEdicao;

        // Construtor para Novo Usuário
        public frmCadastroUsuario(PDVContext context)
        {
            InitializeComponent();
            _context = context;
            this.Text = "Cadastrar Novo Usuário";
            CarregarImagemPadrao();
            chkAdministrador.Checked = false; // Padrão: Operador
        }

        // Construtor para Edição
        public frmCadastroUsuario(PDVContext context, UsuarioCaixa usuario)
        {
            InitializeComponent();
            _context = context;
            _usuarioEmEdicao = usuario;
            this.Text = $"Editar Usuário - {usuario.Nome}";
            PreencherDadosParaEdicao();
        }

        private void CarregarImagemPadrao()
        {
            try
            {
                var appDir = AppDomain.CurrentDomain.BaseDirectory;
                var imagePath = Path.Combine(appDir, "Resources", "Images", "user_default.png");

                if (File.Exists(imagePath))
                    picFoto.Image = Image.FromFile(imagePath);
                else
                    picFoto.Image = Properties.Resources.user_default;
            }
            catch
            {
                picFoto.Image = null;
            }
        }

        private void PreencherDadosParaEdicao()
        {
            if (_usuarioEmEdicao == null) return;

            txtNome.Text = _usuarioEmEdicao.Nome;
            txtNome.Enabled = false; // Não permitir alterar nome

            chkAdministrador.Checked = _usuarioEmEdicao.Permissao == TipoPermissao.Administrador;

            if (!string.IsNullOrEmpty(_usuarioEmEdicao.FotoPath) && File.Exists(_usuarioEmEdicao.FotoPath))
            {
                picFoto.Image = Image.FromFile(_usuarioEmEdicao.FotoPath);
            }
        }

        private void btnEscolherFoto_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Filter = "Imagens|*.jpg;*.jpeg;*.png;*.bmp",
                Title = "Selecionar Foto do Usuário"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _caminhoFotoSelecionada = ofd.FileName;
                picFoto.Image = Image.FromFile(_caminhoFotoSelecionada);
            }
        }

        // Designer may reference btnEscolherFoto_Click_1; provide a small adapter.
        private void btnEscolherFoto_Click_1(object sender, EventArgs e) => btnEscolherFoto_Click(sender, e);

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            _errorProvider.Clear();

            try
            {
                if (string.IsNullOrWhiteSpace(txtNome.Text))
                {
                    _errorProvider.SetError(txtNome, "Nome do usuário é obrigatório!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtSenha.Text) || txtSenha.Text.Length < 6)
                {
                    _errorProvider.SetError(txtSenha, "Senha deve ter no mínimo 6 caracteres!");
                    return;
                }

                if (txtSenha.Text != txtConfirmaSenha.Text)
                {
                    _errorProvider.SetError(txtConfirmaSenha, "As senhas não coincidem!");
                    return;
                }

                UsuarioCaixa usuario;

                if (_usuarioEmEdicao == null) // NOVO USUÁRIO
                {
                    if (_context.Usuarios.Any(u => u.Nome == txtNome.Text.Trim()))
                    {
                        _errorProvider.SetError(txtNome, "Este nome de usuário já está em uso!");
                        return;
                    }

                    usuario = new UsuarioCaixa
                    {
                        Nome = txtNome.Text.Trim(),
                        Permissao = chkAdministrador.Checked ? TipoPermissao.Administrador : TipoPermissao.Operador
                    };

                    _context.Usuarios.Add(usuario);
                }
                else // EDIÇÃO
                {
                    usuario = _usuarioEmEdicao;
                    // Atualiza permissão apenas se o usuário logado for Admin
                    if (PDVStore.Models.Session.CurrentUser?.EhAdmin() == true)
                    {
                        usuario.Permissao = chkAdministrador.Checked ? TipoPermissao.Administrador : TipoPermissao.Operador;
                    }
                }

                usuario.SetSenha(txtSenha.Text);
                usuario.SetFotoPath(SalvarFotoUsuario());

                await _context.SaveChangesAsync();

                Log.Information(_usuarioEmEdicao == null ?
                    "Novo usuário cadastrado: {Nome} | Permissão: {Permissao}" :
                    "Usuário atualizado: {Nome}", usuario.Nome, usuario.Permissao);

                MessageBox.Show("Usuário salvo com sucesso!", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erro ao salvar usuário");
                MessageBox.Show($"Erro ao salvar usuário: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string? SalvarFotoUsuario()
        {
            if (string.IsNullOrEmpty(_caminhoFotoSelecionada))
                return _usuarioEmEdicao?.GetFotoPath(); // Mantém foto anterior

            try
            {
                string pastaFotos = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Users");
                Directory.CreateDirectory(pastaFotos);

                string nomeArquivo = $"user_{DateTime.Now:yyyyMMdd_HHmmss}{Path.GetExtension(_caminhoFotoSelecionada)}";
                string caminhoDestino = Path.Combine(pastaFotos, nomeArquivo);

                File.Copy(_caminhoFotoSelecionada, caminhoDestino, true);
                return caminhoDestino;
            }
            catch (Exception ex)
            {
                Log.Warning(ex, "Falha ao salvar foto do usuário");
                return _usuarioEmEdicao?.GetFotoPath();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}