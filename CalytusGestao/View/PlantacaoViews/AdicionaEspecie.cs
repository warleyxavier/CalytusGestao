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
    public partial class AdicionaEspecie : Form
    {
        private int[] especies = null;
        private bool status = false;

        private int[] restricaoCods = null;
        private bool restricao = false;

        public AdicionaEspecie()
        {
            InitializeComponent();
        }

        public int[] getEspecies()
        {
            return especies;
        }

        public bool getStatus()
        {
            return status;
        }

        public void setRestricao(int[] cods, bool statusRestr)
        {
            restricaoCods = cods;
            restricao = statusRestr;
        }

        private void AdicionaEspecie_Load(object sender, EventArgs e)
        {
            carregaGrid("");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            addEspecie();
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
            if (tbPesquisa.Text.Trim().Length % 2 == 0)
            {
                carregaGrid(tbPesquisa.Text.Trim());
            }
        }

        private void cbMarca_CheckedChanged(object sender, EventArgs e)
        {
            if (cbMarca.Checked)
            {
                for (int i = 0; i < dgEspecie.RowCount; i++)
                {
                    dgEspecie[0, i].Value = true;
                }
            }
            else
            {
                for (int i = 0; i < dgEspecie.RowCount; i++)
                {
                    dgEspecie[0, i].Value = false;
                }
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
                        bool resultado = false;

                        foreach(Especie especieT in especies)
                        {
                            if (restricao)
                            {
                                if (restricaoCods.Contains(especieT.gsCodigo))
                                {
                                    resultado = false;
                                }
                                else
                                {
                                    resultado = true;
                                }

                                if (resultado)
                                {
                                    dgEspecie.Rows.Add(true, especieT.gsCodigo, especieT.gsNomePopular);
                                }
                            }
                            else
                            {
                                dgEspecie.Rows.Add(true, especieT.gsCodigo, especieT.gsNomePopular);
                            }                                                            
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Foram encontrados erros ao carregar os dados.\nErro: " + especie.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Foram encontrados erros ao listar os dados.\nErro: " + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addEspecie()
        {
            int quantidade = 0;

            for(int i = 0; i < dgEspecie.RowCount; i++)
            {
                if(dgEspecie[0,i].Value.Equals(true))
                {
                    quantidade++;
                }
            }

            if(quantidade > 0)
            {
                especies = new int[quantidade];

                int contador = 0;

                for(int i = 0; i < dgEspecie.RowCount; i++)
                {
                    
                    if(dgEspecie[0,i].Value.Equals(true))
                    {
                        especies[contador] = Convert.ToInt32(dgEspecie[1,i].Value);
                        contador++;
                    }
                }

                status = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Para salvar novas espécies se torna necessário que sejam marcadas aquelas que se deseja incluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
