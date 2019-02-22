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
using CalytusGestao.View.AnaliseViews;

namespace CalytusGestao.View.PlantacaoViews
{
    public partial class InfPlantacao : Form
    {
        private bool status = false;
        private int codPlantacao;

        public InfPlantacao(int codigoPlantacao)
        {
            InitializeComponent();

            codPlantacao = codigoPlantacao;
        }

        private void InfPlantacao_Load(object sender, EventArgs e)
        {
            carregaCampos();
            carregaEspecies();
            carregaAtividades();
            carregaAnalise();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRemoveImg_Click(object sender, EventArgs e)
        {
            removeImagem();
        }

        private void btnAlterarImg_Click(object sender, EventArgs e)
        {
            alteraImagem();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            editaPlantacao();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            gerencEspecies();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gerencAtividades();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            gerencAnalises();
        }

        public bool getStatus()
        {
            return status;
        }        

        private void pn01_MouseLeave(object sender, EventArgs e)
        {
            pn01.BackColor = Color.LightGray;
        }

        private void pn01_MouseHover(object sender, EventArgs e)
        {
            pn01.BackColor = Color.DimGray;
        }

        private void pn02_MouseLeave_1(object sender, EventArgs e)
        {
            pn02.BackColor = Color.LightGray;
        }

        private void pn02_MouseHover_1(object sender, EventArgs e)
        {
            pn02.BackColor = Color.DimGray;
        }

        private void pn03_MouseHover(object sender, EventArgs e)
        {
            pn03.BackColor = Color.DimGray;
        }

        private void pn03_MouseLeave(object sender, EventArgs e)
        {
            pn03.BackColor = Color.LightGray;
        }

        private void pn04_MouseHover(object sender, EventArgs e)
        {
            pn04.BackColor = Color.DimGray;
        }

        private void pn04_MouseLeave(object sender, EventArgs e)
        {
            pn04.BackColor = Color.LightGray;
        }

        private void label11_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            InfAlteraAtividade informacoes = new InfAlteraAtividade(Convert.ToInt32(lbCodAtv01.Text));
            informacoes.ShowDialog();

            if(informacoes.getStatus())
            {
                carregaAtividades();
            }

            informacoes.Dispose();
        }

        private void pn02_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            InfAlteraAtividade informacoes = new InfAlteraAtividade(Convert.ToInt32(lbCodAtv02.Text));
            informacoes.ShowDialog();

            if (informacoes.getStatus())
            {
                carregaAtividades();
            }

            informacoes.Dispose();
        }

        private void label28_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            InfAlteraAtividade informacoes = new InfAlteraAtividade(Convert.ToInt32(lbCodAtv03.Text));
            informacoes.ShowDialog();

            if (informacoes.getStatus())
            {
                carregaAtividades();
            }

            informacoes.Dispose();
        }

        private void carregaCampos()
        {
            try
            {
                PlantacaoContr plantacaoDados = new PlantacaoContr();
                Plantacao plantacao = plantacaoDados.consultaPlantacao(codPlantacao);

                if(plantacaoDados.getResultado())
                {
                    if(plantacaoDados.getDados())
                    {
                        tbIdentificacao.Text = plantacao.Identificacao;
                        lbAno.Text = plantacao.AnoPlantacao.ToString();
                        lbQtdCultivada.Text = plantacao.QtdPlantasPlantadas.ToString();
                        lbQtdAtual.Text = plantacao.QtdPlantasAtual.ToString();
                        lbTamanho.Text = plantacao.TamanhoArea.ToString() + " hcts";
                        lbStatus.Text = plantacao.Status;
                        lbMunicipio.Text = plantacao.Municipio;
                        tbLocalizacao.Text = plantacao.Localizacao;
                        lbCustos.Text = "R$ " + plantacao.Custos.ToString();
                        lbGanhos.Text = "R$ " + plantacao.Ganhos.ToString();
                        pbImagem.ImageLocation = (!plantacao.Imagem.Equals("") ? plantacao.Imagem : "");
                        lbIdade.Text = plantacao.Idade.ToString() + "ano(s)";

                        lbPrevisao.Text = (plantacao.AnoPlantacao + 6).ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Foram encontrados problemas ao carregar os dados.\nErro: " + plantacaoDados.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("Foram encontrados problemas ao listar os dados.\nErro: " + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void carregaEspecies()
        {
            try
            {
                PlantacaoContr plantacao = new PlantacaoContr();
                string nomes = plantacao.consultaNomesEspPlantacao(codPlantacao);

                if(plantacao.getResultado())
                {
                    tbEspecies.Text = nomes;
                }
                else
                {
                    MessageBox.Show("Foram encontrados problemas ao carregar os dados.\nErro: " + plantacao.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Foram encontrados problemas ao listar os dados.\nErro: " + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void removeImagem()
        {
            if(!pbImagem.ImageLocation.Equals(""))
            {
                try
                {
                    DialogResult resposta = MessageBox.Show("Realmente deseja remover a imagem da plantação?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resposta.Equals(DialogResult.Yes))
                    {
                        PlantacaoContr plantacao = new PlantacaoContr();
                        plantacao.alteraImagem(codPlantacao, "");

                        if (plantacao.getResultado())
                        {
                            Midia.removeArquivo(pbImagem.ImageLocation);
                            pbImagem.ImageLocation = "";
                            pbImagem.Image = Properties.Resources.logoCalytus5;

                            MessageBox.Show("Imagem removida com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Foram encontrados problemas ao remover a imagem.\nErro: " + plantacao.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch(Exception e)
                {
                    MessageBox.Show("Foram encontrados problemas ao remover a imagem.\nErro: " + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Não há imagem na plantação para ser removida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void alteraImagem()
        {
            try
            {
                if (abrir.ShowDialog().Equals(DialogResult.OK))
                {
                    DialogResult resposta = MessageBox.Show("Realmente deseja alterar a imagem da plantação?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resposta.Equals(DialogResult.Yes))
                    {
                        string endPasta = @"C:\Users\Public\Documents\Excellence\CalytusGestao\Imagens\Plantacoes\";

                        PlantacaoContr plantacao = new PlantacaoContr();
                        plantacao.alteraImagem(codPlantacao, endPasta + Midia.nomeArquivoEndereco(abrir.FileName));

                        if (plantacao.getResultado())
                        {
                            if (!pbImagem.ImageLocation.Equals(""))
                            {
                                Midia.removeArquivo(pbImagem.ImageLocation);
                            }

                            Midia.copiaArquivo(abrir.FileName, endPasta + Midia.nomeArquivoEndereco(abrir.FileName));
                            pbImagem.ImageLocation = endPasta + Midia.nomeArquivoEndereco(abrir.FileName);

                            MessageBox.Show("Imagem alterada com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Foram encontrados problemas ao alterar a imagem.\nErro: " + plantacao.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Foram encontrados problemas ao alterar a imagem.\nErro: " + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }   
        
        private void editaPlantacao()
        {
            AlteraPlantacao altera = new AlteraPlantacao(codPlantacao);
            altera.ShowDialog();

            if(altera.getStatus())
            {
                carregaCampos();
            }

            altera.Dispose();
        }

        private void gerencEspecies()
        {
            ConsultaEspecies2 consulta = new ConsultaEspecies2(codPlantacao);
            consulta.ShowDialog();

            if (consulta.getStatus())
            {
                carregaEspecies();
            }

            consulta.Dispose();
        }

        private void gerencAtividades()
        {
            ConsultaAtividade consulta = new ConsultaAtividade(codPlantacao);
            consulta.ShowDialog();

            if(consulta.getStatus())
            {
                carregaCampos();
                carregaAtividades();
            }

            consulta.Dispose();
        }

        private void carregaAtividades()
        {
            try
            {
                AtividadeContr atividade = new AtividadeContr();
                Atividade[] atividades = atividade.consultaAtividades("", codPlantacao);

                if(atividade.getResultado())
                {
                    if(atividade.getDados())
                    {
                        pn01.Visible = true;
                        lbCodAtv01.Text = atividades[0].gsCodigo.ToString();
                        lbTipAtv01.Text = atividades[0].gsTipo;
                        lbDataAtv01.Text = atividades[0].gsData;

                        if(atividades.Length > 1)
                        {
                            pn02.Visible = true;
                            lbCodAtv02.Text = atividades[1].gsCodigo.ToString();
                            lbTipAtv02.Text = atividades[1].gsTipo;
                            lbDataAtv02.Text = atividades[1].gsData;
                        }

                        if (atividades.Length > 2)
                        {
                            pn03.Visible = true;
                            lbCodAtv03.Text = atividades[2].gsCodigo.ToString();
                            lbTipAtv03.Text = atividades[2].gsTipo;
                            lbDataAtv03.Text = atividades[2].gsData;
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Foram encontrados problemas carregar as atividades.\nErro: " + atividade.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Foram encontrados problemas ao listar as atividades.\nErro: " + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gerencAnalises()
        {
            ConsultaAnalise consulta = new ConsultaAnalise(codPlantacao);
            consulta.ShowDialog();

            if(consulta.getStatus())
            {
                carregaAnalise();
            }

            consulta.Dispose();
        }

        private void carregaAnalise()
        {
            try
            {
                AnaliseContr analiseC = new AnaliseContr();
                Analise[] analises = analiseC.consultaAnalises("", codPlantacao);

                if(analiseC.getResultado())
                {
                    if(analiseC.getDados())
                    {
                        pn04.Visible = true;

                        lbCodigoAnalise.Text = analises[0].Codigo.ToString();
                        lbDataAnalise.Text = analises[0].Data.ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Foram encontrados problemas ao carregar a primeira análise.\nErro: " + analiseC.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("Foram encontrados problemas ao listar a primeira análise.\nErro: " + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pn04_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            InfAnalise informacoes = new InfAnalise(Convert.ToInt32(lbCodigoAnalise.Text));
            informacoes.ShowDialog();

            if(informacoes.getStatus())
            {
                carregaAnalise();
            }

            informacoes.Dispose();
        }
    }
}
