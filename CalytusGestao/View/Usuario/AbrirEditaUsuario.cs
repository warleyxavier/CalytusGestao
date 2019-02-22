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

namespace CalytusGestao.View.Usuario
{
    public partial class AbrirEditaUsuario : Form
    {
        public bool status = false;

        private int codUsuario;

        // *****************************************************************
        // ***************** DECLARAÇÃO DOS ATRIBUTOS **********************
        // *****************************************************************

        private string nome;
        private string usuario;
        private string tipo;


        public AbrirEditaUsuario(int codigo, string modo)
        {
            InitializeComponent();

            this.codUsuario = codigo;

            setTitulo(modo);

            carregaCampos();

            carregaVariaveis();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void setTitulo(string tipo)
        {
            if(tipo.Equals("Abrir"))
            {
                lbTitulo.Text = "INFORMAÇÕES DO USUÁRIO";
                btnCancelar.Visible = false;
                btnSalvar.Visible = false;
                visibilidade(false);
            }
            else
            {
                lbTitulo.Text = "EDIÇÃO DO USUÁRIO";
                btnEditar.Visible = false;
                visibilidade(true);
            }
        }

        private void carregaCampos()
        {
            UsuarioContr usuarioCtr = new UsuarioContr();

            Model.Classes.Usuario usuario = usuarioCtr.getUsuario(this.codUsuario);


            // ************************ CARREGANDO CAMPOS **************************************

            tbCodigo.Text = codUsuario.ToString();
            tbNome.Text = usuario.gsNome;
            tbUsuario.Text = usuario.gsUsuario;
            cbTipo.Text = usuario.gsTipo;
        }

        private void carregaVariaveis()
        {
            this.nome = tbNome.Text.Trim();
            this.usuario = tbUsuario.Text.Trim();
            this.tipo = cbTipo.Text.Trim();
        }

        private void descarregaVariaveis()
        {
            tbNome.Text = this.nome;
            tbUsuario.Text = this.usuario;
            cbTipo.Text = this.tipo;
        }

        private void visibilidade(bool estado)
        {
            tbNome.Enabled = estado;
            tbUsuario.Enabled = estado;
            cbTipo.Enabled = estado;

            gbSenha.Visible = estado;

            btnCancelar.Visible = estado;
            btnEditar.Visible = !estado;
            btnSalvar.Visible = estado;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            descarregaVariaveis();
            visibilidade(false);
            lbTitulo.Text = "INFORMAÇÕES DO USUÁRIO";
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            carregaVariaveis();
            visibilidade(true);
            lbTitulo.Text = "EDIÇÃO DO USUÁRIO";
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            execSalvar();
        }

        private void execSalvar()
        {
            if (checaCampos())
            {
                if (tbSenha1.Text.Equals(tbSenha2.Text))
                {
                    UsuarioContr usuario = new UsuarioContr();
                    usuario.atualizaUsuario(this.codUsuario, tbNome.Text.Trim(), tbUsuario.Text.Trim(), tbSenha1.Text, cbTipo.Text);

                    if(usuario.getResultado())
                    {
                        MessageBox.Show("Usuário " + tbNome.Text.Trim() + " alterado com sucesso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        visibilidade(false);
                        lbTitulo.Text = "INFORMAÇÕES DO USUÁRIO";

                        this.status = true;
                    }
                    else
                    {
                        MessageBox.Show("Erro ao atualizar o usuário " + this.nome + "\nErro: " + usuario.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }                                
                }
                else
                {
                    MessageBox.Show("As senhas não coincidem.\nVerifique se foram digitadas corretamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("É necessário que haja o preenchimento de todos os campos\nVerifique se todos estão preenchidos corretamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool checaCampos()
        {
            if (tbNome.Text.Equals("") || tbUsuario.Text.Equals("") || cbTipo.Text.Equals("") || tbSenha1.Text.Equals("") || tbSenha2.Text.Equals(""))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void tbNome_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode.Equals(Keys.Enter))
            {
                execSalvar();
            }
        }
    }
}
