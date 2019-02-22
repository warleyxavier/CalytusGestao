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
using System.Data.SQLite;

namespace CalytusGestao.View.EspeciesViews
{
    public partial class AbreEditaEspecie : Form
    {
        // *****************************************************************
        // ***************** DECLARAÇÃO DOS ATRIBUTOS **********************
        // *****************************************************************

        private int codEspecie;
        private bool status = false;

        private string nomePopular;
        private string nomeCietifico;
        private string caracteristicas;
        private string imagem = "";

        public AbreEditaEspecie(int codigo, string tipo)
        {
            InitializeComponent();

            codEspecie = codigo;

            carregaEspecie();
            carregaUtilidades();

            if (tipo.Equals("Abre"))
            {
                lbTitulo.Text = "Informações da Espécie";
                visibilidade(false);
            }
            else
            {
                lbTitulo.Text = "Alteração da Espécie";
                visibilidade(true);
                carregaVariaveis();
            }           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            descarregaVariaveis();
            visibilidade(false);
            lbTitulo.Text = "Informações da Espécie";
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            carregaVariaveis();
            visibilidade(true);
            lbTitulo.Text = "Alteração da Espécie";
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            execSalvar();
        }

        private void btnAlterarImg_Click(object sender, EventArgs e)
        {
            alteraImagem();
        }

        private void btnRemoveImg_Click(object sender, EventArgs e)
        {
            removeImagem();
        }

        private void btnAlterarLocais_Click(object sender, EventArgs e)
        {
            execAlteraUtilidades();
        }

        public bool getStatus()
        {
            return status;
        }

        private bool verificaCamposVazios()
        {
            if(tbNomePopular.Text.Trim().Equals(""))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void carregaVariaveis()
        {
            nomePopular = tbNomePopular.Text;
            nomeCietifico = tbNomeCientifico.Text;
            caracteristicas = tbCaracteristicas.Text;
            imagem = pbImagem.ImageLocation;
        }

        private void descarregaVariaveis()
        {
            tbNomePopular.Text = nomePopular;
            tbNomeCientifico.Text = nomeCietifico;
            tbCaracteristicas.Text = caracteristicas;
            pbImagem.ImageLocation = imagem;
        }


        private void visibilidade(bool status)
        {
            tbNomePopular.Enabled = status;
            tbNomeCientifico.Enabled = status;
            tbCaracteristicas.Enabled = status;

            btnCancelar.Visible = status;
            btnSalvar.Visible = status;
            btnAlterarUtilidades.Visible = status;
            btnEditar.Visible = !status;
        }

        private void carregaEspecie()
        {
            try
            {
                EspecieContr especieCt = new EspecieContr();
                Especie especie = especieCt.consultaEspecie(codEspecie);

                if(especieCt.getResultado())
                {
                    if(especieCt.getDados())
                    {
                        tbNomePopular.Text = especie.gsNomePopular;
                        tbNomeCientifico.Text = especie.gsNomeCientifico;
                        tbCaracteristicas.Text = especie.gsCaractesristicas;
                        
                        if (!especie.gsImagem.Equals(""))
                        {
                            pbImagem.ImageLocation = especie.gsImagem;
                        }
                        else
                        {
                            pbImagem.ImageLocation = "";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Erro ao carregar dados do banco de dados.\nErro: " + especieCt.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("Erro ao listar dados do banco de dados.\nErro: " +  e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void execSalvar()
        {
            if(verificaCamposVazios())
            {
                try
                {
                    EspecieContr especie = new EspecieContr();
                    especie.alterarEspecie(codEspecie, tbNomePopular.Text.Trim(), tbNomeCientifico.Text.Trim(), tbCaracteristicas.Text.Trim());

                    if (especie.getResultado())
                    {
                        MessageBox.Show("Espécie " + tbNomePopular.Text.Trim() + " alterada com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        visibilidade(false);
                        lbTitulo.Text = "Informações da Espécie";

                        status = true;
                    }
                    else
                    {
                        MessageBox.Show("Erro ao alterar a espécie.\nErro: " + especie.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch(Exception e)
                {
                    MessageBox.Show("Erro: " + e.Message);
                }
            }
            else
            {
                MessageBox.Show("O campos de nome popular é obrigatório.\nVerifique se este foi preenchido corretamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void carregaUtilidades()
        {
            try
            {
                UtilidadeContr utilidade = new UtilidadeContr();
                SQLiteDataReader leitor = utilidade.consultaUtilidadesEspecie(codEspecie);

                string utilidades = "";

                while(leitor.Read())
                {
                    utilidades += leitor["utiUtilidade"].ToString() + " - "; 
                }

                tbUtilidades.Text = utilidades;
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro ao listar dados do banco de dados.\nErro: " + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void execAlteraUtilidades()
        {
            AlteraUtilidades utilidades = new AlteraUtilidades(codEspecie);
            utilidades.ShowDialog();

            if(utilidades.getStatus())
            {
                carregaUtilidades();
            }

            utilidades.Dispose();
        }

        private void removeImagem()
        {
            if(!pbImagem.ImageLocation.Equals(""))
            {
                DialogResult resposta = MessageBox.Show("Realmente deseja remover a imagem da espécie?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if(resposta.Equals(DialogResult.Yes))
                {
                    EspecieContr especie = new EspecieContr();
                    especie.alteraImagemEsp(codEspecie, "");

                    if(especie.getResultado())
                    {
                        Midia.removeArquivo(pbImagem.ImageLocation);
                        pbImagem.ImageLocation = "";
                        pbImagem.Image = Properties.Resources.logoCalytus5;

                        status = true;
                        MessageBox.Show("Imagem removida com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Foram encontrados problemas ao remover a imagem da espécie.\nErro: " + especie.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Não é possível realizar a remoção pois não há nenhuma imagem a ser removida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void alteraImagem()
        {
            string imgAntiga = pbImagem.ImageLocation;

            abrir.FileName = "";

            if(abrir.ShowDialog().Equals(DialogResult.OK))
            {
                if(!abrir.FileName.Equals(""))
                {
                    DialogResult resposta = MessageBox.Show("Realmente deseja alterar a imagem da espécie?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if(resposta.Equals(DialogResult.Yes))
                    {
                        string enderecoPasta = @"C:\Users\Public\Documents\Excellence\CalytusGestao\Imagens\Especies\";
                        
                        EspecieContr especie = new EspecieContr();
                        especie.alteraImagemEsp(codEspecie, enderecoPasta + Midia.nomeArquivoEndereco(abrir.FileName));

                        if(especie.getResultado())
                        {
                            if (!pbImagem.ImageLocation.Equals(""))
                            {
                                Midia.removeArquivo(imgAntiga);
                            }

                            Midia.copiaArquivo(abrir.FileName, enderecoPasta + Midia.nomeArquivoEndereco(abrir.FileName));
                            pbImagem.ImageLocation = enderecoPasta + Midia.nomeArquivoEndereco(abrir.FileName);

                            status = true;

                            MessageBox.Show("Imagem alterada com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Foram encontrados problemas ao alterar a imagem da espécie.\nErro: " + especie.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
    }
}
