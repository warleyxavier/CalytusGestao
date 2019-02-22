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
    public partial class AlteraPlantacao : Form
    {
        private int codPlantacao;
        private bool status = false;

        public AlteraPlantacao(int codigo)
        {
            InitializeComponent();

            codPlantacao = codigo;
        }

        private void AlteraPlantacao_Load(object sender, EventArgs e)
        {
            carregaCampos();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
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
            altera();
        }

        public bool getStatus()
        {
            return status;
        }

        private void tbAno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)) && !e.KeyChar.Equals((char)8))
            {
                e.Handled = true;
            }
        }

        private void tbTamArea_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)44 && tbTamArea.Text.Equals(""))
            {
                e.Handled = true;
            }
            else if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != (char)44 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            else if (tbTamArea.Text.Contains(",") && !Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void tbQtdPlantas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)) && !e.KeyChar.Equals((char)8))
            {
                e.Handled = true;
            }
        }

        private void tbAtualPlantas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)) && !e.KeyChar.Equals((char)8))
            {
                e.Handled = true;
            }
        }

        private void tbIdentificacao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                altera();
            }
        }

        private void carregaCampos()
        {
            try
            {
                PlantacaoContr plantacaoDados = new PlantacaoContr();
                Plantacao plantacao = plantacaoDados.consultaPlantacao(codPlantacao);

                if(plantacaoDados.getResultado())
                {
                    if (plantacaoDados.getDados())
                    {
                        tbIdentificacao.Text = plantacao.Identificacao;
                        tbAno.Text = plantacao.AnoPlantacao.ToString();
                        tbQtdPlantas.Text = plantacao.QtdPlantasPlantadas.ToString();
                        tbTamArea.Text = plantacao.TamanhoArea.ToString();
                        tbAtualPlantas.Text = plantacao.QtdPlantasAtual.ToString();

                        tbLocalizacao.Text = plantacao.Localizacao;
                        tbMunicipio.Text = plantacao.Municipio;

                        cbStatus.Text = plantacao.Status;
                    }
                }
                else
                {
                    MessageBox.Show("Foram encontrados problemas ao carregar os dados.\nErro: " + plantacaoDados.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("Foram encontrados problemas ao listar os dados.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void limpaCampos()
        {
            tbIdentificacao.Text = "";
            tbAno.Text = "";
            tbQtdPlantas.Text = "";
            tbAtualPlantas.Text = "";
            tbTamArea.Text = "";
            tbMunicipio.Text = "";
            tbLocalizacao.Text = "";
        }

        private void altera()
        {
            PlantacaoContr plantacao = new PlantacaoContr();
            plantacao.alteraPlantacao(codPlantacao, tbIdentificacao.Text.Trim(), Convert.ToInt32(tbAno.Text), Convert.ToInt32(tbQtdPlantas.Text), Convert.ToInt32(tbAtualPlantas.Text), Convert.ToDouble(tbTamArea.Text), tbMunicipio.Text, tbLocalizacao.Text, cbStatus.Text);

            if(plantacao.getResultado())
            {
                MessageBox.Show("Plantação alterada com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                status = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Foram encontrados problemas ao alterar a plantação.\nErro: " + plantacao.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
