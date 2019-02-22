using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CalytusGestao.Model.Helpers;
using CalytusGestao.Controller.Controller;
using CalytusGestao.Model.Helpers;

namespace CalytusGestao.View.EspeciesViews
{
    public partial class CadastroEspecie : Form
    {
        // *****************************************************************
        // ***************** DECLARAÇÃO DOS ATRIBUTOS **********************
        // *****************************************************************

        private int[] codUtilidades = null;

        public CadastroEspecie()
        {
            InitializeComponent();

            limparCampos();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            limparCampos();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            escolheUtilidades();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            escolheImagem();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            execCadastro();
        }

        private void limparCampos()
        {
            tbNomePopular.Text = "";
            tbNomeCientifico.Text = "";
            tbCaracteristicas.Text = "";
            lbUtilidades.Text = "";
            lbImagem.Text = "";
        }

        private bool verificaCamposVazios()
        {
            if(tbNomePopular.Text.Trim().Equals(""))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void escolheImagem()
        {
            if(abrir.ShowDialog().Equals(DialogResult.OK))
            {
                lbImagem.Text = Midia.nomeArquivoEndereco(abrir.FileName);
            }
        }

        private void escolheUtilidades()
        {
            EscolhaUtilidades utilidades = new EscolhaUtilidades();
            utilidades.ShowDialog();

            if(utilidades.getStatus())
            {
                lbUtilidades.Text = utilidades.getUtilidadesTexto();
                codUtilidades = utilidades.getCodsUtilidades();
            }

            utilidades.Dispose();
        }

        private void execCadastro()
        {
            if(verificaCamposVazios())
            {
                EspecieContr especie = new EspecieContr();
                int codEspecie = especie.cadastraEspecie(tbNomePopular.Text.Trim(), tbNomeCientifico.Text.Trim(), tbCaracteristicas.Text.Trim(), lbImagem.Text);

                if (especie.getResultado())
                {
                    if (!lbImagem.Text.Equals(""))
                    {
                        Midia.copiaArquivo(abrir.FileName, @"C:\Users\Public\Documents\Excellence\CalytusGestao\Imagens\Especies\" + lbImagem.Text);
                    }

                    bool status = true;

                    if (!lbUtilidades.Text.Equals(""))
                    {
                        UtilidadeContr utilidade = new UtilidadeContr();
                        utilidade.cadastraUtilidadeEspecie(codEspecie, codUtilidades);

                        if(utilidade.getResultado())
                        {
                            status = true;
                        }
                        else
                        {
                            status = false;
                        }                        
                    }

                    if (status)
                    {
                        limparCampos();
                        MessageBox.Show("Espécie cadastrada " + tbNomePopular.Text.Trim() + " com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Espécie cadastrada " + tbNomePopular.Text.Trim() + " com sucesso.\nContudo houve erro ao salvar as utilidades.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Erro ao cadastrar a espécie " + tbNomePopular.Text.Trim() + "\nErro: " + especie.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Campo de nome popular obrigatório.\nVerifique se foi digitado corretamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }        
    }
}
