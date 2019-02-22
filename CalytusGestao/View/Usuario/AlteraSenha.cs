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
    public partial class AlteraSenha : Form
    {
        private int codigo;
        private string nome;

        public AlteraSenha(int codigo, string nome)
        {
            InitializeComponent();

            this.codigo = codigo;
            this.nome = nome;

            carregaCampos();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            execSalvar();
        }

        private void carregaCampos()
        {
            tbCodigo.Text = codigo.ToString();
            tbNome.Text = nome;
        }

        private bool checaCamposVazios()
        {
            if(tbSenhaAntiga.Text.Equals("") || tbNovaSenha1.Text.Equals("") || tbNovaSenha2.Text.Equals(""))
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
                if(tbNovaSenha1.Text.Equals(tbNovaSenha2.Text))
                {
                    UsuarioContr usuario = new UsuarioContr();
                    usuario.alteraUsuarioSenha(this.codigo, tbSenhaAntiga.Text, tbNovaSenha1.Text);

                    if(usuario.getResultado())
                    {
                        MessageBox.Show("Usuário " + tbNome.Text + " alterado com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Erro ao alterar o usuário " + tbNome.Text + "\nErro: " + usuario.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("As senhas não coincidem.\nVerifique se foram digitadas corretamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("É necessário que todos os campos sejam preenchidos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning );
            }
        }

        private void tbSenhaAntiga_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode.Equals(Keys.Enter))
            {
                execSalvar();
            }
        }
    }
}
