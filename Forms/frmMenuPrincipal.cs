using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PDVStore.Data;

namespace PDVStore.Forms
{
    public partial class frmMenuPrincipal : Form
    {
        private readonly PDVContext _context;

        public frmMenuPrincipal(PDVContext context)
        {
            InitializeComponent();
            _context = context;
        }

        private void btnProdutos_Click(object sender, EventArgs e)
        {
            // open products management
            var frm = new frmGerenciarProdutos(_context);
            frm.ShowDialog();
        }
    }
}
