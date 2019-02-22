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
using CalytusGestao.Model.Classes;

namespace CalytusGestao.View.AnotacaoViews
{
    public partial class ConsultaAnotacao : Form
    {
        public ConsultaAnotacao()
        {
            InitializeComponent();
        }

        private void ConsultaAnotacao_Load(object sender, EventArgs e)
        {
            carregaGrid("");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(checaGridVazio())
            {
                abreAnotacao("Abre");
            }
            else
            {
                MessageBox.Show("É necessário que haja alguma anotação selecionada para realizar essa ação.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            carregaGrid(tbPesquisa.Text.Trim());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            carregaGrid("");
        }

        private void carregaGrid(string pesquisa)
        {
            try
            {
                dgAnotacao.Rows.Clear();

                AnotacaoContr anotacao = new AnotacaoContr();
                Anotacao[] anotacoes = anotacao.consultaAnotacoes(pesquisa);

                if (anotacao.getResultado())
                {                    
                    if (anotacao.getDados())
                    {
                        for (int i = 0; i < anotacoes.Length; i++)
                        {
                            dgAnotacao.Rows.Add(anotacoes[i].gsCodigo, anotacoes[i].gsAssunto, anotacoes[i].gsData);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Erro ao carregas os dados do banco de dados.\n Erro: " + anotacao.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("Erro: " + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tbPesquisa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                carregaGrid(tbPesquisa.Text.Trim());
            }
        }

        private void tbPesquisa_TextChanged(object sender, EventArgs e)
        {
            if(tbPesquisa.Text.Trim().Length % 3 == 0)
            {
                carregaGrid(tbPesquisa.Text.Trim());
            }
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checaGridVazio())
            {
                abreAnotacao("Editar");
            }
            else
            {
                MessageBox.Show("É necessário que haja alguma anotação selecionada para realizar essa ação.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            execRemove();
        }

        private void execRemove()
        {
            if(checaGridVazio())
            {
                DialogResult resposta = MessageBox.Show("Realmente deseja remover a anotação " + dgAnotacao.SelectedCells[1].Value + "?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resposta.Equals(DialogResult.Yes))
                {
                    AnotacaoContr anotacao = new AnotacaoContr();
                    anotacao.removeAnotacao(Convert.ToInt32(dgAnotacao.SelectedCells[0].Value));

                    if (anotacao.getResultado())
                    {
                        MessageBox.Show("Anotação " + dgAnotacao.SelectedCells[1].Value + " removida com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        carregaGrid("");
                    }
                    else
                    {
                        MessageBox.Show("Erro ao remover a anotação " + dgAnotacao.SelectedCells[1].Value + ".\nErro: " + anotacao.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Para realizar a remoção é necessário que haja alguma anotação selecionada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool checaGridVazio()
        {
            if(dgAnotacao.RowCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void abreAnotacao(string tipo)
        {
            AbreEditaAnotacao informacoes = new AbreEditaAnotacao(Convert.ToInt32(dgAnotacao.SelectedCells[0].Value), tipo);
            informacoes.ShowDialog();
            
            if(informacoes.status)
            {
                carregaGrid("");
            }

            informacoes.Dispose();
        }

        private void dgAnotacao_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (checaGridVazio())
            {
                abreAnotacao("Abre");
            }
            else
            {
                MessageBox.Show("É necessário que haja alguma anotação selecionada para realizar essa ação.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgAnotacao_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode.Equals(Keys.Enter))
            {
                if (checaGridVazio())
                {
                    abreAnotacao("Abre");
                }
                else
                {
                    MessageBox.Show("É necessário que haja alguma anotação selecionada para realizar essa ação.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checaGridVazio())
            {
                abreAnotacao("Abre");
            }
            else
            {
                MessageBox.Show("É necessário que haja alguma anotação selecionada para realizar essa ação.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
