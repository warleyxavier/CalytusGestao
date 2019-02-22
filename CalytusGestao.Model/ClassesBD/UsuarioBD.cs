using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using CalytusGestao.Model.Classes;
using CalytusGestao.Model.Helpers;

namespace CalytusGestao.Model.ClassesBD
{
    public class UsuarioBD
    {
        Conexao server = new Conexao();

        // *****************************************************************
        // ***************** DECLARAÇÃO DOS ATRIBUTOS **********************
        // *****************************************************************

        private string erro = null;
        private bool resultado;

        // *****************************************************************
        // *********************** PROPRIEDADES ****************************
        // *****************************************************************

        public string getErro()
        {
            return this.erro;
        }

        public bool getResultado()
        {
            return this.resultado;
        }

        // *****************************************************************
        // *********** CONSULTA USUÁRIO --> USUÁRIO E SENHA ****************
        // *****************************************************************

        public SQLiteDataReader consultUsuario(Usuario usuario)
        {
            SQLiteDataReader leitor = null;
            SQLiteConnection conexao = null;

            try
            {
                conexao = server.conexao();

                string sql = "select * from Usuario where usuUsuario = @usuario and usuSenha = @senha;";

                SQLiteCommand command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@usuario", usuario.gsUsuario);
                command.Parameters.AddWithValue("@senha", Helper.criptografia(usuario.gsSenha));

                leitor = command.ExecuteReader();
                this.resultado = true;
            }
            catch(SQLiteException e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }

            return leitor;
            conexao.Close();
        }

        // *****************************************************************
        // ******************** CADASTRA USUÁRIO ***************************
        // *****************************************************************

        public void cadastraUsuario(Usuario usuario)
        {
            SQLiteConnection conexao = null;

            try
            {
                conexao = server.conexao();

                string sql = "insert into Usuario(usuNome, usuUsuario, usuSenha, usuTipo) values (UPPER(@Nome), @Usuario, @Senha, @Tipo);";

                SQLiteCommand command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Nome", usuario.gsNome.ToUpper());
                command.Parameters.AddWithValue("@Usuario", usuario.gsUsuario);
                command.Parameters.AddWithValue("@Senha", Helper.criptografia(usuario.gsSenha));
                command.Parameters.AddWithValue("@Tipo", usuario.gsTipo);

                command.ExecuteNonQuery();

                resultado = true;
            }
            catch (SQLiteException e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }
            finally
            {
                conexao.Close();
            }
        }

        // *****************************************************************
        // ***********    CONSULTA USUÁRIO --> NOME         ****************
        // *****************************************************************

        public SQLiteDataReader consultaUsuarioNome(Usuario usuario)
        {
            SQLiteDataReader leitor = null;
            SQLiteConnection conexao = null;

            try
            {
                conexao = server.conexao();

                string sql = "select * from Usuario where usuNome like @nome order by usuNome;";

                SQLiteCommand command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@nome", "%" + usuario.gsNome.ToUpper() + "%");

                leitor = command.ExecuteReader();

                resultado = true;
            }
            catch (SQLiteException e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }
            
            return leitor;
            conexao.Close();
        }

        // *****************************************************************
        // *****************  REMOVE USUÁRIO - CÓDIGO **********************
        // *****************************************************************

        public void removeUsuario(Usuario usuario)
        {
            SQLiteConnection conexao = null;

            try
            {
                conexao = server.conexao();

                string sql = "delete from Usuario where usuCodigo = @Codigo;";

                SQLiteCommand command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Codigo", usuario.gsCodigo);
                    
                command.ExecuteNonQuery();

                resultado = true;
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }
            finally
            {
                conexao.Close();
            }
        }

        // *****************************************************************
        // ***********       CONSULTA USUÁRIO --> CÓDIGO    ****************
        // *****************************************************************

        public Usuario getUsuario(Usuario usuarioCod)
        {
            SQLiteDataReader leitor = null;
            SQLiteConnection conexao = null;

            Usuario usuario = null;

            try
            {
                conexao = server.conexao();

                string sql = "select * from Usuario where usuCodigo = @Codigo;";

                SQLiteCommand command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Codigo", usuarioCod.gsCodigo);

                leitor = command.ExecuteReader();

                if(leitor.HasRows)
                {
                    usuario = new Usuario();
                    usuario.gsNome = leitor["usuNome"].ToString();
                    usuario.gsUsuario = leitor["usuUsuario"].ToString();
                    usuario.gsTipo = leitor["usuTipo"].ToString();
                }

                leitor.Dispose();

                resultado = true;
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }
            finally
            {
                conexao.Close();
            }

            return usuario;
        }

        // *****************************************************************
        // ***********             ALTERA USUÁRIO           ****************
        // *****************************************************************

        public void atualizaUsuario(Usuario usuario)
        {
            SQLiteConnection conexao = null;

            try
            {
                conexao = server.conexao();

                string sql = "update Usuario set usuNome = Upper(@Nome), usuUsuario = @Usuario, usuSenha = @Senha, usuTipo = @Tipo where usuCodigo = @Codigo;";

                SQLiteCommand command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Nome", usuario.gsNome.ToUpper());
                command.Parameters.AddWithValue("@Usuario", usuario.gsUsuario);
                command.Parameters.AddWithValue("@Senha", Helper.criptografia(usuario.gsSenha));
                command.Parameters.AddWithValue("@Tipo", usuario.gsTipo);
                command.Parameters.AddWithValue("@Codigo", usuario.gsCodigo);

                command.ExecuteNonQuery();

                this.resultado = true;
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }
            finally
            {
                conexao.Close();
            }
        }

        // *****************************************************************
        // **********       ALTERAÇÃO DE USUÁRIO -> SENHA    ***************
        // *****************************************************************

        public void alteraUsuarioSenha(Usuario usuario, string senhaAntiga)
        {
            SQLiteConnection conexao = null;

            try
            {
                conexao = server.conexao();

                string sql = "select usuSenha from Usuario where usuCodigo = @Codigo;";

                SQLiteCommand command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Codigo", usuario.gsCodigo);

                string senha = command.ExecuteScalar().ToString();

                conexao.Close();

                if (senha.Equals(Helper.criptografia(senhaAntiga)))
                {
                    conexao = server.conexao();

                    sql = "update Usuario set usuSenha = @Senha where usuCodigo = @Codigo; ";

                    command = server.command(sql, conexao);
                    command.Parameters.AddWithValue("@Senha", Helper.criptografia(usuario.gsSenha));
                    command.Parameters.AddWithValue("@Codigo", usuario.gsCodigo);

                    command.ExecuteNonQuery();

                    resultado = true;
                }
                else
                {
                    resultado = false;
                    erro = "A senha antiga não coincide com a presente na base de dados.\nCertifique-se que a digitou corretamente." ;
                }
                 
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }
            finally
            {
                conexao.Close();
            }
        }

    }
}
