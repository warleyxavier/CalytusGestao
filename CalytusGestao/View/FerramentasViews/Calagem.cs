using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalytusGestao.Model.Classes;
using System.Windows.Forms;

namespace CalytusGestao.View.FerramentasViews
{
    public partial class Calagem : Form
    {
        public Calagem()
        {
            InitializeComponent();

            visibilidade(true);
        }

        public Calagem(double CTCT, double saturacao)
        {
            InitializeComponent();

            tbT.Text = CTCT.ToString("N2");
            tbV.Text = saturacao.ToString("N2");

            calcula();

            if(!lbResultado.Text.Equals(""))
            {
                visibilidade(false);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)44 && tbT.Text.Equals(""))
            {
                e.Handled = true;
            }
            else if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != (char)44 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            else if (tbT.Text.Contains(",") && !Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void tbV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)44 && tbV.Text.Equals(""))
            {
                e.Handled = true;
            }
            else if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != (char)44 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            else if (tbV.Text.Contains(",") && !Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            calcula();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            limpaCampos();
            visibilidade(true);
        }
        
        private void tbT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                calcula();
            }
        }

        private void visibilidade(bool estado)
        {
            tbT.Enabled = estado;
            tbV.Enabled = estado;

            lbResultado.Visible = !estado;

            btnCalcular.Visible = estado;
            btnLimpar.Visible = !estado;
        }

        private bool checaCamposVazios()
        {
            if(tbT.Text.Equals("") || tbV.Text.Equals("") || tbT.Text.Equals(",") || tbV.Text.Equals(","))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void calcula()
        {
            if(checaCamposVazios())
            {
                Analise analise = new Analise();
                analise.CtcT = Convert.ToDouble(tbT.Text);
                analise.IndSaturacao = Convert.ToDouble(tbV.Text);

                Especie especie = new Especie();
                double resultado = especie.calcCalagem(analise);

                if(!resultado.Equals(0))
                {
                    lbResultado.Text = "HÁ A NECESSIDADE DE APLICAÇÃO DE " + resultado + " TON/HA NO SOLO\nESPECIFICADO PARA A PRODUÇÃO DE EUCALIPTO.";
                }
                else
                {
                    lbResultado.Text = "NÃO HÁ A NECESSIDADE DE APLICAÇÃO DE CALCÁRIO NO SOLO\nESPECIFICADO PARA A PRODUÇÃO DE EUCALIPTO.";
                }

                visibilidade(false);
            }
            else
            {
                MessageBox.Show("Verifique se todos os campos foram preenchidos corretamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void limpaCampos()
        {
            tbV.Text = "";
            tbT.Text = "";
            lbResultado.Text = "";
        }
    }
}
