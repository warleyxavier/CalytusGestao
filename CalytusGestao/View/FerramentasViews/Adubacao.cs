using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CalytusGestao.Model.Classes;

namespace CalytusGestao.View.FerramentasViews
{
    public partial class Adubacao : Form
    {
        public Adubacao()
        {
            InitializeComponent();
        }

        public Adubacao(double fosforo, double potassio)
        {
            InitializeComponent();

            tbFosforo.Text = fosforo.ToString("N2");
            tbPotassio.Text = potassio.ToString("N2");
        }

        private void Adubacao_Load(object sender, EventArgs e)
        {
            visibilidades(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            calculaAdubacao();
        }

        private void tbFosforo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)44 && tbFosforo.Text.Equals(""))
            {
                e.Handled = true;
            }
            else if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != (char)44 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            else if (tbFosforo.Text.Contains(",") && !Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void tbPotassio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)44 && tbPotassio.Text.Equals(""))
            {
                e.Handled = true;
            }
            else if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != (char)44 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            else if (tbPotassio.Text.Contains(",") && !Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void tbFosforo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                calculaAdubacao();
            }
        }

        private void calculaAdubacao()
        {
            if(checaCamposVazios())
            {
                int produtividade = Convert.ToInt32(gbProdutividade.Controls.OfType<RadioButton>().SingleOrDefault(rad => rad.Checked.Equals(true)).Text);

                string solo = gbSolo.Controls.OfType<RadioButton>().SingleOrDefault(rad => rad.Checked.Equals(true)).Text;

                Especie especie = new Especie();

                int resultFosforo = especie.calcFosforo(produtividade, Convert.ToDouble(tbFosforo.Text), solo);

                if(resultFosforo.Equals(0))
                {
                    lbResultadoFosforo.Text = "NÃO FOI POSSÍVEL OBTER NENHUM RESULTADO.";
                }
                else if(resultFosforo < 200)
                {
                    lbResultadoFosforo.Text = "É NECESSÁRIO A APLICAÇÃO DE " + resultFosforo + " G DE SUPERFOSFATO SIMPLES DO MOMENTO DO PLANTIO,\nCONSIDERANDO UMA COVA MÉDIA (20 x 20 x 20cm)";
                }
                else
                {
                    lbResultadoFosforo.Text = "É NECESSÁRIO INCORPORAR " + resultFosforo + " KG/ha DE FOSFATO NATURAL EM UM SULCO AO LADO DA\nLINHA DE PLANTIO";
                }

                double resultPotassio = especie.calcPotassio(Convert.ToDouble(tbPotassio.Text), produtividade);

                if(resultPotassio.Equals(0))
                {
                    lbResultadoPotassio.Text = "NÃO HÁ A NECESSIDADE DE APLICAÇÃO DE ADUBO POTÁSSICO NO SOLO ESPECIFICADO.";

                    lbResultadoNitrogenio.Text = "APLIQUE 80 G DE SULFATO DE AMÔNIO UM ANO APÓS O PLANTIO" + (produtividade >= 40 ? ", REPETINDO A\nOPERAÇÃO APÓS UM ANO." : ".");
                }
                else
                {
                    lbResultadoPotassio.Text = "É NECESSÁRIA A APLICAÇÃO DE " + resultPotassio + " KG/ha DE K20. PARCELANDO O MESMO EM DUAS\nAPLICAÇÕES, A PRIMEIRA 4 MESES APÓS O PLANTIO E A SEGUNDA APÓS 18 MESES.";

                    lbResultadoNitrogenio.Text = "RECOMENDA-SE A APLICAÇÃO DO ADUBO COMPOSTO 10-0-20.";
                }

                visibilidades(false);
            }
            else
            {
                MessageBox.Show("Verifique se todos os campos foram preenchidos corretamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool checaCamposVazios()
        {
            if(tbFosforo.Text.Equals("") || tbPotassio.Text.Equals("") || tbFosforo.Text.Equals(",") || tbPotassio.Text.Equals(","))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void visibilidades(bool estado)
        {
            tbFosforo.Enabled = estado;
            tbPotassio.Enabled = estado;

            btnCalcular.Visible = estado;
            btnLimpar.Visible = !estado;

            lbResultadoFosforo.Visible = !estado;
            lbResultadoPotassio.Visible = !estado;
            lbResultadoNitrogenio.Visible = !estado;
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            tbPotassio.Text = "";
            tbFosforo.Text = "";

            lbResultadoNitrogenio.Text = "";
            lbResultadoFosforo.Text = "";
            lbResultadoPotassio.Text = "";

            visibilidades(true);
        }
    }
}
