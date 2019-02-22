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

namespace CalytusGestao.View.EspeciesViews
{
    public partial class EscolhaUtilidades : Form
    {
        private bool status = false;
        private int[] codUtilidades = null;
        private string utilidades = "";

        private int[] restricaoUtilidades = null;
        private bool restricao = false;

        public EscolhaUtilidades()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            execSalvar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            carregaGrid(tbPesquisa.Text.Trim());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            carregaGrid("");
        }

        private void EscolhaUtilidades_Load(object sender, EventArgs e)
        {
            carregaGrid("");
        }

        private void tbPesquisa_TextChanged(object sender, EventArgs e)
        {
            carregaGrid(tbPesquisa.Text.Trim());
        }

        private void cbMarca_CheckedChanged(object sender, EventArgs e)
        {
            bool status;

            if(cbMarca.Checked)
            {
                status = true;
            }
            else
            {
                status = false;
            }

            for(int i = 0; i < dgUtilidade.RowCount; i++)
            {
                dgUtilidade[0, i].Value = status;
            }
        }

        public bool getStatus()
        {
            return status;
        }

        public int[] getCodsUtilidades()
        {
            return codUtilidades;
        }

        public string getUtilidadesTexto()
        {
            return utilidades;
        }

        public void setRestricao(int[] codsRestricao, bool statusRestricao)
        {
            restricaoUtilidades = codsRestricao;
            restricao = statusRestricao;
        }

        private void carregaGrid(string pesquisa)
        {
            UtilidadeContr utilidade = new UtilidadeContr();
            Utilidade[] codsUtilidades = utilidade.consultaUtilidades(pesquisa);

            if (utilidade.getResultado())
            {
                dgUtilidade.Rows.Clear();

                if (utilidade.getDados())
                {
                    bool autorizacao = false;

                    for (int i = 0; i < codsUtilidades.Length; i++)
                    {
                        if (restricao)
                        {
                            if(restricaoUtilidades.Contains(codsUtilidades[i].gsCodigo))
                            {
                                autorizacao = false;
                            }
                            else
                            {
                                autorizacao = true;
                            }
                            
                            if(autorizacao)
                            {
                                dgUtilidade.Rows.Add(true, codsUtilidades[i].gsCodigo, codsUtilidades[i].gsUtilidade);
                            }
                        }
                        else
                        {
                            dgUtilidade.Rows.Add(true, codsUtilidades[i].gsCodigo, codsUtilidades[i].gsUtilidade);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Erro ao carregar dados do banco de dados.\nErro: " + utilidade.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void execSalvar()
        {
            if (dgUtilidade.RowCount > 0)
            {
                int quantidade = 0;

                for (int i = 0; i < dgUtilidade.RowCount; i++)
                {
                    if(dgUtilidade[0,i].Value.Equals(true))
                    {
                        quantidade++;
                    }
                }
                
                if(quantidade > 0)
                {
                    codUtilidades = new int[quantidade];

                    int contador = 0;

                    for(int i = 0; i < dgUtilidade.RowCount; i++)
                    {
                        if(dgUtilidade[0,i].Value.Equals(true))
                        {
                            codUtilidades[contador] = Convert.ToInt32(dgUtilidade[1,i].Value);

                            if(contador.Equals(quantidade))
                            {
                                utilidades += dgUtilidade[2, i].Value + ".";
                            }
                            else
                            {
                                utilidades += dgUtilidade[2, i].Value + "; ";
                            }
                            contador++;
                        }
                    }

                    status = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Para realizar essa ação é necessário que haja pelo menos uma utilidade marcada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Para realizar essa ação é necessário que haja utilidades na consulta.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        
    }
}
