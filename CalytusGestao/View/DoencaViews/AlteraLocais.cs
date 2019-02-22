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

namespace CalytusGestao.View.DoencaViews
{
    public partial class AlteraLocais : Form
    {
        private bool status = false;
        private int codDoenca;

        public AlteraLocais(int codigo)
        {
            InitializeComponent();

            this.codDoenca = codigo;

            carregaLocais();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            execAdicionaLocalDoenca();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            execRemoveLocalDoenca();
        }

        public bool getStatus()
        {
            return status;
        }

        private void carregaLocais()
        {
            try
            {
                LocalContr local = new LocalContr();
                SQLiteDataReader leitor = local.consultaLocaisDoenca(codDoenca);

                if(local.getResultado())
                {
                    dgLocal.Rows.Clear();

                    if (leitor.HasRows)
                    {
                        while (leitor.Read())
                        {
                            dgLocal.Rows.Add(leitor["lodCodigo"], leitor["locCodigo"], leitor["locLocal"]);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Erro ao carregar os locais do banco de dados.\nErro: " + local.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro ao carregar os dados.\nErro: " + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void execRemoveLocalDoenca()
        {
            if(dgLocal.RowCount > 0)
            {
                LocalContr local = new LocalContr();
                local.removeLocalDoenca(Convert.ToInt32(dgLocal.SelectedCells[0].Value));

                if(local.getResultado())
                {
                    MessageBox.Show("Local " + dgLocal.SelectedCells[2].Value.ToString() + " removido com sucesso da doença.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    carregaLocais();
                    status = true;
                }
                else
                {
                    MessageBox.Show("Erro ao remover o local da doença.\nErro: " + local.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Para realizar essa ação é necessário que haja pelo menos um local selecionado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void execAdicionaLocalDoenca()
        {
            EscolhaLocais escolhe = new EscolhaLocais();

            if (dgLocal.RowCount > 0)
            {
                int[] restricao = new int[dgLocal.RowCount];

                for(int i = 0; i < dgLocal.RowCount; i++)
                {
                    restricao[i] = Convert.ToInt32(dgLocal[1,i].Value);
                }
                
                escolhe.setRestricao(restricao, true);
            }
            escolhe.ShowDialog();

            if(escolhe.getStatus())
            {
                int[] locais = escolhe.getcodLocais();

                LocalContr local = new LocalContr();
                local.cadatraLocaisDoenca(codDoenca, locais);

                if(local.getResultado())
                {
                    MessageBox.Show("Locais adicionados com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    carregaLocais();
                    status = true;
                }
                else
                {
                    MessageBox.Show("Erro ao adicionar os locais.\nErro: " + local.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            escolhe.Dispose();
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            execRemoveLocalDoenca();
        }
    }
}
