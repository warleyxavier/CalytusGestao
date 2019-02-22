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

namespace CalytusGestao.View.PlantacaoViews
{
    public partial class ConsultaPlantacoes : Form
    {
        public ConsultaPlantacoes()
        {
            InitializeComponent();
        }

        private void ConsultaPlantacoes_Load(object sender, EventArgs e)
        {
            carregaGrid("");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            execAbrirPlantacao();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            carregaGrid(tbPesquisa.Text.Trim());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            carregaGrid("");
        }

        private void tbPesquisa_TextChanged(object sender, EventArgs e)
        {
            if (tbPesquisa.Text.Trim().Length % 3 == 0)
            {
                carregaGrid(tbPesquisa.Text.Trim());
            }
        }

        private void tbPesquisa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                carregaGrid(tbPesquisa.Text.Trim());
            }
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            execAbrirPlantacao();
        }

        private void dgPlantacao_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            execAbrirPlantacao();
        }

        private void dgPlantacao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                execAbrirPlantacao();
            }
        }

        private bool checaGridVazio()
        {
            if(dgPlantacao.RowCount > 0)
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
                PlantacaoContr plantacao = new PlantacaoContr();
                Plantacao[] plantacoes = plantacao.consultaPlantacoes(pesquisa);

                if(plantacao.getResultado())
                {
                    dgPlantacao.Rows.Clear();

                    if(plantacao.getDados())
                    {
                        for(int i = 0; i < plantacoes.Length; i++)
                        {
                            dgPlantacao.Rows.Add(plantacoes[i].Codigo, plantacoes[i].Identificacao, plantacoes[i].Status, plantacoes[i].Imagem);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Erro ao carregar os dados do banco de dados.\nErro: " + plantacao.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro ao listar os dados.\nErro: " + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void removePlantacao()
        {
            if(checaGridVazio())
            {
                DialogResult resposta = MessageBox.Show("Realmente deseja remover a plantação " + dgPlantacao.SelectedCells[1].Value + "?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resposta.Equals(DialogResult.Yes))
                {
                    PlantacaoContr plantacao = new PlantacaoContr();
                    plantacao.removePlantacao(Convert.ToInt32(dgPlantacao.SelectedCells[0].Value));

                    if (plantacao.getResultado())
                    {
                        if(!dgPlantacao.SelectedCells[3].Value.ToString().Equals(""))
                        {
                            Midia.removeArquivo(dgPlantacao.SelectedCells[3].Value.ToString());
                        }

                        MessageBox.Show("Plantação " + dgPlantacao.SelectedCells[1].Value + " removida com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        carregaGrid("");
                    }
                    else
                    {
                        MessageBox.Show("Erro ao remover a plantação " + dgPlantacao.SelectedCells[1].Value + ".\nErro: " + plantacao.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Para realizar essa ação é necessário que haja alguma plantação selecionada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            removePlantacao();
        }

        private void execAbrirPlantacao()
        {
            if(checaGridVazio())
            {
                this.Visible = false;

                InfPlantacao informacoes = new InfPlantacao(Convert.ToInt32(dgPlantacao.SelectedCells[0].Value));
                informacoes.ShowDialog();

                this.Visible = true;

                if(informacoes.getStatus())
                {
                    carregaGrid("");
                }

                informacoes.Dispose();
            }
            else
            {
                MessageBox.Show("Para realizar essa ação é necessário que haja alguma plantação selecionada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }        
    }
}
