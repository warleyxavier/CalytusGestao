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

namespace CalytusGestao.View.DoencaViews
{
    public partial class EscolhaLocais : Form
    {
        private bool status = false;
        private string locais = "";
        private int[] codLocais = null;

        private bool restricao = false;
        private int[] restricaoLocais = null;

        public EscolhaLocais()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            status = false;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            carregaGrid(tbPesquisa.Text.Trim());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            execSalvar();
        }

        private void EscolhaLocais_Load(object sender, EventArgs e)
        {
            carregaGrid("");
        }

        public bool getStatus()
        {
            return status;
        }

        public int[] getcodLocais()
        {
            return codLocais;
        }

        public string getNomeLocais()
        {
            return locais;
        }

        public void setRestricao(int[] codsrestricao, bool status)
        {
            restricaoLocais = codsrestricao;
            restricao = status;
        }

        private void carregaGrid(string pesquisa)
        {
            try
            {
                LocalContr local = new LocalContr();
                Local[] locais = local.consultaLocais(pesquisa);

                if(local.getResultado())
                {
                    dgLocal.Rows.Clear();

                    if(local.getDados())
                    {
                        bool autorizacao = false;

                        for(int i = 0; i < locais.Length; i++)
                        {
                            if(restricao)
                            {
                                for(int j = 0; j < restricaoLocais.Length; j++)
                                {
                                    if(locais[i].gsCodigo.Equals(restricaoLocais[j]))
                                    {
                                        autorizacao = false;
                                        break;
                                    }
                                    else
                                    {
                                        autorizacao = true;                                       
                                    }
                                }

                                if(autorizacao)
                                {
                                    dgLocal.Rows.Add(true, locais[i].gsCodigo, locais[i].gsLocal);
                                }
                            }
                            else
                            {
                                dgLocal.Rows.Add(true, locais[i].gsCodigo, locais[i].gsLocal); 
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Erro ao carregar dados do banco de dados.\nErro: " + local.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro ao carregar dados do banco de dados.\nErro: " + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbPesquisa_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode.Equals(Keys.Enter))
            {
                carregaGrid(tbPesquisa.Text.Trim());
            }
        }

        private void tbPesquisa_TextChanged(object sender, EventArgs e)
        {
            if(tbPesquisa.Text.Length % 1 == 0)
            {
                carregaGrid(tbPesquisa.Text.Trim());
            }
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

            for(int i = 0; i < dgLocal.RowCount; i++)
            {
                dgLocal[0, i].Value = status;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            carregaGrid("");
        }

        private void execSalvar()
        {
            int quantidade = 0;

            for(int i = 0; i < dgLocal.RowCount; i++)
            {
                if(dgLocal[0,i].Value.Equals(true))
                {
                    quantidade++;
                }
            }

            if(quantidade > 0)
            {
                codLocais = new int[quantidade];

                int contador = 0;

                for (int i = 0; i < dgLocal.RowCount; i++)
                {
                    if (dgLocal[0, i].Value.ToString().Equals("True"))
                    {                        
                        codLocais[contador] = Convert.ToInt32(dgLocal[1,i].Value);

                        if(i < dgLocal.RowCount - 1)
                        {
                            locais += dgLocal[2, i].Value.ToString() + "; ";
                        }
                        else
                        {
                            locais += dgLocal[2, i].Value.ToString() + ".";
                        }
                        contador++;
                    }
                }

                status = true;

                this.Close();
            }
            else
            {
                MessageBox.Show("Para salvar os locais é necessário que haja pelo menos um local marcado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        
    }
}
