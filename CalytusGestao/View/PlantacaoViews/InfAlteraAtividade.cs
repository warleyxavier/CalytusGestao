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
    public partial class InfAlteraAtividade : Form
    {
        Atividade atividade = new Atividade();

        private int codAtividade;
        private int codPlantacao;

        private bool status = false;

        public InfAlteraAtividade(int codAtividade2)
        {
            InitializeComponent();

            codAtividade = codAtividade2;
        }

        private void InfAlteraAtividade_Load(object sender, EventArgs e)
        {
            carregaTipos();
            carregaAtividade();
            visibilidade(false);
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            execSalvar();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            limpaCampos();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            descarregaObjeto();
            visibilidade(false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            carregaObjeto();
            visibilidade(true);
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

        private void carregaAtividade()
        {
            try
            {
                AtividadeContr atividadeContr = new AtividadeContr();
                Atividade atividadeT = atividadeContr.consultaAtividade(codAtividade);

                if(atividadeContr.getResultado())
                {
                    if(atividadeContr.getDados())
                    {
                        cbTipo.Text = atividadeT.gsTipo;
                        tbCustos.Text = atividadeT.gsCustos.ToString();
                        tbGanhos.Text = atividadeT.gsGanhos.ToString();
                        tbRelatorio.Text = atividadeT.gsRelatorio;
                        dtData.Text = atividadeT.gsData;

                        codPlantacao = atividadeT.gsPlantacao;
                    }
                }
                else
                {
                    MessageBox.Show("Foram encontrados erros ao carregar os dados da atividade.\nErro: " + atividadeContr.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("Foram encontrados erros ao listar os dados da atividade.\nErro: " + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void carregaTipos()
        {
            try
            {
                AtividadeContr tiposContr = new AtividadeContr();
                Atividade[] tipos = tiposContr.consultaTipos("");

                if(tiposContr.getResultado())
                {
                    if(tiposContr.getDados())
                    {
                        foreach(Atividade atividadeT in tipos)
                        {
                            cbTipo.Items.Add(atividadeT.gsTipo);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Foram encontrados erros ao carregar os tipos.\nErro: " + tiposContr.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Foram encontrados erros ao listar os tipos.\nErro: " + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void visibilidade(bool estado)
        {
            cbTipo.Enabled = estado;
            dtData.Enabled = estado;
            tbCustos.Enabled = estado;
            tbGanhos.Enabled = estado;
            tbRelatorio.Enabled = estado;

            btnCancelar.Visible = estado;
            btnSalvar.Visible = estado;
            btnLimpar.Visible = estado;

            btnEditar.Visible = !estado;
        }

        private void carregaObjeto()
        {
            atividade.gsTipo = cbTipo.Text;
            atividade.gsData = dtData.Text;
            atividade.gsCustos = Convert.ToDouble(tbCustos.Text);
            atividade.gsGanhos = Convert.ToDouble(tbGanhos.Text);
            atividade.gsRelatorio = tbRelatorio.Text;
        }

        private void descarregaObjeto()
        {
            cbTipo.Text = atividade.gsTipo;
            dtData.Text = atividade.gsData;
            tbCustos.Text = atividade.gsCustos.ToString();
            tbGanhos.Text = atividade.gsGanhos.ToString();
            tbRelatorio.Text = atividade.gsRelatorio;
        }

        private void limpaCampos()
        {
            cbTipo.Text = "OUTRO";
            dtData.Value = DateTime.Today;
            tbCustos.Text = "0,00";
            tbGanhos.Text = "0,00";
            tbRelatorio.Text = "";
        }

        private bool checaCamposVazios()
        {
            if(tbCustos.Text.Equals("") || tbCustos.Text.Equals(",") || tbGanhos.Text.Equals("") || tbGanhos.Text.Equals(","))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private int getCodTipo()
        {
            int codTipo = 0;

            AtividadeContr atividadeDados = new AtividadeContr();
            codTipo = atividadeDados.consultaCodTipo(cbTipo.Text);

            return codTipo;
        }

        private void execSalvar()
        {
            if(checaCamposVazios())
            {
                int codTipo = getCodTipo();

                if(!codTipo.Equals(0))
                {
                    AtividadeContr atividadeDados = new AtividadeContr();
                    atividadeDados.alteraAtividade(codAtividade, codTipo, dtData.Text, (!tbCustos.Text.Equals("") ? Convert.ToDouble(tbCustos.Text) : 0), (!tbGanhos.Text.Equals("") ? Convert.ToDouble(tbGanhos.Text) : 0), tbRelatorio.Text.Trim(), codPlantacao);

                    if(atividadeDados.getResultado())
                    {
                        MessageBox.Show("Atividade alterada com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        visibilidade(false);
                        carregaObjeto();
                        status = true;
                    }
                    else
                    {
                        MessageBox.Show("Foram encontrados problemas ao alterar a atividade.\nErro: " + atividadeDados.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    }
                }
            }
            else
            {
                MessageBox.Show("Verifique se os campos de valores foram preenchidos corretamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
