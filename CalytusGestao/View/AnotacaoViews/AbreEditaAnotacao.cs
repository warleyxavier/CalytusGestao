using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CalytusGestao.Model.Classes;
using CalytusGestao.Controller.Controller;

namespace CalytusGestao.View.AnotacaoViews
{
    public partial class AbreEditaAnotacao : Form
    {
        private int codAnotacao;

        public bool status = false;

        //****************************************************
        //************** VARIAVEIS ***************************
        //****************************************************

        private string assunto;
        private string anotacao;

        public AbreEditaAnotacao(int codigo, string tipo)
        {
            InitializeComponent();

            this.codAnotacao = codigo;

            carregaCampos();

            checaTipo(tipo);            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            descarregaVariaveis();
            visibilidade(false);
            lbTitulo.Text = "Informações da Anotação";
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            carregaVariaveis();
            visibilidade(true);
            lbTitulo.Text = "Edição da Anotação";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            execUpdate();
        }

        private void carregaCampos()
        {
            Anotacao anotacao = new Anotacao();
            AnotacaoContr anotacao2 = new AnotacaoContr();

            anotacao = anotacao2.consultaAnotacao(this.codAnotacao);

            if(!anotacao.Equals(null))
            {
                lbData.Text = anotacao.gsData;
                tbAssunto.Text = anotacao.gsAssunto;
                tbAnotacao.Text = anotacao.gsAnotacao;
            }
        }

        private void carregaVariaveis()
        {
            assunto = tbAssunto.Text;
            anotacao = tbAnotacao.Text;
        }

        private void descarregaVariaveis()
        {
            tbAssunto.Text = assunto;
            tbAnotacao.Text = anotacao;
        }

        private void checaTipo(string tipo)
        {
            if(tipo.Equals("Abre"))
            {
                lbTitulo.Text = "Informações da Anotação";
                visibilidade(false);
            }
            else
            {
                lbTitulo.Text = "Edição da Anotação";
                carregaVariaveis();
                visibilidade(true);
            }
        }

        private void visibilidade(bool statusVis)
        {
            tbAssunto.Enabled = statusVis;
            tbAnotacao.Enabled = statusVis;

            btnSalvar.Visible = statusVis;
            btnCancelar.Visible = statusVis;

            btnEditar.Visible = !statusVis;
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

        private void execUpdate()
        {
            if(checaCamposVazios())
            {
                AnotacaoContr anotacao = new AnotacaoContr();
                anotacao.atualizaAnotacao(codAnotacao, tbAssunto.Text.Trim(), tbAnotacao.Text.Trim());

                if(anotacao.getResultado())
                {
                    MessageBox.Show("Anotação " + tbAssunto.Text.Trim() + " atualizada com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    visibilidade(false);
                    lbTitulo.Text = "Informações da Anotação";
                    status = true;
                }
                else
                {
                    MessageBox.Show("Erro ao atualizar a anotação " + this.assunto + "\nErro: " + anotacao.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    visibilidade(false);
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
                execUpdate();
            }
        }
    }
}
