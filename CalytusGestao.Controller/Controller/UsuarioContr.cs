using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using CalytusGestao.Model.Classes;
using CalytusGestao.Model.ClassesBD;

namespace CalytusGestao.Controller.Controller
{
    public class UsuarioContr
    {
        Usuario usuario = null;
        UsuarioBD usuarioBD = null;

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

        public void verificaErro()
        {
            if (!usuarioBD.getResultado())
            {
                this.resultado = false;
                this.erro = usuarioBD.getErro();
            }
        }

        // *****************************************************************
        // *********** CONSULTA USUÁRIO --> USUÁRIO E SENHA ****************
        // *****************************************************************

        public SQLiteDataReader retornaUsuario(string user, string senha)
        {
            SQLiteDataReader leitor = null;

            try
            {
                usuario = new Usuario();
                usuario.gsUsuario = user;
                usuario.gsSenha = senha;

                usuarioBD = new UsuarioBD();
                leitor = usuarioBD.consultUsuario(usuario);

                this.resultado = true;

                verificaErro();
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }

            return leitor;
        }

        // *****************************************************************
        // ******************** CADASTRA USUÁRIO ***************************
        // *****************************************************************
        
        public void cadatraUsuario(string nome, string user, string tipo, string senha)
        {
            try
            {
                usuario = new Usuario();
                usuario.gsNome = nome;
                usuario.gsUsuario = user;
                usuario.gsTipo = tipo;
                usuario.gsSenha = senha;

                usuarioBD = new UsuarioBD();
                usuarioBD.cadastraUsuario(usuario);

                this.resultado = true;

                verificaErro();
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }
        }

        // *****************************************************************
        // **************  RETORNA UMA LISTA DE USUÁRIOS *******************
        // *****************************************************************

        public SQLiteDataReader consultaUsuarios(string nome)
        {
            SQLiteDataReader leitor = null;

            try
            {
                usuario = new Usuario();
                usuario.gsNome = nome;

                usuarioBD = new UsuarioBD();
                leitor = usuarioBD.consultaUsuarioNome(usuario);

                resultado = true;

                verificaErro();
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }

            return leitor;
        }

        // *****************************************************************
        // *****************  REMOVE USUÁRIO - CÓDIGO **********************
        // *****************************************************************

        public void removeUsuario(int codigo)
        {
            try
            {
                usuario = new Usuario();
                usuario.gsCodigo = codigo;

                usuarioBD = new UsuarioBD();
                usuarioBD.removeUsuario(usuario);

                this.resultado = true;

                verificaErro();
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }
        }

        // *****************************************************************
        // ***********       CONSULTA USUÁRIO --> CÓDIGO    ****************
        // *****************************************************************

        public Usuario getUsuario(int codigo)
        {
            try
            {
                usuario = new Usuario();
                usuario.gsCodigo = codigo;

                usuarioBD = new UsuarioBD();
                usuario = usuarioBD.getUsuario(usuario);

                resultado = true;

                verificaErro();
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }

            return usuario;
        }

        // *****************************************************************
        // ***********             ALTERA USUÁRIO           ****************
        // *****************************************************************

        public void atualizaUsuario(int codigo, string nome, string user, string senha, string tipo)
        {
            try
            {
                usuario = new Usuario();
                usuario.gsCodigo = codigo;
                usuario.gsNome = nome;
                usuario.gsUsuario = user;
                usuario.gsSenha = senha;
                usuario.gsTipo = tipo;

                usuarioBD = new UsuarioBD();
                usuarioBD.atualizaUsuario(usuario);

                this.resultado = true;

                verificaErro();
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }
        }

        // *****************************************************************
        // **********       ALTERAÇÃO DE USUÁRIO -> SENHA    ***************
        // *****************************************************************

        public void alteraUsuarioSenha(int codigo, string senhaAntiga, string novaSenha)
        {
            try
            {
                usuario = new Usuario();
                usuario.gsCodigo = codigo;
                usuario.gsSenha = novaSenha;

                usuarioBD = new UsuarioBD();
                usuarioBD.alteraUsuarioSenha(usuario, senhaAntiga);

                this.resultado = true;

                verificaErro();
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }
        }
    }
}
