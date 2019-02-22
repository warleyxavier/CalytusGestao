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

namespace CalytusGestao.View.AnaliseViews
{
    public partial class ConsultaAnalise : Form
    {

        private int codPlantacao;
        private bool status = false;

        public ConsultaAnalise(int codPlantacao)
        {
            InitializeComponent();

            this.codPlantacao = codPlantacao;
        }

        private void ConsultaAnalise_Load(object sender, EventArgs e)
        {
            carregaAnalises("");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            cadastraAnalise();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            carregaAnalises("");
            dtData.Value = DateTime.Today;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            carregaAnalises(dtData.Value.ToString().Substring(0, 10));
        }

        private void dtData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                carregaAnalises(dtData.Value.ToString().Substring(0, 10));
            }
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            removeAnalise();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            abreAnalise();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            abreAnalise();
        }

        private void dgAnalise_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            abreAnalise();
        }

        private void dgAnalise_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                abreAnalise();
            }
        }

        public bool getStatus()
        {
            return status;
        }

        private void cadastraAnalise()
        {
            CadastroAnalise cadastro = new CadastroAnalise(codPlantacao);
            cadastro.ShowDialog();

            if(cadastro.getStatus())
            {
                status = true;
                carregaAnalises("");
                dtData.Value = DateTime.Today;
            }

            cadastro.Dispose();
        }

        private void carregaAnalises(string data)
        {
            try
            {
                AnaliseContr analise = new AnaliseContr();
                Analise[] analises = analise.consultaAnalises(data, codPlantacao);

                if (analise.getResultado())
                {
                    dgAnalise.Rows.Clear();

                    if (analise.getDados())
                    {
                        foreach(Analise analiseT in analises)
                        {
                            dgAnalise.Rows.Add(analiseT.Codigo, analiseT.Data, analiseT.Profundidade.ToString("N2"), analiseT.Imagem);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Foram encontrados problemas ao carregar as análises.\nErro: " + analise.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("Foram encontrados problemas ao listar as análises.\nErro: " + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private bool verificaGridVazio()
        {
            if(dgAnalise.RowCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void removeAnalise()
        {
            if (verificaGridVazio())
            {
                DialogResult resposta = MessageBox.Show("Realmente deseja excluir a análise de solo selecionada?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resposta.Equals(DialogResult.Yes))
                {
                    AnaliseContr analise = new AnaliseContr();
                    analise.removeAnalise(Convert.ToInt32(dgAnalise.SelectedCells[0].Value));

                    if (analise.getResultado())
                    {
                        if (!dgAnalise.SelectedCells[3].Value.ToString().Equals("null") && !dgAnalise.SelectedCells[3].Value.ToString().Equals(""))
                        {
                            Midia.removeArquivo(dgAnalise.SelectedCells[3].Value.ToString());
                        }

                        status = true;
                        carregaAnalises("");
                        dtData.Value = DateTime.Today;

                        MessageBox.Show("Análise removida com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Foram encontrados problemas ao remover a análise selecionada.\nErro: " + analise.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Para acessar esta opção é necessário que haja uma análise selecionada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void abreAnalise()
        {
            if(verificaGridVazio())
            {
                InfAnalise informacoes = new InfAnalise(Convert.ToInt32(dgAnalise.SelectedCells[0].Value));
                informacoes.ShowDialog();

                if(informacoes.getStatus())
                {
                    carregaAnalises("");
                }

                informacoes.Dispose();
            }
            else
            {
                MessageBox.Show("Para acessar esta opção é necessário que haja uma análise selecionada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
