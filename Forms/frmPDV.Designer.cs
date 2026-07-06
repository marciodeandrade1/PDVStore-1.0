namespace PDVStore.Forms
{
    partial class frmPDV
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            grpTroco = new GroupBox();
            lblValorTroco = new Label();
            lblTroco = new Label();
            txtValorRecebido = new TextBox();
            lblValorRecebido = new Label();
            btnFinalizar = new Button();
            cmbFormaPagamento = new ComboBox();
            lblUsuarioLogado = new Label();
            grpTroco.SuspendLayout();
            SuspendLayout();
            // 
            // grpTroco
            // 
            grpTroco.Controls.Add(lblValorTroco);
            grpTroco.Controls.Add(lblTroco);
            grpTroco.Controls.Add(txtValorRecebido);
            grpTroco.Controls.Add(lblValorRecebido);
            grpTroco.Location = new Point(736, 207);
            grpTroco.Name = "grpTroco";
            grpTroco.Size = new Size(398, 120);
            grpTroco.TabIndex = 0;
            grpTroco.TabStop = false;
            grpTroco.Text = "Troco";
            // 
            // lblValorTroco
            // 
            lblValorTroco.AutoSize = true;
            lblValorTroco.Location = new Point(134, 82);
            lblValorTroco.Name = "lblValorTroco";
            lblValorTroco.Size = new Size(36, 20);
            lblValorTroco.TabIndex = 3;
            lblValorTroco.Text = "0,00";
            // 
            // lblTroco
            // 
            lblTroco.AutoSize = true;
            lblTroco.Location = new Point(19, 82);
            lblTroco.Name = "lblTroco";
            lblTroco.Size = new Size(49, 20);
            lblTroco.TabIndex = 2;
            lblTroco.Text = "Troco:";
            // 
            // txtValorRecebido
            // 
            txtValorRecebido.Location = new Point(134, 40);
            txtValorRecebido.Name = "txtValorRecebido";
            txtValorRecebido.Size = new Size(242, 27);
            txtValorRecebido.TabIndex = 1;
            // 
            // lblValorRecebido
            // 
            lblValorRecebido.AutoSize = true;
            lblValorRecebido.Location = new Point(19, 40);
            lblValorRecebido.Name = "lblValorRecebido";
            lblValorRecebido.Size = new Size(109, 20);
            lblValorRecebido.TabIndex = 0;
            lblValorRecebido.Text = "Valor recebido:";
            // 
            // btnFinalizar
            // 
            btnFinalizar.Location = new Point(736, 342);
            btnFinalizar.Name = "btnFinalizar";
            btnFinalizar.Size = new Size(154, 29);
            btnFinalizar.TabIndex = 1;
            btnFinalizar.Text = "Finalizar venda";
            btnFinalizar.UseVisualStyleBackColor = true;
            btnFinalizar.Click += btnFinalizar_Click;
            // 
            // cmbFormaPagamento
            // 
            cmbFormaPagamento.FormattingEnabled = true;
            cmbFormaPagamento.Location = new Point(34, 25);
            cmbFormaPagamento.Name = "cmbFormaPagamento";
            cmbFormaPagamento.Size = new Size(151, 28);
            cmbFormaPagamento.TabIndex = 2;
            // 
            // lblUsuarioLogado
            // 
            lblUsuarioLogado.AutoSize = true;
            lblUsuarioLogado.Location = new Point(1020, 25);
            lblUsuarioLogado.Name = "lblUsuarioLogado";
            lblUsuarioLogado.Size = new Size(59, 20);
            lblUsuarioLogado.TabIndex = 3;
            lblUsuarioLogado.Text = "Usuário";
            // 
            // frmPDV
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1401, 630);
            Controls.Add(lblUsuarioLogado);
            Controls.Add(cmbFormaPagamento);
            Controls.Add(btnFinalizar);
            Controls.Add(grpTroco);
            Name = "frmPDV";
            Text = "frmPDV";
            grpTroco.ResumeLayout(false);
            grpTroco.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox grpTroco;
        private Label lblValorRecebido;
        private Label lblValorTroco;
        private Label lblTroco;
        private TextBox txtValorRecebido;
        private Button btnFinalizar;
        private ComboBox cmbFormaPagamento;
        private Label lblUsuarioLogado;
    }
}