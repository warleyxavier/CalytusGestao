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

namespace CalytusGestao.View.Usuario
{
    public partial class CadastroUsuario : Form
    {
        public CadastroUsuario()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            limpaCampos();
        }

        private void limpaCampos()
        {
            tbNome.Text = "";
            tbUsuario.Text = "";
            cbTipo.Text = "";
            tbSenha1.Text = "";
            tbSenha2.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            execCadastro();
        }

        private void execCadastro()
        {
            if(checaCamposVazios())
            {
                if(tbSenha1.Text.Equals(tbSenha2.Text))
                {
                    UsuarioContr usuario = new UsuarioContr();
                    usuario.cadatraUsuario(tbNome.Text.Trim(), tbUsuario.Text.Trim(), cbTipo.Text, tbSenha1.Text);

                    if(usuario.getResultado())
                    {
                        MessageBox.Show("Usuário " + tbNome.Text.Trim() + " cadastrado com sucesso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        limpaCampos();
                    }
                    else
                    {
                        MessageBox.Show("Erro ao cadastrar o usuário " + tbNome.Text.Trim() + ": " + usuario.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    usuario = null;
                }
                else
                {
                    MessageBox.Show("As senhas não coincidem.\nVerifique se foram digitadas corretamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Verifique se todos os campos foram preenchidos corretamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool checaCamposVazios()
        {
            if(tbNome.Text.Trim().Equals("") || tbUsuario.Text.Trim().Equals("") || cbTipo.Text.Equals("") || tbSenha1.Text.Equals("") || tbSenha2.Text.Equals(""))
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
                execCadastro();
            }
        }
    }
}
