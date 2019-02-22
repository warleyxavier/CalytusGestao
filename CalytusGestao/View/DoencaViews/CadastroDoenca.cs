using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CalytusGestao.Controller.Controller;
using CalytusGestao.Model.Helpers;

namespace CalytusGestao.View.DoencaViews
{
    public partial class CadastroDoenca : Form
    {
        // *****************************************************************
        // ***************** DECLARAÇÃO DOS ATRIBUTOS **********************
        // *****************************************************************

        private int[] codLocais = null;
        private int codDoenca = 0;

        public CadastroDoenca()
        {
            InitializeComponent();

            limparCampos();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            execSalvar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            limparCampos();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            escolheImagem();
        }

        private void limparCampos()
        {
            tbNome.Text = "";
            tbCaracteristicas.Text = "";
            tbDiagnostico.Text = "";
            lbImagem.Text = "";
            lbLocais.Text = "";
        }        

        private void escolheImagem()
        {
            if (abrir.ShowDialog().Equals(DialogResult.OK))
            {
                lbImagem.Text = Midia.nomeArquivoEndereco(abrir.FileName);
            }
        }

        private bool checaCamposVazios()
        {
            if(tbNome.Text.Trim().Equals(""))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void execSalvar()
        {
            if (checaCamposVazios())
            {
                DoencaContr doenca = new DoencaContr();
                codDoenca = doenca.cadastroDoenca(tbNome.Text.Trim(), tbCaracteristicas.Text.Trim(), tbDiagnostico.Text.Trim(), lbImagem.Text);

                if (doenca.getResultado())
                {
                    if (!lbImagem.Text.Equals(""))
                    {
                        Midia.copiaArquivo(abrir.FileName, @"C:\Users\Public\Documents\Excellence\CalytusGestao\Imagens\Doenças\" + lbImagem.Text);
                    }

                    LocalContr local = new LocalContr();
                    local.cadatraLocaisDoenca(codDoenca, codLocais);

                    if (local.getResultado())
                    {
                        MessageBox.Show("Doença " + tbNome.Text.Trim() + " cadastrada com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Doença " + tbNome.Text.Trim() + " cadastrada com sucesso.\nContudo houve erro ao salvar os locais de predominância da doença.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    limparCampos();
                }
                else
                {
                    MessageBox.Show("Erro ao cadastrar a doença " + tbNome.Text.Trim() + ".\nErro: " + doenca.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }               
            }
            else
            {
                MessageBox.Show("Para realizar o cadastro é necessário que o campo de nome esteja preenchido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            escolheLocais();
        }

        private void escolheLocais()
        {
            EscolhaLocais locais = new EscolhaLocais();
            locais.ShowDialog();

            if(locais.getStatus())
            {
                codLocais = locais.getcodLocais();
                lbLocais.Text = locais.getNomeLocais();
            }

            locais.Dispose();
        }
    }
}
