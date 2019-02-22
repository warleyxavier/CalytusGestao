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

namespace CalytusGestao.View.Usuario
{
    public partial class ConsultaUsuario : Form
    {
        public ConsultaUsuario()
        {
            InitializeComponent();
        }

        private void ConsultaUsuario_Load(object sender, EventArgs e)
        {
            carregaGrid("");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void carregaGrid(string pesquisa)
        {
            SQLiteDataReader leitor = null;

            try
            {
                dgUsuario.Rows.Clear();

                UsuarioContr usuario = new UsuarioContr();
                leitor = usuario.consultaUsuarios(pesquisa);

                if (usuario.getResultado())
                {
                    while (leitor.Read())
                    {
                        dgUsuario.Rows.Add(leitor["usuCodigo"], leitor["usuNome"], leitor["usuUsuario"], leitor["usuTipo"]);
                    }
                }
                else
                {
                    MessageBox.Show("Erro: " + usuario.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                leitor.Dispose();
            }
            catch(Exception e)
            {
                MessageBox.Show("Erro: " + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            carregaGrid("");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            carregaGrid(tbPesquisa.Text.Trim());
        }

        private void tbPesquisa_TextChanged(object sender, EventArgs e)
        {
            if(tbPesquisa.Text.Length % 5 == 0)
            {
                carregaGrid(tbPesquisa.Text.Trim());
            }
        }

        // *****************************************************************
        // *****************  REMOVE USUÁRIO - CÓDIGO **********************
        // *****************************************************************

        private void removeUsuario()
        {
            DialogResult resposta = MessageBox.Show("Realmente deseja remover o usuário " + dgUsuario.SelectedCells[1].Value + "?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resposta.Equals(DialogResult.Yes))
            {
                UsuarioContr usuario = new UsuarioContr();
                usuario.removeUsuario(Convert.ToInt32(dgUsuario.SelectedCells[0].Value));

                if (!usuario.getResultado())
                {
                    MessageBox.Show("Erro ao tentar remover o usuário " + dgUsuario.SelectedCells[1].Value + "\nErro: " + usuario.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                carregaGrid("");
            }        
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgUsuario.RowCount > 0)
            {
                removeUsuario();
            }
            else
            {
                MessageBox.Show("É necessário que haja um usuário selecionado para realizar a remoção", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // *****************************************************************
        // *****************      ABRE USUÁRIO        **********************
        // *****************************************************************

        private void execAbrir(string tipo)
        {
            if(dgUsuario.RowCount > 0)
            {
                AbrirEditaUsuario abrir = new AbrirEditaUsuario(Convert.ToInt32(dgUsuario.SelectedCells[0].Value), tipo);
                abrir.ShowDialog();

                if(abrir.status)
                {
                    carregaGrid("");
                }

                abrir.Dispose();
            }
            else
            {
                MessageBox.Show("É necessário que haja um usuário selecionado para que a função seja executada!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            execAbrir("Abrir");
        }

        private void dgUsuario_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            execAbrir("Abrir");
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            execAbrir("Abrir");
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            execAbrir("Editar");
        }

        private void dgUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode.Equals(Keys.Enter))
            {
                execAbrir("Abrir");
            }
        }
    }
}
