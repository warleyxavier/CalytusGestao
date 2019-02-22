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
using System.Data.SQLite;

namespace CalytusGestao.View.EspeciesViews
{
    public partial class AlteraUtilidades : Form
    {
        private int codEspecie;
        private bool status = false;

        public AlteraUtilidades(int codigo)
        {
            InitializeComponent();

            codEspecie = codigo;
            carregaGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            execAdicionarUtilidade();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            execRemoverUtilidade();
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            execRemoverUtilidade();
        }

        public bool getStatus()
        {
            return status;
        }

        private bool verificaGridVazio()
        {
            if(dgUtilidade.RowCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void carregaGrid()
        {
            try
            {
                UtilidadeContr utilidade = new UtilidadeContr();
                SQLiteDataReader leitor = utilidade.consultaUtilidadesEspecie(codEspecie);

                if (utilidade.getResultado())
                {
                    dgUtilidade.Rows.Clear();

                    if (leitor.HasRows)
                    {
                        while (leitor.Read())
                        {
                            dgUtilidade.Rows.Add(leitor["utilEsCodigo"], leitor["utilEsUtilidade"], leitor["utiUtilidade"]);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Erro ao carregar os locais do banco de dados.\nErro: " + utilidade.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro ao listar os dados.\nErro: " + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void execRemoverUtilidade()
        {
            if(verificaGridVazio())
            {
                UtilidadeContr utilidade = new UtilidadeContr();
                utilidade.removeUtilidadeEspecie(Convert.ToInt32(dgUtilidade.SelectedCells[0].Value));

                if(utilidade.getResultado())
                {
                    MessageBox.Show("Utilidade " + dgUtilidade.SelectedCells[2].Value + " removida com sucesso da espécie.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    carregaGrid();
                    status = true;
                }
                else
                {
                    MessageBox.Show("Erro ao remover a utilidade " + dgUtilidade.SelectedCells[2].Value + ".\nErro: " + utilidade.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Para realizar esta ação é necessário que hajam dados litados.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void execAdicionarUtilidade()
        {
            EscolhaUtilidades escolhe = new EscolhaUtilidades();

            if(dgUtilidade.RowCount > 0)
            {
                int[] codsUtilidadesRestricao = new int[dgUtilidade.RowCount];

                for(int i = 0; i < dgUtilidade.RowCount; i++)
                {
                    codsUtilidadesRestricao[i] = Convert.ToInt32(dgUtilidade[1,i].Value);
                }

                escolhe.setRestricao(codsUtilidadesRestricao, true);
            }

            escolhe.ShowDialog();

            if(escolhe.getStatus())
            {
                int[] codUtilidades = escolhe.getCodsUtilidades();

                UtilidadeContr utilidade = new UtilidadeContr();
                utilidade.cadastraUtilidadeEspecie(codEspecie, codUtilidades);

                if(utilidade.getResultado())
                {
                    MessageBox.Show("Utilidade(s) adicionada(s) com sucesso a Espécie.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    carregaGrid();
                    status = true;
                }
                else
                {
                    MessageBox.Show("Erro ao adicionar utilidade(s) a Espécie.\nErro: " + utilidade.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            escolhe.Dispose();
        }
    }
}
