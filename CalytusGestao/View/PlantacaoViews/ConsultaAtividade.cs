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

namespace CalytusGestao.View.PlantacaoViews
{
    public partial class ConsultaAtividade : Form
    {
        private int codPlantacao;
        private bool status = false;

        public ConsultaAtividade(int codigo)
        {
            InitializeComponent();
            codPlantacao = codigo;
        }

        private void ConsultaAtividade_Load(object sender, EventArgs e)
        {
            carregaGrid("");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            abreAtividade();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            carregaGrid(tbPesquisa.Text.Trim());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            carregaGrid("");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            addAtividade();
        }

        private void tbPesquisa_TextChanged(object sender, EventArgs e)
        {
            if (tbPesquisa.Text.Length % 3 == 0)
            {
                carregaGrid(tbPesquisa.Text.Trim());
            }
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            removeAtividade();
        }

        private void dgAtividade_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            abreAtividade();
        }

        private void dgAtividade_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                abreAtividade();
            }
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            abreAtividade();
        }

        public bool getStatus()
        {
            return status;
        }

        private bool checaGridVazio()
        {
            if (dgAtividade.RowCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void addAtividade()
        {
            this.Visible = false;

            AdicionaAtividade adiciona = new AdicionaAtividade(codPlantacao);
            adiciona.ShowDialog();

            this.Visible = true;

            if (adiciona.getStatus())
            {
                status = true;
                carregaGrid("");
            }

            adiciona.Dispose();
        }

        private void carregaGrid(string pesquisa)
        {
            try
            {
                AtividadeContr atividade = new AtividadeContr();
                Atividade[] atividades = atividade.consultaAtividades(pesquisa, codPlantacao);

                if (atividade.getResultado())
                {
                    dgAtividade.Rows.Clear();

                    if (atividade.getDados())
                    {
                        foreach (Atividade atividadeT in atividades)
                        {
                            dgAtividade.Rows.Add(atividadeT.gsCodigo, atividadeT.gsTipo, atividadeT.gsCustos, atividadeT.gsGanhos, atividadeT.gsData);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Foram encontrados problemas ao carregar os dados.\nErro: " + atividade.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Foram encontrados problemas ao listar os dados.\nErro: " + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void removeAtividade()
        {
            if (checaGridVazio())
            {
                AtividadeContr atividade = new AtividadeContr();
                atividade.removeAtividade(Convert.ToInt32(dgAtividade.SelectedCells[0].Value), codPlantacao);

                if (atividade.getResultado())
                {
                    MessageBox.Show("Atividade removida com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    carregaGrid("");
                    status = true;
                }
                else
                {
                    MessageBox.Show("Foram encontrados problemas ao remover a atividade selecionada.\nErro: " + atividade.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Para executar essa ação é necessário que haja uma atividade selecionada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void abreAtividade()
        {
            if (checaGridVazio())
            {
                InfAlteraAtividade informacoes = new InfAlteraAtividade(Convert.ToInt32(dgAtividade.SelectedCells[0].Value));
                informacoes.ShowDialog();

                if (informacoes.getStatus())
                {
                    carregaGrid("");
                }

                informacoes.Dispose();
            }
            else
            {
                MessageBox.Show("Para executar essa ação é necessário que haja uma atividade selecionada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        
    }
}
