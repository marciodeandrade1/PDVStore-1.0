namespace PDVStore.Forms
{
    partial class frmCadastroUsuario
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
            txtNome = new TextBox();
            label1 = new Label();
            txtSenha = new TextBox();
            lblConfirma = new Label();
            picFoto = new PictureBox();
            lblNome = new Label();
            txtConfirmaSenha = new TextBox();
            btnSalvar = new Button();
            btnEscolherFoto = new Button();
            btnCancelar = new Button();
            openFileDialog1 = new OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)picFoto).BeginInit();
            SuspendLayout();
            // 
            // txtNome
            // 
            txtNome.Location = new Point(288, 33);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(298, 27);
            txtNome.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(200, 70);
            label1.Name = "label1";
            label1.Size = new Size(52, 20);
            label1.TabIndex = 2;
            label1.Text = "Senha:";
            // 
            // txtSenha
            // 
            txtSenha.Location = new Point(288, 70);
            txtSenha.Name = "txtSenha";
            txtSenha.Size = new Size(182, 27);
            txtSenha.TabIndex = 3;
            // 
            // lblConfirma
            // 
            lblConfirma.AutoSize = true;
            lblConfirma.Location = new Point(204, 116);
            lblConfirma.Name = "lblConfirma";
            lblConfirma.Size = new Size(78, 20);
            lblConfirma.TabIndex = 5;
            lblConfirma.Text = "Confirmar:";
            // 
            // picFoto
            // 
            picFoto.BorderStyle = BorderStyle.Fixed3D;
            picFoto.Location = new Point(22, 29);
            picFoto.Name = "picFoto";
            picFoto.Size = new Size(147, 131);
            picFoto.SizeMode = PictureBoxSizeMode.StretchImage;
            picFoto.TabIndex = 6;
            picFoto.TabStop = false;
            // 
            // lblNome
            // 
            lblNome.AutoSize = true;
            lblNome.Location = new Point(200, 36);
            lblNome.Name = "lblNome";
            lblNome.Size = new Size(53, 20);
            lblNome.TabIndex = 7;
            lblNome.Text = "Nome:";
            // 
            // txtConfirmaSenha
            // 
            txtConfirmaSenha.Location = new Point(288, 113);
            txtConfirmaSenha.Name = "txtConfirmaSenha";
            txtConfirmaSenha.Size = new Size(182, 27);
            txtConfirmaSenha.TabIndex = 8;
            // 
            // btnSalvar
            // 
            btnSalvar.Location = new Point(288, 164);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(147, 29);
            btnSalvar.TabIndex = 9;
            btnSalvar.Text = "Cadastrar usuário";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // btnEscolherFoto
            // 
            btnEscolherFoto.Location = new Point(22, 180);
            btnEscolherFoto.Name = "btnEscolherFoto";
            btnEscolherFoto.Size = new Size(147, 29);
            btnEscolherFoto.TabIndex = 10;
            btnEscolherFoto.Text = "Escolher foto";
            btnEscolherFoto.UseVisualStyleBackColor = true;
            btnEscolherFoto.Click += btnEscolherFoto_Click_1;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(288, 199);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(147, 29);
            btnCancelar.TabIndex = 11;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // frmCadastroUsuario
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(610, 276);
            Controls.Add(btnCancelar);
            Controls.Add(btnEscolherFoto);
            Controls.Add(btnSalvar);
            Controls.Add(txtConfirmaSenha);
            Controls.Add(lblNome);
            Controls.Add(picFoto);
            Controls.Add(lblConfirma);
            Controls.Add(txtSenha);
            Controls.Add(label1);
            Controls.Add(txtNome);
            Name = "frmCadastroUsuario";
            Text = "frmCadastroUsuario";
            ((System.ComponentModel.ISupportInitialize)picFoto).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtNome;
        private TextBox txtSenha;
        private Label lblConfirma;
        private PictureBox picFoto;
        private Label lblNome;
        private TextBox txtConfirmaSenha;
        private Button btnSalvar;
        private Button btnEscolherFoto;
        private Button btnCancelar;
        private OpenFileDialog openFileDialog1;
    }
}