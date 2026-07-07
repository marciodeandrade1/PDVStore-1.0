using iText.StyledXmlParser.Jsoup.Nodes;
using Microsoft.Extensions.DependencyInjection;
using PDVStore.Data;
using PDVStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PDVStore.Forms
{
    public partial class frmLogin : Form
    {
        private readonly IServiceProvider _serviceProvider;

        public frmLogin(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Create a scope to resolve the DbContext (registered as scoped by AddDbContext)
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<PDVContext>();

            var user = context.UsuarioCaixa.FirstOrDefault(u => u.Nome == txtUsuario.Text);
            if (user != null && user.Autenticar(txtSenha.Text))
            {
                // Store authenticated user in session so other forms can access the Id
                Session.CurrentUser = user;

                var frmMenu = _serviceProvider.GetService<frmMenuPrincipal>();
                frmMenu.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Credenciais inválidas!");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmCadastroUsuario frmCadastro = _serviceProvider.GetService<frmCadastroUsuario>();
            frmCadastro.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmGerenciarUsuarios frmGerenciar = _serviceProvider.GetService<frmGerenciarUsuarios>();
            frmGerenciar.Show();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmGerenciarProdutos frmGerenciarProdutos = _serviceProvider.GetService<frmGerenciarProdutos>();
            frmGerenciarProdutos.Show();
        }
    }
}