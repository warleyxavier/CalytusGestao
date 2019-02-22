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

namespace CalytusGestao.View.DoencaViews
{
    public partial class AbreEditaDoenca : Form
    {
        private bool status = false;
        private int codDoenca;
        private string nome;
        private string caracteristicas;
        private string diagnostico;
        private string imagem;

        public AbreEditaDoenca(int codigo, string tipo)
        {
            InitializeComponent();

            this.codDoenca = codigo;

            carregaCampos();

            if(tipo.Equals("Abre"))
            {
                visibilidade(false);

                lbTitulo.Text = "INFORMAÇÕES DA DOENÇA";
            }
            else
            {
                carregaVariaveis();
                visibilidade(true);

                lbTitulo.Text = "ALTERAÇÃO DA DOENÇA";
            }

            carregaLocais();
        }

        public bool getStatus()
        {
            return status;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            descarregaVariaveis();
            visibilidade(false);
            lbTitulo.Text = "INFORMAÇÕES DA DOENÇA";
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            carregaVariaveis();
            visibilidade(true);
            lbTitulo.Text = "EDIÇÃO DA DOENÇA";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            execAlterar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            alteraImagem();
        }

        private void btnRemoveImg_Click(object sender, EventArgs e)
        {
            removeImagem();
        }


        private void btnAlterarLocais_Click(object sender, EventArgs e)
        {
            alteraLocais();
        }

        private void carregaCampos()
        {
            try
            {
                DoencaContr doencCt = new DoencaContr();

                Doenca doenca = new Doenca();
                doenca = doencCt.consultaDoenca(codDoenca);

                if (doencCt.getResultado())
                {
                    if (!doenca.Equals(null))
                    {
                        tbNome.Text = doenca.gsNome;
                        tbCaracteristicas.Text = doenca.gsCaracteristicas;
                        tbDiagnostico.Text = doenca.gsDiagnostico;

                        if (!doenca.gsImagem.Equals(""))
                        {
                            pbImagem.ImageLocation = doenca.gsImagem;                            
                        }
                        else
                        {
                            pbImagem.ImageLocation = "";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Erro ao carregar os dados.\nErro: " + doencCt.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro ao carregar os dados.\nErro: " + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void carregaVariaveis()
        {
            nome = tbNome.Text;
            caracteristicas = tbCaracteristicas.Text;
            diagnostico = tbDiagnostico.Text;
            imagem = pbImagem.ImageLocation;
        }  

        private void descarregaVariaveis()
        {
            tbNome.Text = nome;
            tbCaracteristicas.Text = caracteristicas;
            tbDiagnostico.Text = diagnostico;
            pbImagem.ImageLocation = imagem;
        }

        private void visibilidade(bool status)
        {
            tbNome.Enabled = status;
            tbCaracteristicas.Enabled = status;
            tbDiagnostico.Enabled = status;

            btnEditar.Visible = !status;
            btnCancelar.Visible = status;
            btnSalvar.Visible = status;
            btnAlterarLocais.Visible = status;
        }

        private void execAlterar()
        {
            if (!tbNome.Text.Trim().Equals(""))
            {
                try
                {
                    DoencaContr doenca = new DoencaContr();
                    doenca.alteraDoenca(codDoenca, tbNome.Text.Trim(), tbCaracteristicas.Text.Trim(), tbDiagnostico.Text.Trim());

                    if (doenca.getResultado())
                    {
                        MessageBox.Show("Doença alterada com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.status = true;

                        visibilidade(false);
                        lbTitulo.Text = "INFORMAÇÕES DA DOENÇA";
                    }
                    else
                    {
                        MessageBox.Show("Erro ao alterar a doença " + nome + ".\nErro: " + doenca.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch(Exception e)
                {
                    MessageBox.Show("Erro: " + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Para realizar a alteração é necessário que haja valores no campo de nome.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void carregaLocais()
        {
            try
            {
                string sql = "";

                LocalContr local = new LocalContr();
                SQLiteDataReader leitor = local.consultaLocaisDoenca(codDoenca);

                if(local.getResultado())
                {
                    if(leitor.HasRows)
                    {
                        while(leitor.Read())
                        {
                            sql += leitor["locLocal"].ToString() + " - ";
                        }
                    }

                    lbLocais.Text = sql;
                }
                else
                {
                    MessageBox.Show("Erro ao carregar os locais do banco de dados.\nErro: " + local.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro ao carregar os dados.\nErro: " + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void alteraLocais()
        {
            AlteraLocais altera = new AlteraLocais(codDoenca);
            altera.ShowDialog();

            if(altera.getStatus())
            {
                carregaLocais();
            }

            altera.Dispose();
        }

        private void removeImagem()
        {
            if (!pbImagem.ImageLocation.Equals("") && !pbImagem.ImageLocation.Equals("null"))
            {
                DialogResult resposta = MessageBox.Show("Realmente deseja remover a imagem da doença?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resposta.Equals(DialogResult.Yes))
                {
                    DoencaContr doenca = new DoencaContr();
                    doenca.alteraImgDoenca(codDoenca, "");

                    if (doenca.getResultado())
                    {
                        Midia.removeArquivo(pbImagem.ImageLocation);
                        pbImagem.ImageLocation = "";
                        pbImagem.Image = Properties.Resources.logoCalytus5;

                        status = true;

                        MessageBox.Show("Imagem removida com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Foram encontrados problemas ao remover a imagem da doença.\nErro: " + doenca.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Não há como realizar a remoção pois não há imagem para ser removida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void alteraImagem()
        {
            abrir.FileName = "";

            if (abrir.ShowDialog().Equals(DialogResult.OK))
            {
                if (!abrir.FileName.Equals(""))
                {
                    DialogResult resposta = MessageBox.Show("Realmente deseja alterar a imagem da doença?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resposta.Equals(DialogResult.Yes))
                    {
                        string enderecoPasta = @"C:\Users\Public\Documents\Excellence\CalytusGestao\Imagens\Doenças\";
                        
                        DoencaContr doenca = new DoencaContr();
                        doenca.alteraImgDoenca(codDoenca, enderecoPasta + Midia.nomeArquivoEndereco(abrir.FileName));

                        if(doenca.getResultado())
                        {
                            if (!pbImagem.ImageLocation.Equals("") && !pbImagem.ImageLocation.Equals("null"))
                            {
                                Midia.removeArquivo(pbImagem.ImageLocation);
                            }

                            Midia.copiaArquivo(abrir.FileName, enderecoPasta + Midia.nomeArquivoEndereco(abrir.FileName));
                            pbImagem.ImageLocation = enderecoPasta + Midia.nomeArquivoEndereco(abrir.FileName);

                            status = true;

                            MessageBox.Show("Imagem alterada com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Foram encontrados problemas ao alterar a imagem da doença.\nErro: " + doenca.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
    }
}
