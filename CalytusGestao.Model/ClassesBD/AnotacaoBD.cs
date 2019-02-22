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
    public class AnotacaoBD
    {
        Conexao server = new Conexao();

        // *****************************************************************
        // ***************** DECLARAÇÃO DOS ATRIBUTOS **********************
        // *****************************************************************

        private string erro = null;
        private bool resultado;
        private bool dados = false;

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

        public bool getDados()
        {
            return this.dados;
        }

        // *****************************************************************
        // ***********         CADASTRO DE ANOTAÇÃO         ****************
        // *****************************************************************

        public void cadatraAnotacao(Anotacao anotacao)
        {
            SQLiteConnection conexao = null;

            try
            {
                conexao = server.conexao();

                string sql = "insert into Anotacao(anoAssunto, anoAnotacao, anoData) values (Upper(@Assunto), Upper(@Anotacao), @Data);";

                SQLiteCommand command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Assunto", anotacao.gsAssunto.ToUpper());
                command.Parameters.AddWithValue("@Anotacao", anotacao.gsAnotacao.ToUpper());
                command.Parameters.AddWithValue("@Data", Helper.formataData(DateTime.Today.ToString()));

                command.ExecuteNonQuery();

                this.resultado = true;
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
        // ***********   CONSULTA DE ANOTAÇÕES => ASSUNTO   ****************
        // *****************************************************************

        public Anotacao[] consultaAnotacoes(Anotacao anotacao)
        {
            SQLiteConnection conexao = null;
            SQLiteDataReader leitor = null;

            Anotacao[] anotacoes = null;

            try
            {
                conexao = server.conexao();

                string sql = "select count(anoCodigo) from Anotacao where anoAssunto like @Pesquisa; ";

                SQLiteCommand command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Pesquisa", "%" + anotacao.gsAssunto.ToUpper() + "%");

                int quantidade = Convert.ToInt32(command.ExecuteScalar());

                conexao.Close();

                if (quantidade > 0)
                {
                    conexao = server.conexao();

                    sql = "select * from Anotacao where anoAssunto like @Pesquisa order by anoData desc, anoAssunto;";

                    command = server.command(sql, conexao);
                    command.Parameters.AddWithValue("@Pesquisa", "%" + anotacao.gsAssunto.ToUpper() + "%");

                    leitor = command.ExecuteReader();

                    anotacoes = new Anotacao[quantidade];

                    int contador = 0;

                    while (leitor.Read())
                    {
                         anotacoes[contador] = new Anotacao();
                         anotacoes[contador].gsCodigo = Convert.ToInt32(leitor["anoCodigo"]);
                         anotacoes[contador].gsAssunto = leitor["anoAssunto"].ToString();
                         anotacoes[contador].gsAnotacao = leitor["anoAnotacao"].ToString();
                         anotacoes[contador].gsData = leitor["anoData"].ToString().Substring(0,10);

                        contador++;                  
                    }

                    this.dados = true;
                }

                this.resultado = true;
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

            return anotacoes;
        }

        // *****************************************************************
        // ***********       REMOVE ANOTAÇÃO ==> CÓDIGO     ****************
        // *****************************************************************

        public void removeAnotacao(Anotacao anotacao)
        {
            SQLiteConnection conexao = null;

            try
            {
                conexao = server.conexao();

                string sql = "delete from Anotacao where anoCodigo = @Codigo;" ;

                SQLiteCommand command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Codigo", anotacao.gsCodigo);

                command.ExecuteNonQuery();

                this.resultado = true;
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
        // *********       CONSULTA ANOTAÇÃO ==> CÓDIGO     ****************
        // *****************************************************************

        public Anotacao consultaAnotacao(Anotacao anotacaoo)
        {
            Anotacao anotacao = null;

            SQLiteConnection conexao = null;
            SQLiteDataReader leitor = null;

            try
            {
                conexao = server.conexao();

                string sql = "select * from Anotacao where anoCodigo = @Codigo;";

                SQLiteCommand command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Codigo", anotacaoo.gsCodigo);

                leitor = command.ExecuteReader();

                if(leitor.HasRows)
                {
                    anotacao = new Anotacao();

                    anotacao.gsCodigo = Convert.ToInt32(leitor["anoCodigo"]);
                    anotacao.gsAssunto = leitor["anoAssunto"].ToString();
                    anotacao.gsAnotacao = leitor["anoAnotacao"].ToString();
                    anotacao.gsData = leitor["anoData"].ToString().Substring(0, 10);
                }

                this.resultado = true;

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

            return anotacao;
        }

        // *****************************************************************
        // *********           ATUALIZA ANOTAÇÃO            ****************
        // *****************************************************************

        public void atualizaAnotacao(Anotacao anotacao)
        {
            SQLiteConnection conexao = null;

            try
            {
                conexao = server.conexao();

                string sql = "Update Anotacao set anoAssunto = Upper(@Assunto), anoAnotacao = Upper(@Anotacao) where anoCodigo = @Codigo; ";

                SQLiteCommand command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Assunto", anotacao.gsAssunto.ToUpper());
                command.Parameters.AddWithValue("@Anotacao", anotacao.gsAnotacao.ToUpper());
                command.Parameters.AddWithValue("@Codigo", anotacao.gsCodigo);

                command.ExecuteNonQuery();

                this.resultado = true;
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
    }
}
