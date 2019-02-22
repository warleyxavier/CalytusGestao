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

namespace CalytusGestao.View.DoencaViews
{
    public partial class ConsultaDoenca : Form
    {
        public ConsultaDoenca()
        {
            InitializeComponent();            

            cbTipoPesquisa.Text = "Nome";

            carregaGrid("");
            carregaLocais();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            abreDoenca("Abre");
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
            abreDoenca("Abre");
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            abreDoenca("Edita");
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            execRemove();
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
            if(tbPesquisa.Text.Length % 4 == 0)
            {
                carregaGrid(tbPesquisa.Text.Trim());
            }
        }

        private void dgDoenca_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            abreDoenca("Abre");
        }

        private void dgDoenca_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                abreDoenca("Abre");
            }
        }

        private void carregaGrid(string pesquisa)
        {
            try
            {
                DoencaContr doenca = new DoencaContr();

                string tipo;

                if(cbTipoPesquisa.Text.Equals("Nome"))
                {
                    tipo = "Nome";
                }
                else
                {
                    tipo = "Local";
                    pesquisa = cbLocais.Text;
                }

                Doenca[] doencas = doenca.consultaDoencas(pesquisa, tipo);

                if (doenca.getResultado())
                {
                    dgDoenca.Rows.Clear();

                    if (doenca.getDados())
                    {
                        for(int i = 0; i < doencas.Length; i++)
                        {
                            dgDoenca.Rows.Add(doencas[i].gsCodigo, doencas[i].gsNome, doencas[i].gsImagem);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Erro ao consultar doenças no banco de dados.\nErro: " + doenca.getErro(), "Avisoo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro: " + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool verificaGrid()
        {
            if(dgDoenca.RowCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void execRemove()
        {
            if(verificaGrid())
            {
                DialogResult resposta = MessageBox.Show("Realmente deseja remover a doença " + dgDoenca.SelectedCells[1].Value + "?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resposta.Equals(DialogResult.Yes))
                {
                    DoencaContr doenca = new DoencaContr();
                    doenca.removeDoenca(Convert.ToInt32(dgDoenca.SelectedCells[0].Value));

                    if (doenca.getResultado())
                    {
                        if(!dgDoenca.SelectedCells[2].Value.ToString().Equals(""))
                        {
                            Midia.removeArquivo(dgDoenca.SelectedCells[2].Value.ToString());
                        }

                        MessageBox.Show("Doença " + dgDoenca.SelectedCells[1].Value + " removida com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        carregaGrid(tbPesquisa.Text.Trim());
                    }
                    else
                    {
                        MessageBox.Show("Erro ao remover a doença " + dgDoenca.SelectedCells[1].Value + ".\nErro: " + doenca.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("É necessário que haja uma doença selecionada para realizar a remoção.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void abreDoenca(string tipo)
        {
            if(verificaGrid())
            {
                AbreEditaDoenca informacoes = new AbreEditaDoenca(Convert.ToInt32(dgDoenca.SelectedCells[0].Value), tipo);
                informacoes.ShowDialog();

                if(informacoes.getStatus())
                {
                    carregaGrid("");
                }

                informacoes.Dispose();
            }
            else
            {
                MessageBox.Show("É necessário que haja uma doença selecionada para realizar esta ação.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cbTipoPesquisa_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool status;

            if(cbTipoPesquisa.Text.Equals("Nome"))
            {
                status = true;
            }
            else
            {
                status = false;
            }

            tbPesquisa.Visible = status;
            lbDoenca.Visible = status;

            cbLocais.Visible = !status;
            lbLocal.Visible = !status;
        }

        private void carregaLocais()
        {
            LocalContr local = new LocalContr();
            Local[] locais = local.consultaLocais("");

            if(local.getDados())
            {
                for(int i = 0; i < locais.Length; i++)
                {
                    cbLocais.Items.Add(locais[i].gsLocal);
                }
            }
        }
    }
}
