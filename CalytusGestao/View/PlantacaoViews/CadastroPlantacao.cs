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

namespace CalytusGestao.View.PlantacaoViews
{
    public partial class CadastroPlantacao : Form
    {
        public CadastroPlantacao()
        {
            InitializeComponent();
        }

        private void CadastroPlantacao_Load(object sender, EventArgs e)
        {
            tbAno.Text = DateTime.Now.Year.ToString();
            cbStatus.Text = "Ativa";
            lbImagem.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            limparCampos();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            execSalvar();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            escolheImagem();
        }

        private void tbAno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)) && !e.KeyChar.Equals((char)8))
            {
                e.Handled = true;
            }
        }

        private void tbQtdPlantas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!(Char.IsDigit(e.KeyChar)) && !e.KeyChar.Equals((char)8))
            {
                e.Handled = true;
            }
        }

        private void tbTamArea_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)44 && tbTamArea.Text.Equals(""))
            {
                e.Handled = true;
            }
            else if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != (char)44 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            else if (tbTamArea.Text.Contains(",") && !Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void tbIdentificacao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                execSalvar();
            }
        }

        private void limparCampos()
        {
            tbIdentificacao.Text = "";
            tbAno.Text = "";
            tbQtdPlantas.Text = "";
            tbTamArea.Text = "";
            tbLocalizacao.Text = "";
            tbMunicipio.Text = "";
            cbStatus.Text = "Ativa";
            lbImagem.Text = "";
        }

        private bool checaCamposVazios()
        {
            if(tbIdentificacao.Text.Trim().Equals("") ||tbAno.Text.Trim().Equals("") || tbQtdPlantas.Text.Equals("") || tbTamArea.Text.Trim().Equals(""))
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
            if(checaCamposVazios())
            {
                string enderecoPasta = @"C:\Users\Public\Documents\Excellence\CalytusGestao\Imagens\Plantacoes\";

                PlantacaoContr plantacao = new PlantacaoContr();
                plantacao.cadastroPlantacao(tbIdentificacao.Text.Trim(), Convert.ToInt32(tbAno.Text), Convert.ToInt32(tbQtdPlantas.Text), Convert.ToDouble(tbTamArea.Text), tbLocalizacao.Text.Trim(), tbMunicipio.Text.Trim(), cbStatus.Text, (!lbImagem.Text.Equals("") ? enderecoPasta + Midia.nomeArquivoEndereco(abrir.FileName) : ""));

                if(plantacao.getResultado())
                {
                    if(!lbImagem.Text.Equals(""))
                    {
                        Midia.copiaArquivo(abrir.FileName, enderecoPasta + Midia.nomeArquivoEndereco(abrir.FileName));
                    }

                    MessageBox.Show("Plantação " + tbIdentificacao.Text.Trim() + " cadastrada com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limparCampos();
                }
                else
                {
                    MessageBox.Show("Erro ao cadastrar a plantação " + tbIdentificacao.Text.Trim() + ".\nErro: " + plantacao.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Alguns campos são obrigatórios.\nVerifique se estes foram preenchidos corretamente.\nOs obrigatórios estão maracados com *.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void escolheImagem()
        {
            if(abrir.ShowDialog().Equals(DialogResult.OK))
            {
                lbImagem.Text = Midia.nomeArquivoEndereco(abrir.FileName);
            }
        }
    }
}
