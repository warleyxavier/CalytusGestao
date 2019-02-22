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
using CalytusGestao.Model.Helpers;

namespace CalytusGestao.View.EspeciesViews
{
    public partial class ConsultaEspecies : Form
    {
        public ConsultaEspecies()
        {
            InitializeComponent();

            carregaGrid("");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            abreEspecie("Abre");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            carregaGrid(tbPesquisa.Text.Trim());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            carregaGrid("");
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            abreEspecie("Abre");
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            abreEspecie("Edita");
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            removeEspecie();
        }

        private void tbPesquisa_TextChanged(object sender, EventArgs e)
        {
            if(tbPesquisa.Text.Trim().Length % 3 == 0)
            {
                carregaGrid(tbPesquisa.Text.Trim());
            }
        }

        private void dgEspecie_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            abreEspecie("Abre");
        }

        private void dgEspecie_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                abreEspecie("Abre");
            }
        }

        private bool verificaGridVazio()
        {
            if(dgEspecie.RowCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void carregaGrid(string pesquisa)
        {
            try
            {
                EspecieContr especie = new EspecieContr();
                Especie[] especies = especie.consultaEspecies(pesquisa);

                if(especie.getResultado())
                {
                    dgEspecie.Rows.Clear();

                    if(especie.getDados())
                    {
                        for(int i = 0; i < especies.Length; i++)
                        {
                            dgEspecie.Rows.Add(especies[i].gsCodigo, especies[i].gsNomePopular + (!especies[i].gsNomeCientifico.Equals("")? " - " + especies[i].gsNomeCientifico : "") , especies[i].gsImagem);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Erro ao carregar dados do banco de dados.\nErro: " + especie.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("Erro ao listar dados do banco de dados.\nErro: " + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        private void removeEspecie()
        {
            if(verificaGridVazio())
            {
                EspecieContr especie = new EspecieContr();
                especie.removeEspecie(Convert.ToInt32(dgEspecie.SelectedCells[0].Value));

                if(especie.getResultado())
                {
                    if(!dgEspecie.SelectedCells[2].Value.ToString().Equals(""))
                    {
                        Midia.removeArquivo(dgEspecie.SelectedCells[2].Value.ToString());
                    }

                    MessageBox.Show("Espécie " + dgEspecie.SelectedCells[1].Value + " removida com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    carregaGrid("");
                }
                else
                {
                    MessageBox.Show("Erro ao remover a espécie " + dgEspecie.SelectedCells[1].Value + ".\nErro: " + especie.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Para realizar essa ação é necessário que haja uma espécie selecionada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void abreEspecie(string tipo)
        {
            if(verificaGridVazio())
            {
                AbreEditaEspecie informacoes = new AbreEditaEspecie(Convert.ToInt32(dgEspecie.SelectedCells[0].Value), tipo);
                informacoes.ShowDialog();

                if(informacoes.getStatus())
                {
                    carregaGrid("");
                }

                informacoes.Dispose();

            }
            else
            {
                MessageBox.Show("Para realizar essa ação é necessário que haja uma espécie selecionada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        
    }
}
