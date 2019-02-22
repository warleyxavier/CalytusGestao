namespace CalytusGestao.View
{
    partial class MenuPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuPrincipal));
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.plantaçõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cadastroToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.gerenciamentoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ferramentasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adubaçãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calagemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.catalogoEspéciesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cadastroToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.consultaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.catalogoDoençasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cadastroToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.doençaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usuáriosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuUsuarioCadastro = new System.Windows.Forms.ToolStripMenuItem();
            this.menuUsuarioConsulta = new System.Windows.Forms.ToolStripMenuItem();
            this.menuUsuarioAlteracaoSenha = new System.Windows.Forms.ToolStripMenuItem();
            this.segurançaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restauraçãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.anotaçõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cadastroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.consultaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sobreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.logoCalytus = new System.Windows.Forms.PictureBox();
            this.lbVersao = new System.Windows.Forms.Label();
            this.lbSaudacao = new System.Windows.Forms.Label();
            this.salvar = new System.Windows.Forms.SaveFileDialog();
            this.abrir = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoCalytus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = global::CalytusGestao.Properties.Resources.eucalipto;
            this.pictureBox2.Location = new System.Drawing.Point(784, -37);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(547, 666);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.White;
            this.menu.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plantaçõesToolStripMenuItem,
            this.ferramentasToolStripMenuItem,
            this.usuáriosToolStripMenuItem,
            this.segurançaToolStripMenuItem,
            this.anotaçõesToolStripMenuItem,
            this.sobreToolStripMenuItem,
            this.sairToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(1058, 24);
            this.menu.TabIndex = 2;
            this.menu.Text = "menuStrip1";
            // 
            // plantaçõesToolStripMenuItem
            // 
            this.plantaçõesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cadastroToolStripMenuItem3,
            this.gerenciamentoToolStripMenuItem});
            this.plantaçõesToolStripMenuItem.Name = "plantaçõesToolStripMenuItem";
            this.plantaçõesToolStripMenuItem.Size = new System.Drawing.Size(100, 20);
            this.plantaçõesToolStripMenuItem.Text = "Plantações";
            // 
            // cadastroToolStripMenuItem3
            // 
            this.cadastroToolStripMenuItem3.Image = global::CalytusGestao.Properties.Resources.cadastro;
            this.cadastroToolStripMenuItem3.Name = "cadastroToolStripMenuItem3";
            this.cadastroToolStripMenuItem3.Size = new System.Drawing.Size(187, 22);
            this.cadastroToolStripMenuItem3.Text = "Cadastro";
            this.cadastroToolStripMenuItem3.Click += new System.EventHandler(this.cadastroToolStripMenuItem3_Click);
            // 
            // gerenciamentoToolStripMenuItem
            // 
            this.gerenciamentoToolStripMenuItem.Image = global::CalytusGestao.Properties.Resources.consulta;
            this.gerenciamentoToolStripMenuItem.Name = "gerenciamentoToolStripMenuItem";
            this.gerenciamentoToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.gerenciamentoToolStripMenuItem.Text = "Gerenciamento";
            this.gerenciamentoToolStripMenuItem.Click += new System.EventHandler(this.gerenciamentoToolStripMenuItem_Click);
            // 
            // ferramentasToolStripMenuItem
            // 
            this.ferramentasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adubaçãoToolStripMenuItem,
            this.calagemToolStripMenuItem,
            this.catalogoEspéciesToolStripMenuItem,
            this.catalogoDoençasToolStripMenuItem});
            this.ferramentasToolStripMenuItem.Name = "ferramentasToolStripMenuItem";
            this.ferramentasToolStripMenuItem.Size = new System.Drawing.Size(114, 20);
            this.ferramentasToolStripMenuItem.Text = "Ferramentas";
            // 
            // adubaçãoToolStripMenuItem
            // 
            this.adubaçãoToolStripMenuItem.Image = global::CalytusGestao.Properties.Resources.adubo;
            this.adubaçãoToolStripMenuItem.Name = "adubaçãoToolStripMenuItem";
            this.adubaçãoToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.adubaçãoToolStripMenuItem.Text = "Adubação";
            this.adubaçãoToolStripMenuItem.Click += new System.EventHandler(this.adubaçãoToolStripMenuItem_Click);
            // 
            // calagemToolStripMenuItem
            // 
            this.calagemToolStripMenuItem.Name = "calagemToolStripMenuItem";
            this.calagemToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.calagemToolStripMenuItem.Text = "Calagem";
            this.calagemToolStripMenuItem.Click += new System.EventHandler(this.calagemToolStripMenuItem_Click);
            // 
            // catalogoEspéciesToolStripMenuItem
            // 
            this.catalogoEspéciesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cadastroToolStripMenuItem2,
            this.consultaToolStripMenuItem1});
            this.catalogoEspéciesToolStripMenuItem.Image = global::CalytusGestao.Properties.Resources.eucalipto;
            this.catalogoEspéciesToolStripMenuItem.Name = "catalogoEspéciesToolStripMenuItem";
            this.catalogoEspéciesToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.catalogoEspéciesToolStripMenuItem.Text = "Catalogo Espécies";
            // 
            // cadastroToolStripMenuItem2
            // 
            this.cadastroToolStripMenuItem2.Image = global::CalytusGestao.Properties.Resources.cadastro;
            this.cadastroToolStripMenuItem2.Name = "cadastroToolStripMenuItem2";
            this.cadastroToolStripMenuItem2.Size = new System.Drawing.Size(142, 22);
            this.cadastroToolStripMenuItem2.Text = "Cadastro";
            this.cadastroToolStripMenuItem2.Click += new System.EventHandler(this.cadastroToolStripMenuItem2_Click);
            // 
            // consultaToolStripMenuItem1
            // 
            this.consultaToolStripMenuItem1.Image = global::CalytusGestao.Properties.Resources.consulta;
            this.consultaToolStripMenuItem1.Name = "consultaToolStripMenuItem1";
            this.consultaToolStripMenuItem1.Size = new System.Drawing.Size(142, 22);
            this.consultaToolStripMenuItem1.Text = "Consulta";
            this.consultaToolStripMenuItem1.Click += new System.EventHandler(this.consultaToolStripMenuItem1_Click);
            // 
            // catalogoDoençasToolStripMenuItem
            // 
            this.catalogoDoençasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cadastroToolStripMenuItem1,
            this.doençaToolStripMenuItem});
            this.catalogoDoençasToolStripMenuItem.Image = global::CalytusGestao.Properties.Resources.doença;
            this.catalogoDoençasToolStripMenuItem.Name = "catalogoDoençasToolStripMenuItem";
            this.catalogoDoençasToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.catalogoDoençasToolStripMenuItem.Text = "Catalogo Doenças";
            // 
            // cadastroToolStripMenuItem1
            // 
            this.cadastroToolStripMenuItem1.Image = global::CalytusGestao.Properties.Resources.cadastro;
            this.cadastroToolStripMenuItem1.Name = "cadastroToolStripMenuItem1";
            this.cadastroToolStripMenuItem1.Size = new System.Drawing.Size(142, 22);
            this.cadastroToolStripMenuItem1.Text = "Cadastro";
            this.cadastroToolStripMenuItem1.Click += new System.EventHandler(this.cadastroToolStripMenuItem1_Click);
            // 
            // doençaToolStripMenuItem
            // 
            this.doençaToolStripMenuItem.Image = global::CalytusGestao.Properties.Resources.consulta;
            this.doençaToolStripMenuItem.Name = "doençaToolStripMenuItem";
            this.doençaToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.doençaToolStripMenuItem.Text = "Consulta";
            this.doençaToolStripMenuItem.Click += new System.EventHandler(this.doençaToolStripMenuItem_Click);
            // 
            // usuáriosToolStripMenuItem
            // 
            this.usuáriosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuUsuarioCadastro,
            this.menuUsuarioConsulta,
            this.menuUsuarioAlteracaoSenha});
            this.usuáriosToolStripMenuItem.Name = "usuáriosToolStripMenuItem";
            this.usuáriosToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.usuáriosToolStripMenuItem.Text = "Usuários";
            // 
            // menuUsuarioCadastro
            // 
            this.menuUsuarioCadastro.Image = global::CalytusGestao.Properties.Resources.cadastro;
            this.menuUsuarioCadastro.Name = "menuUsuarioCadastro";
            this.menuUsuarioCadastro.Size = new System.Drawing.Size(217, 22);
            this.menuUsuarioCadastro.Text = "Cadastro";
            this.menuUsuarioCadastro.Click += new System.EventHandler(this.cadastroToolStripMenuItem_Click);
            // 
            // menuUsuarioConsulta
            // 
            this.menuUsuarioConsulta.Image = global::CalytusGestao.Properties.Resources.consulta;
            this.menuUsuarioConsulta.Name = "menuUsuarioConsulta";
            this.menuUsuarioConsulta.Size = new System.Drawing.Size(217, 22);
            this.menuUsuarioConsulta.Text = "Consulta";
            this.menuUsuarioConsulta.Click += new System.EventHandler(this.menuUsuarioConsulta_Click);
            // 
            // menuUsuarioAlteracaoSenha
            // 
            this.menuUsuarioAlteracaoSenha.Image = global::CalytusGestao.Properties.Resources.editar;
            this.menuUsuarioAlteracaoSenha.Name = "menuUsuarioAlteracaoSenha";
            this.menuUsuarioAlteracaoSenha.Size = new System.Drawing.Size(217, 22);
            this.menuUsuarioAlteracaoSenha.Text = "Alteração de Senha";
            this.menuUsuarioAlteracaoSenha.Click += new System.EventHandler(this.menuUsuarioAlteracaoSenha_Click);
            // 
            // segurançaToolStripMenuItem
            // 
            this.segurançaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backupToolStripMenuItem,
            this.restauraçãoToolStripMenuItem});
            this.segurançaToolStripMenuItem.Name = "segurançaToolStripMenuItem";
            this.segurançaToolStripMenuItem.Size = new System.Drawing.Size(97, 20);
            this.segurançaToolStripMenuItem.Text = "Segurança";
            // 
            // backupToolStripMenuItem
            // 
            this.backupToolStripMenuItem.Image = global::CalytusGestao.Properties.Resources.backup;
            this.backupToolStripMenuItem.Name = "backupToolStripMenuItem";
            this.backupToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.backupToolStripMenuItem.Text = "Backup";
            this.backupToolStripMenuItem.Click += new System.EventHandler(this.backupToolStripMenuItem_Click);
            // 
            // restauraçãoToolStripMenuItem
            // 
            this.restauraçãoToolStripMenuItem.Image = global::CalytusGestao.Properties.Resources.restaurar;
            this.restauraçãoToolStripMenuItem.Name = "restauraçãoToolStripMenuItem";
            this.restauraçãoToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.restauraçãoToolStripMenuItem.Text = "Restauração";
            this.restauraçãoToolStripMenuItem.Click += new System.EventHandler(this.restauraçãoToolStripMenuItem_Click);
            // 
            // anotaçõesToolStripMenuItem
            // 
            this.anotaçõesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cadastroToolStripMenuItem,
            this.consultaToolStripMenuItem});
            this.anotaçõesToolStripMenuItem.Name = "anotaçõesToolStripMenuItem";
            this.anotaçõesToolStripMenuItem.Size = new System.Drawing.Size(97, 20);
            this.anotaçõesToolStripMenuItem.Text = "Anotações";
            // 
            // cadastroToolStripMenuItem
            // 
            this.cadastroToolStripMenuItem.Image = global::CalytusGestao.Properties.Resources.cadastro;
            this.cadastroToolStripMenuItem.Name = "cadastroToolStripMenuItem";
            this.cadastroToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.cadastroToolStripMenuItem.Text = "Cadastro";
            this.cadastroToolStripMenuItem.Click += new System.EventHandler(this.cadastroToolStripMenuItem_Click_1);
            // 
            // consultaToolStripMenuItem
            // 
            this.consultaToolStripMenuItem.Image = global::CalytusGestao.Properties.Resources.consulta;
            this.consultaToolStripMenuItem.Name = "consultaToolStripMenuItem";
            this.consultaToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.consultaToolStripMenuItem.Text = "Consulta";
            this.consultaToolStripMenuItem.Click += new System.EventHandler(this.consultaToolStripMenuItem_Click);
            // 
            // sobreToolStripMenuItem
            // 
            this.sobreToolStripMenuItem.Name = "sobreToolStripMenuItem";
            this.sobreToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.sobreToolStripMenuItem.Text = "Sobre";
            this.sobreToolStripMenuItem.Click += new System.EventHandler(this.sobreToolStripMenuItem_Click);
            // 
            // sairToolStripMenuItem
            // 
            this.sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            this.sairToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.sairToolStripMenuItem.Text = "Sair";
            this.sairToolStripMenuItem.Click += new System.EventHandler(this.sairToolStripMenuItem_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.Color.White;
            this.linkLabel1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.LinkColor = System.Drawing.Color.DarkRed;
            this.linkLabel1.Location = new System.Drawing.Point(992, 3);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(58, 16);
            this.linkLabel1.TabIndex = 3;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Logout";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // logoCalytus
            // 
            this.logoCalytus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.logoCalytus.BackColor = System.Drawing.Color.Transparent;
            this.logoCalytus.Image = global::CalytusGestao.Properties.Resources.logoCalytus3;
            this.logoCalytus.Location = new System.Drawing.Point(347, 70);
            this.logoCalytus.Name = "logoCalytus";
            this.logoCalytus.Size = new System.Drawing.Size(361, 494);
            this.logoCalytus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoCalytus.TabIndex = 4;
            this.logoCalytus.TabStop = false;
            // 
            // lbVersao
            // 
            this.lbVersao.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbVersao.AutoSize = true;
            this.lbVersao.BackColor = System.Drawing.Color.Transparent;
            this.lbVersao.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVersao.Location = new System.Drawing.Point(475, 502);
            this.lbVersao.Name = "lbVersao";
            this.lbVersao.Size = new System.Drawing.Size(114, 18);
            this.lbVersao.TabIndex = 5;
            this.lbVersao.Text = "V.   1.0.0.34";
            // 
            // lbSaudacao
            // 
            this.lbSaudacao.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbSaudacao.AutoSize = true;
            this.lbSaudacao.BackColor = System.Drawing.Color.Transparent;
            this.lbSaudacao.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSaudacao.Location = new System.Drawing.Point(462, 54);
            this.lbSaudacao.Name = "lbSaudacao";
            this.lbSaudacao.Size = new System.Drawing.Size(139, 29);
            this.lbSaudacao.TabIndex = 6;
            this.lbSaudacao.Text = "saudacao";
            this.lbSaudacao.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // abrir
            // 
            this.abrir.Filter = "Arq. Zipado |*zip";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::CalytusGestao.Properties.Resources.eucalipto;
            this.pictureBox1.Location = new System.Drawing.Point(-268, -39);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(547, 666);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // MenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::CalytusGestao.Properties.Resources.fundoTela;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1058, 576);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbSaudacao);
            this.Controls.Add(this.lbVersao);
            this.Controls.Add(this.logoCalytus);
            this.Controls.Add(this.pictureBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu;
            this.Name = "MenuPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calytus Gestão - Menu Principal";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MenuPrincipal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoCalytus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem plantaçõesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ferramentasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usuáriosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem anotaçõesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sobreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sairToolStripMenuItem;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.PictureBox logoCalytus;
        private System.Windows.Forms.Label lbVersao;
        private System.Windows.Forms.Label lbSaudacao;
        private System.Windows.Forms.ToolStripMenuItem menuUsuarioCadastro;
        private System.Windows.Forms.ToolStripMenuItem menuUsuarioConsulta;
        private System.Windows.Forms.ToolStripMenuItem menuUsuarioAlteracaoSenha;
        private System.Windows.Forms.ToolStripMenuItem cadastroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem consultaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adubaçãoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calagemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem catalogoEspéciesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem catalogoDoençasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cadastroToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem doençaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cadastroToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem consultaToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cadastroToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem gerenciamentoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem segurançaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restauraçãoToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog salvar;
        private System.Windows.Forms.OpenFileDialog abrir;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}