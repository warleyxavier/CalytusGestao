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
using CalytusGestao.View.DoencaViews;
using CalytusGestao.View.EspeciesViews;
using CalytusGestao.View.FerramentasViews;

namespace CalytusGestao.View.PlantacaoViews
{
    public partial class AdicionaAtividade : Form
    {
        private int codPlantacao;
        private bool status = false;

        public AdicionaAtividade(int codigo)
        {
            InitializeComponent();

            codPlantacao = codigo;
        }

        private void AdicionaAtividade_Load(object sender, EventArgs e)
        {
            carregaTipos();
            carregaDadosPlantacao();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            limparCampos();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            execSalvar();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            catalogoEspecies();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            catalogoDoencas();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            calagem();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            adubacao();
        }

        private void tbCustos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)44 && tbCustos.Text.Equals(""))
            {
                e.Handled = true;
            }
            else if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != (char)44 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            else if (tbCustos.Text.Contains(",") && !Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void tbGanhos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)44 && tbGanhos.Text.Equals(""))
            {
                e.Handled = true;
            }
            else if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != (char)44 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            else if (tbGanhos.Text.Contains(",") && !Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        public bool getStatus()
        {
            return status;
        }

        private void carregaTipos()
        {
            try
            {
                AtividadeContr atividade = new AtividadeContr();
                Atividade[] atividades = atividade.consultaTipos("");

                if(atividade.getResultado())
                {
                    if(atividade.getDados())
                    {
                        foreach(Atividade atividadeT in atividades)
                        {
                            cbTipo.Items.Add(atividadeT.gsTipo);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Foram encontrados problemas ao carregar os dados.\nErro: " + atividade.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("Foram encontrados problemas ao listar os dados.\nErro: " + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void carregaDadosPlantacao()
        {
            try
            {
                PlantacaoContr plantacaoContr = new PlantacaoContr();
                Plantacao plantacao = plantacaoContr.consultaPlantacao(codPlantacao);

                if(plantacaoContr.getResultado())
                {
                    if(plantacaoContr.getDados())
                    {
                        lbIdentificacao.Text = (plantacao.Identificacao.Length > 17 ? plantacao.Identificacao.Substring(0,17) + "..." : plantacao.Identificacao);
                        lbAno.Text = plantacao.AnoPlantacao.ToString();
                        lbQtdPlantas.Text = plantacao.QtdPlantasAtual.ToString();
                        lbTamanho.Text = plantacao.TamanhoArea.ToString() + " hcts";
                        lbIdade.Text = plantacao.Idade.ToString() + " anos";
                    }
                }
                else
                {
                    MessageBox.Show("Foram encontrados problemas ao carregar os dados.\nErro: " + plantacaoContr.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Foram encontrados problemas ao listar os dados.\nErro: " + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void limparCampos()
        {
            cbTipo.Text = "";
            dtData.Value = DateTime.Today;
            tbCustos.Text = "0,00";
            tbGanhos.Text = "0,00";
            tbRelatorio.Text = "";
        }

        private void catalogoDoencas()
        {
            ConsultaDoenca consulta = new ConsultaDoenca();
            consulta.ShowDialog();
            consulta.Dispose();
        }

        private void catalogoEspecies()
        {
            ConsultaEspecies consulta = new ConsultaEspecies();
            consulta.ShowDialog();
            consulta.Dispose();
        }

        private bool checaCamposVazios()
        {
            if(cbTipo.Text.Equals("") || tbCustos.Text.Equals("") || tbGanhos.Text.Equals("") || tbCustos.Text.Equals(",") || tbGanhos.Text.Equals(","))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void execSalvar()
        {
            if(checaCamposVazios())
            {
                AtividadeContr atividade = new AtividadeContr();
                int codTipo = atividade.consultaCodTipo(cbTipo.Text);

                if(atividade.getResultado())
                {
                    atividade.cadastroAtividade(codPlantacao, dtData.Text, Convert.ToDouble(tbCustos.Text), Convert.ToDouble(tbGanhos.Text), codTipo, tbRelatorio.Text);

                    if(atividade.getResultado())
                    {
                        MessageBox.Show("Atividade cadastrada com sucesso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        status = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Foram encontrados problemas ao cadastrar a atividade.\nErro: " + atividade.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Foram encontrados problemas ao carregar o código do tipo.\nErro: " + atividade.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }              
            }
            else
            {
                MessageBox.Show("Verifique se todos os campos foram preenhcidos corretamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void calagem()
        {
            int codAnalise = getCodAnaliseSolo();

            if(codAnalise > 0)
            {
                AnaliseContr analiseC = new AnaliseContr();
                Analise analise = analiseC.consultaAnalise(codAnalise);

                if (analiseC.getResultado())
                {
                    Calagem calagem = new Calagem(analise.CtcT, analise.IndSaturacao);
                    calagem.ShowDialog();
                    calagem.Dispose();
                }
                else
                {
                    MessageBox.Show("Foram encontrados problemas ao pesquisar os dados da última análise de solo realizada na plantação.\nErro: " + analiseC.getResultado(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Não há nenhuma análise se solo cadastrada para efetuar o cálculo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private int getCodAnaliseSolo()
        {
            int codAnalise = 0;

            AnaliseContr analiseC = new AnaliseContr();
            Analise[] analises = analiseC.consultaAnalises("", codPlantacao);

            if(analiseC.getResultado())
            {
                if(analiseC.getDados())
                {
                    codAnalise = analises[0].Codigo;
                }
            }
            else
            {
                MessageBox.Show("Foram encontrados problemas ao pesquisar o código da última análise de solo realizada na plantação.\nErro: " + analiseC.getResultado(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return codAnalise;
        }

        private void adubacao()
        {
            int codAnalise = getCodAnaliseSolo();

            if (codAnalise > 0)
            {
                AnaliseContr analiseC = new AnaliseContr();
                Analise analise = analiseC.consultaAnalise(codAnalise);

                if (analiseC.getResultado())
                {
                    Adubacao adubacao = new Adubacao(analise.Fosforo, analise.Potassio);
                    adubacao.ShowDialog();
                    adubacao.Dispose();
                }
                else
                {
                    MessageBox.Show("Foram encontrados problemas ao pesquisar os dados da última análise de solo realizada na plantação.\nErro: " + analiseC.getResultado(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Não há nenhuma análise se solo cadastrada para efetuar o cálculo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
