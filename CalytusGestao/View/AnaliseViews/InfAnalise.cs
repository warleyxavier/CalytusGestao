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
using CalytusGestao.Model.Classes;
using CalytusGestao.Model.Helpers;

namespace CalytusGestao.View.AnaliseViews
{
    public partial class InfAnalise : Form
    {
        private int codAnalise;
        private bool status = false;

        Analise analiseObj = new Analise();


        public InfAnalise(int codAnalise)
        {
            InitializeComponent();

            this.codAnalise = codAnalise;
        }
        
        private void InfAnalise_Load(object sender, EventArgs e)
        {
            lbImagem.Text = "";
            carregaAnalise();

            visibilidadeCampos(false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            carregaObjeto();
            visibilidadeCampos(true);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            descarregaObjeto();
            visibilidadeCampos(false);
        }

        private void btnVerImg_Click(object sender, EventArgs e)
        {
            visibilidadePanels(false);
        }

        private void btnRemoveImg_Click(object sender, EventArgs e)
        {
            removeImg();
        }

        private void btnAltImg_Click(object sender, EventArgs e)
        {
            alteraImagem();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            visibilidadePanels(true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            removeImg();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            alteraImagem();
        }


        private void btnSalvar_Click(object sender, EventArgs e)
        {
            alteraAnalise();
        }

        private void tbPh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                alteraAnalise();
            }
        }

        public bool getStatus()
        {
            return status;
        }

        private void carregaAnalise()
        {
            try
            {
                AnaliseContr analiseC = new AnaliseContr();
                Analise analise = analiseC.consultaAnalise(codAnalise);

                if(analiseC.getResultado())
                {
                    if(analiseC.getDados())
                    {
                        dtData.Value = Convert.ToDateTime(analise.Data);
                        tbProfundidade.Text = analise.Profundidade.ToString("N2");
                        tbPh.Text = analise.Ph.ToString("N2");
                        tbFosforo.Text = analise.Fosforo.ToString("N2");
                        tbPotassio.Text = analise.Potassio.ToString("N2");
                        tbCalcio.Text = analise.Calcio.ToString("N2");
                        tbMagnesio.Text = analise.Magnesio.ToString("N2");
                        tbAluminio.Text = analise.Aluminio.ToString("N2");
                        tbCtct.Text = analise.Ctct.ToString("N2");
                        tbCtcT2.Text = analise.CtcT.ToString("N2");
                        tbV.Text = analise.IndSaturacao.ToString("N2");
                        tbPRem.Text = analise.PRem.ToString("N2");

                        if(!analise.Imagem.Equals("") && !analise.Imagem.Equals("null"))
                        {
                            pbAnalise.ImageLocation = analise.Imagem;
                            lbImagem.Text = Midia.nomeArquivoEndereco(analise.Imagem);
                            visibilidadeBtnImg(true);
                        }
                        else
                        {
                            visibilidadeBtnImg(false);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Foram encontrados problemas ao carregar os dados da análise.\nErro: " + analiseC.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("Foram encontrados problemas ao listar os dados da análise.\nErro: " + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void visibilidadeCampos(bool estado)
        {
            dtData.Enabled = estado;
            tbProfundidade.Enabled = estado;
            tbPh.Enabled = estado;
            tbFosforo.Enabled = estado;
            tbPotassio.Enabled = estado;
            tbCalcio.Enabled = estado;
            tbMagnesio.Enabled = estado;
            tbAluminio.Enabled = estado;
            tbCtct.Enabled = estado;
            tbCtcT2.Enabled = estado;
            tbV.Enabled = estado;
            tbPRem.Enabled = estado;

            btnEditar.Visible = !estado;

            btnSalvar.Visible = estado;
            btnCancelar.Visible = estado;
        }

        private void visibilidadeBtnImg(bool estado)
        {
            btnVerImg.Visible = estado;
            btnRemoveImg.Visible = estado;
        }
        
        private void visibilidadePanels(bool estado)
        {
            gbDados.Visible = estado;
            gbAnalise.Visible = estado;
            gbMidia.Visible = estado;
            gbImagem.Visible = !estado;
        }

        private void carregaObjeto()
        {
            analiseObj.Data = dtData.Value.ToString().Substring(0,10);
            analiseObj.Profundidade = Convert.ToDouble(tbProfundidade.Text);
            analiseObj.Ph = Convert.ToDouble(tbPh.Text);
            analiseObj.Fosforo = Convert.ToDouble(tbFosforo.Text);
            analiseObj.Potassio = Convert.ToDouble(tbPotassio.Text);
            analiseObj.Calcio = Convert.ToDouble(tbCalcio.Text);
            analiseObj.Magnesio = Convert.ToDouble(tbMagnesio.Text);
            analiseObj.Aluminio = Convert.ToDouble(tbAluminio.Text);
            analiseObj.Ctct = Convert.ToDouble(tbCtct.Text);
            analiseObj.CtcT = Convert.ToDouble(tbCtcT2.Text);
            analiseObj.IndSaturacao = Convert.ToDouble(tbV.Text);
            analiseObj.PRem = Convert.ToDouble(tbPRem.Text);
        }

        private void descarregaObjeto()
        {
            dtData.Value = Convert.ToDateTime(analiseObj.Data);
            tbProfundidade.Text = analiseObj.Profundidade.ToString("N2");
            tbPh.Text = analiseObj.Ph.ToString("N2");
            tbFosforo.Text = analiseObj.Fosforo.ToString("N2");
            tbPotassio.Text = analiseObj.Potassio.ToString("N2");
            tbCalcio.Text = analiseObj.Calcio.ToString("N2");
            tbMagnesio.Text = analiseObj.Magnesio.ToString("N2");
            tbAluminio.Text = analiseObj.Aluminio.ToString("N2");
            tbCtct.Text = analiseObj.Ctct.ToString("N2");
            tbCtcT2.Text = analiseObj.CtcT.ToString("N2");
            tbV.Text = analiseObj.IndSaturacao.ToString("N2");
            tbPRem.Text = analiseObj.PRem.ToString("N2");
        }

        private void removeImg()
        {
            DialogResult resposta = MessageBox.Show("Realmente deseja remover a imagem da análise de solo?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resposta.Equals(DialogResult.Yes))
            {
                AnaliseContr analise = new AnaliseContr();
                analise.alteraImagemAnalise(codAnalise, "");

                if (analise.getResultado())
                {
                    status = true;

                    Midia.removeArquivo(pbAnalise.ImageLocation);

                    lbImagem.Text = "";
                    visibilidadeBtnImg(false);
                    visibilidadePanels(true);
                    pbAnalise.ImageLocation = "";

                    MessageBox.Show("Imagem removida com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Foram encontrados problemas ao remover a imagem.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }                
            }
        }

        private void alteraImagem()
        {
            abrir.FileName = "";

            if (abrir.ShowDialog().Equals(DialogResult.OK))
            {
                DialogResult resposta = MessageBox.Show("Realmente deseja alterar a imagem da análise de solo?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resposta.Equals(DialogResult.Yes))
                {
                    string endPasta = @"C:\Users\Public\Documents\Excellence\CalytusGestao\Imagens\Analises\";


                    AnaliseContr analise = new AnaliseContr();
                    analise.alteraImagemAnalise(codAnalise, endPasta + Midia.nomeArquivoEndereco(abrir.FileName));

                    if (analise.getResultado())
                    {
                        status = true;

                        try
                        {
                            if (!pbAnalise.ImageLocation.Equals(""))
                            {
                                Midia.removeArquivo(pbAnalise.ImageLocation);
                            }

                            Midia.copiaArquivo(abrir.FileName, endPasta + Midia.nomeArquivoEndereco(abrir.FileName));

                            lbImagem.Text = Midia.nomeArquivoEndereco(abrir.FileName);
                            pbAnalise.ImageLocation = endPasta + Midia.nomeArquivoEndereco(abrir.FileName);
                            visibilidadeBtnImg(true);

                            MessageBox.Show("Imagem alterada com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch(Exception e)
                        {
                            MessageBox.Show("Foram encontrados problemas ao copiar a nova imagem para a pasta de origem.\nErro: " + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Foram encontrados problemas ao alterar a imagem.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void alteraAnalise()
        {
            Analise analise = new Analise();

            analise.Codigo = codAnalise;
            analise.Data = Helper.formataData(dtData.Text);
            analise.Profundidade = ((tbProfundidade.Text.Equals("") || tbProfundidade.Text.Equals(",")) ?  0 : Convert.ToDouble(tbProfundidade.Text));
            analise.Ph = ((tbPh.Text.Equals("") || tbPh.Text.Equals(",")) ? 0 : Convert.ToDouble(tbPh.Text));
            analise.Fosforo = ((tbFosforo.Text.Equals("") || tbFosforo.Text.Equals(",")) ? 0 : Convert.ToDouble(tbFosforo.Text));
            analise.Potassio = ((tbPotassio.Text.Equals("") || tbPotassio.Text.Equals(",")) ? 0 : Convert.ToDouble(tbPotassio.Text));
            analise.Calcio = ((tbCalcio.Text.Equals("") || tbCalcio.Text.Equals(",")) ? 0 : Convert.ToDouble(tbCalcio.Text));
            analise.Magnesio = ((tbMagnesio.Text.Equals("") || tbMagnesio.Text.Equals(",")) ? 0 : Convert.ToDouble(tbMagnesio.Text));
            analise.Aluminio = ((tbAluminio.Text.Equals("") || tbAluminio.Text.Equals(",")) ? 0 : Convert.ToDouble(tbAluminio.Text));
            analise.CtcT = ((tbCtcT2.Text.Equals("") || tbCtcT2.Text.Equals(",")) ? 0 : Convert.ToDouble(tbCtcT2.Text));
            analise.Ctct = ((tbCtct.Text.Equals("") || tbCtct.Text.Equals(",")) ? 0 : Convert.ToDouble(tbCtct.Text));
            analise.IndSaturacao = ((tbV.Text.Equals("") || tbV.Text.Equals(",")) ? 0 : Convert.ToDouble(tbV.Text));
            analise.PRem = ((tbPRem.Text.Equals("") || tbPRem.Text.Equals(",")) ? 0 : Convert.ToDouble(tbPRem.Text));

            AnaliseContr analiseC = new AnaliseContr();
            analiseC.alteraAnalise(analise);

            if(analiseC.getResultado())
            {
                MessageBox.Show("Análise alterada com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                tbProfundidade.Text = analise.Profundidade.ToString("N2");
                tbPh.Text = analise.Ph.ToString("N2");
                tbFosforo.Text = analise.Fosforo.ToString("N2");
                tbPotassio.Text = analise.Potassio.ToString("N2");
                tbCalcio.Text = analise.Calcio.ToString("N2");
                tbMagnesio.Text = analise.Magnesio.ToString("N2");
                tbAluminio.Text = analise.Aluminio.ToString("N2");
                tbCtcT2.Text = analise.CtcT.ToString("N2");
                tbV.Text = analise.IndSaturacao.ToString("N2");
                tbPRem.Text = analise.PRem.ToString("N2");

                visibilidadeCampos(false);

                status = true;
            }
            else
            {
                MessageBox.Show("Foram encontrados problemas ao alterar a análise.\nErro: " + analiseC.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
