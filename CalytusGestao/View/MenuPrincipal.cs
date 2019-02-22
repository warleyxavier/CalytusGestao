using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CalytusGestao.View.Usuario;
using CalytusGestao.View.AnotacaoViews;
using CalytusGestao.View.DoencaViews;
using CalytusGestao.View.EspeciesViews;
using CalytusGestao.View.PlantacaoViews;
using CalytusGestao.View.FerramentasViews;
using CalytusGestao.Model.ClassesBD;
using System.IO;

namespace CalytusGestao.View
{
    public partial class MenuPrincipal : Form
    {
        public bool status = false;

        // *****************************************************************
        // ***************** DADOS DO USUÁRIO LOGADO ***********************
        // *****************************************************************

        private int codigo;
        private string nome;
        private string tipo;

        public MenuPrincipal(int codigo, string nome, string tipo)
        {
            InitializeComponent();

            this.codigo = codigo;
            this.nome = nome;
            this.tipo = tipo;
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.status = true;
            this.Close();
        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            lbSaudacao.Text = "BEM VINDO(A) " + this.nome.ToUpper() + "!";

            logoCalytus.Location = new Point(this.Width/2 - logoCalytus.Width/2, this.Height/2 - logoCalytus.Height/2);

            lbVersao.Location = new Point(this.Width/2 - lbVersao.Width/2, logoCalytus.Height + 60);

            lbSaudacao.Location = new Point(this.Width / 2 - lbSaudacao.Width / 2, logoCalytus.Width - 250);

            // *****************************************************************
            // *********************** RESTRIÇÕES ******************************
            // *****************************************************************

            if(!this.tipo.Equals("Admin"))
            {
                menuUsuarioCadastro.Visible = false;
                menuUsuarioConsulta.Visible = false;
            }
        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sobre sobre = new Sobre();
            sobre.ShowDialog();
            sobre.Dispose();
        }

        private void cadastroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CadastroUsuario cadastro = new CadastroUsuario();
            cadastro.ShowDialog();
            cadastro.Dispose();
        }

        private void menuUsuarioConsulta_Click(object sender, EventArgs e)
        {
            ConsultaUsuario consulta = new ConsultaUsuario();
            consulta.ShowDialog();
            consulta.Dispose();
        }

        private void menuUsuarioAlteracaoSenha_Click(object sender, EventArgs e)
        {
            AlteraSenha altera = new AlteraSenha(codigo, nome);
            altera.ShowDialog();
            altera.Dispose();
        }

        private void cadastroToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CadastroAnotacao cadastro = new CadastroAnotacao();
            cadastro.ShowDialog();
            cadastro.Dispose();
        }

        private void consultaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsultaAnotacao consulta = new ConsultaAnotacao();
            consulta.ShowDialog();
            consulta.Dispose();
        }

        private void cadastroToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CadastroDoenca cadastro = new CadastroDoenca();
            cadastro.ShowDialog();
            cadastro.Dispose();
        }

        private void doençaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsultaDoenca consulta = new ConsultaDoenca();
            consulta.ShowDialog();
            consulta.Dispose();
        }

        private void cadastroToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CadastroEspecie cadastro = new CadastroEspecie();
            cadastro.ShowDialog();
            cadastro.Dispose();
        }

        private void consultaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ConsultaEspecies consulta = new ConsultaEspecies();
            consulta.ShowDialog();
            consulta.Dispose();
        }

        private void cadastroToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            CadastroPlantacao cadastro = new CadastroPlantacao();
            cadastro.ShowDialog();
            cadastro.Dispose();
        }

        private void gerenciamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsultaPlantacoes consulta = new ConsultaPlantacoes();
            consulta.ShowDialog();
            consulta.Dispose();
        }

        private void adubaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Adubacao adubacao = new Adubacao();
            adubacao.ShowDialog();
            adubacao.Dispose();
        }

        private void calagemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Calagem calagem = new Calagem();
            calagem.ShowDialog();
            calagem.Dispose();
        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(salvar.ShowDialog().Equals(DialogResult.OK))
            {
                Banco banco = new Banco();
                banco.backup(salvar.FileName);

                if(banco.getResultado())
                {
                    MessageBox.Show("Backup realizado com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Foram encontrados problemas ao realizar o backup.\nError: " + banco.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void restauraçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (abrir.ShowDialog().Equals(DialogResult.OK))
            {
                Banco banco = new Banco();
                banco.restaurar(abrir.FileName);

                if (banco.getResultado())
                {
                    MessageBox.Show("Restauração realizada com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Foram encontrados problemas ao realizar a restauração.\nError: " + banco.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void resetarDadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string origem = @"C:\Users\Public\Documents\Excellence\CalytusGestao";

            DirectoryInfo pasta = new DirectoryInfo(origem);
            pasta.Delete();
        }
    }
}