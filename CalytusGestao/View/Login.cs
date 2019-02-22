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
using System.Data.SQLite;

namespace CalytusGestao.View
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            execLogin();
        }

        private bool checaCampos()
        {
            if (tbUsuario.Text.Trim().Equals("") || tbSenha.Text.Trim().Equals(""))
            {
                return false;
            }
            return true;
        }

        private void execLogin()
        {
            if(checaCampos())
            {
                UsuarioContr usuario = new UsuarioContr();

                SQLiteDataReader leitor = usuario.retornaUsuario(tbUsuario.Text.Trim(), tbSenha.Text.Trim());

                if (usuario.getResultado())
                {
                    if (leitor.HasRows)
                    {
                        limpaCampos();
                        this.Visible = false;

                        int codigo = Convert.ToInt32(leitor["usuCodigo"]);
                        string nome = leitor["usuNome"].ToString();
                        string tipo = leitor["usuTipo"].ToString();

                        leitor.Dispose();

                        MenuPrincipal menu = new MenuPrincipal(codigo, nome , tipo);
                        menu.ShowDialog();

                        if(menu.status)
                        {
                            this.Visible = true;
                            tbUsuario.Focus();
                        }
                        else
                        {
                            Application.Exit();
                        }
                    }
                    else
                    {
                        MessageBox.Show("O usuário " + tbUsuario.Text.Trim() + " não foi encontrado na base de dados. \nVerifique se todos os campos foram preenchidos corretamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Erro: " + usuario.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Verifique se todos os campos foram preenchidos corretamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tbUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode.Equals(Keys.Enter))
            {
                execLogin();
            }
        }

        private void limpaCampos()
        {
            tbUsuario.Text = "";
            tbSenha.Text = "";
        }
    }
}
