using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CalytusGestao.Model.Helpers;
using CalytusGestao.Controller.Controller;
using CalytusGestao.Model.Classes;
using CalytusGestao.Model.Helpers;

namespace CalytusGestao.View.AnaliseViews
{
    public partial class CadastroAnalise : Form
    {
        private int codPlantacao;
        private bool status = false;

        public CadastroAnalise(int codPlantacao)
        {
            InitializeComponent();

            this.codPlantacao = codPlantacao;
        }

        private void CadastroAnalise_Load(object sender, EventArgs e)
        {
            lbImagem.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            escolheImagem();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            limpaCampos();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cadastraAnalise();
        }

        private void tbProfundidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)44 && tbProfundidade.Text.Equals(""))
            {
                e.Handled = true;
            }
            else if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != (char)44 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            else if (tbProfundidade.Text.Contains(",") && !Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void tbPh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)44 && tbPh.Text.Equals(""))
            {
                e.Handled = true;
            }
            else if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != (char)44 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            else if (tbPh.Text.Contains(",") && !Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
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

        private void tbCalcio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)44 && tbCalcio.Text.Equals(""))
            {
                e.Handled = true;
            }
            else if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != (char)44 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            else if (tbCalcio.Text.Contains(",") && !Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void tbMagnesio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)44 && tbMagnesio.Text.Equals(""))
            {
                e.Handled = true;
            }
            else if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != (char)44 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            else if (tbMagnesio.Text.Contains(",") && !Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void tbAluminio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)44 && tbAluminio.Text.Equals(""))
            {
                e.Handled = true;
            }
            else if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != (char)44 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            else if (tbAluminio.Text.Contains(",") && !Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void tbCtct_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)44 && tbCtct.Text.Equals(""))
            {
                e.Handled = true;
            }
            else if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != (char)44 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            else if (tbCtct.Text.Contains(",") && !Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void tbCtcT2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)44 && tbCtcT2.Text.Equals(""))
            {
                e.Handled = true;
            }
            else if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != (char)44 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            else if (tbCtcT2.Text.Contains(",") && !Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
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
            else if (tbPh.Text.Contains(",") && !Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void tbPRem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)44 && tbPRem.Text.Equals(""))
            {
                e.Handled = true;
            }
            else if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != (char)44 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            else if (tbPRem.Text.Contains(",") && !Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
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

        private void dtData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                cadastraAnalise();
            }
        }

        public bool getStatus()
        {
            return status;
        }

        private void escolheImagem()
        {
            abrir.FileName = "";

            if (abrir.ShowDialog().Equals(DialogResult.OK))
            {
                lbImagem.Text = Midia.nomeArquivoEndereco(abrir.FileName);
            }
        }
        
        private void limpaCampos()
        {
            dtData.Value = DateTime.Today;
            tbProfundidade.Text = "";
            tbPh.Text = "";
            tbFosforo.Text = "";
            tbPotassio.Text = "";
            tbCalcio.Text = "";
            tbMagnesio.Text = "";
            tbAluminio.Text = "";
            tbCtct.Text = "";
            tbCtcT2.Text = "";
            tbPRem.Text = "";
            tbV.Text = "";
        }

        private void cadastraAnalise()
        {
            Analise analise = new Analise();
            analise.Profundidade = (tbProfundidade.Text.Equals("") || tbProfundidade.Text.Equals(",") ? 0 : Convert.ToDouble(tbProfundidade.Text));
            analise.Ph = (tbPh.Text.Equals("") || tbPh.Text.Equals(",") ? 0 : Convert.ToDouble(tbPh.Text));
            analise.Fosforo = (tbFosforo.Text.Equals("") || tbFosforo.Text.Equals(",") ? 0 : Convert.ToDouble(tbFosforo.Text));
            analise.Potassio = (tbPotassio.Text.Equals("") || tbPotassio.Text.Equals(",") ? 0 : Convert.ToDouble(tbPotassio.Text));
            analise.Calcio = (tbCalcio.Text.Equals("") || tbCalcio.Text.Equals(",") ? 0 : Convert.ToDouble(tbCalcio.Text));
            analise.Magnesio = (tbMagnesio.Text.Equals("") || tbMagnesio.Text.Equals(",") ? 0 : Convert.ToDouble(tbMagnesio.Text));
            analise.Aluminio = (tbAluminio.Text.Equals("") || tbAluminio.Text.Equals(",") ? 0 : Convert.ToDouble(tbAluminio.Text));
            analise.Ctct = (tbCtct.Text.Equals("") || tbCtct.Text.Equals(",") ? 0 : Convert.ToDouble(tbCtct.Text));
            analise.CtcT = (tbCtcT2.Text.Equals("") || tbCtcT2.Text.Equals(",") ? 0 : Convert.ToDouble(tbCtcT2.Text));
            analise.IndSaturacao = (tbV.Text.Equals("") || tbV.Text.Equals(",") ? 0 : Convert.ToDouble(tbV.Text));
            analise.PRem = (tbPRem.Text.Equals("") || tbPRem.Text.Equals(",") ? 0 : Convert.ToDouble(tbPRem.Text));

            AnaliseContr analiseC = new AnaliseContr();
            analiseC.cadastroAnalise(codPlantacao, dtData.Value.ToString(), analise.Profundidade, analise.Ph, analise.Fosforo, analise.Potassio,
                analise.Calcio, analise.Magnesio, analise.Aluminio, analise.Ctct, analise.CtcT, 
                analise.IndSaturacao, analise.PRem, lbImagem.Text);

            if(analiseC.getResultado())
            {

                if (!lbImagem.Text.Equals(""))
                {
                    string endPasta = @"C:\Users\Public\Documents\Excellence\CalytusGestao\Imagens\Analises\";
                    Midia.copiaArquivo(abrir.FileName, endPasta + lbImagem.Text);
                }

                MessageBox.Show("Análise de solo cadastrada com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                status = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Foram encontrados problemas ao cadastrar a análise de solo.\nErro: " + analiseC.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
