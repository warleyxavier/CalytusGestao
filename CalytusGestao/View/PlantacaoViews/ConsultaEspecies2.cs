using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using CalytusGestao.Controller.Controller;

namespace CalytusGestao.View.PlantacaoViews
{
    public partial class ConsultaEspecies2 : Form
    {
        private bool status = false;
        private int codPlantacao;

        public ConsultaEspecies2(int codigo)
        {
            InitializeComponent();

            codPlantacao = codigo;
        }

        private void ConsultaEspecies2_Load(object sender, EventArgs e)
        {
            carregaGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            addEspecie();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            removeEspecie();
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            removeEspecie();
        }

        public bool getStatus()
        {
            return status;
        }

        private void carregaGrid()
        {
            SQLiteDataReader leitor = null;

            try
            {
                PlantacaoContr plantacao = new PlantacaoContr();
                leitor = plantacao.consultaEspPlantacao(codPlantacao);

                if(plantacao.getResultado())
                {
                    dgEspecie.Rows.Clear();

                    if(plantacao.getDados())
                    {
                        while(leitor.Read())
                        {
                            dgEspecie.Rows.Add(leitor["plECodigo"], leitor["espCodigo"], leitor["espNomePopular"]);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Foram encontrados erros ao carregar os dados.\nErro: " + plantacao.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Foram encontrados erros ao listar os dados.\nErro: " + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                leitor.Dispose();
            }
        }

        private bool checaGridVazio()
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

        private void removeEspecie()
        {
            if (checaGridVazio())
            {
                DialogResult resposta = MessageBox.Show("Realmente deseja remover a espécie " + dgEspecie.SelectedCells[2].Value + "?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resposta.Equals(DialogResult.Yes))
                {
                    PlantacaoContr plantacaoo = new PlantacaoContr();
                    plantacaoo.remocaoEspPlantacao(Convert.ToInt32(dgEspecie.SelectedCells[0].Value));

                    if(plantacaoo.getResultado())
                    {
                        MessageBox.Show("Espécie " + dgEspecie.SelectedCells[2].Value + " removida com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        carregaGrid();
                        status = true;
                    }
                    else
                    {
                        MessageBox.Show("Foram encontrados problemas ao remover a espécie " + dgEspecie.SelectedCells[2].Value + "\nErro: " + plantacaoo.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Para acessar essa opção é necessário que haja espécies listadas.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void addEspecie()
        {
            AdicionaEspecie adiciona = new AdicionaEspecie();

            if(dgEspecie.RowCount > 0)
            {
                int[] restricao = new int[dgEspecie.RowCount];

                for(int i = 0; i < dgEspecie.RowCount; i++)
                {
                    restricao[i] = Convert.ToInt32(dgEspecie[1, i].Value);
                }

                adiciona.setRestricao(restricao, true);
            }

            adiciona.ShowDialog();

            if(adiciona.getStatus())
            {
                int[] codsEspecies = adiciona.getEspecies();

                PlantacaoContr plantacao = new PlantacaoContr();
                plantacao.cadastroEspPlantacao(codsEspecies, codPlantacao);

                if(plantacao.getResultado())
                {
                    MessageBox.Show("Espécies cadastradas com sucesso na plantação.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    carregaGrid();
                    status = true;
                }
                else
                {
                    MessageBox.Show("Foram encontrados problemas ao inseris as espécies na plantação.\nErro: " + plantacao.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } 
            }
        }

        
    }
}
