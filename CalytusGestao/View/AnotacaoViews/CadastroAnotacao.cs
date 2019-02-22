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

namespace CalytusGestao.View.AnotacaoViews
{
    public partial class CadastroAnotacao : Form
    {
        public CadastroAnotacao()
        {
            InitializeComponent();
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


        private void limparCampos()
        {
            tbAssunto.Text = "";
            tbAnotacao.Text = "";
        }

        private bool checaCamposVazios()
        {
            if(tbAssunto.Text.Trim().Equals("") || tbAnotacao.Text.Trim().Equals(""))
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
                AnotacaoContr anotacao = new AnotacaoContr();
                anotacao.cadastraAnotacao(tbAssunto.Text.Trim(), tbAnotacao.Text.Trim());

                if (anotacao.getResultado())
                {
                    MessageBox.Show("Anotacao " + tbAssunto.Text.Trim() + " cadastrada com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limparCampos();

                    tbAssunto.Focus();
                }
                else
                {
                    MessageBox.Show("Erro ao cadastrar a anotação " + tbAssunto.Text + "\nErro: " + anotacao.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Verifique se todos os campos foram preenchidos corretamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tbAssunto_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode.Equals(Keys.Enter))
            {
                execSalvar();
            }
        }
    }
}
