using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalytusGestao.Model.Classes;
using System.Data.SQLite;

namespace CalytusGestao.Model.ClassesBD
{
    public class UtilidadeBD : Controle
    {
        Conexao server = new Conexao();

        // *****************************************************************
        // ****************   CONSULTA UTILIDADES        *******************
        // *****************************************************************

        public Utilidade[] consultaUtilidades(Utilidade utilidade)
        {
            Utilidade[] codsUtilidade = null;

            try
            {
                conexao = server.conexao();
                string sql = "select count(utiCodigo) from Utilidade where utiUtilidade like @Pesquisa;";
                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Pesquisa", "%" + utilidade.gsUtilidade + "%");
                int quantidade = Convert.ToInt32(command.ExecuteScalar());

                if(quantidade > 0)
                {
                    conexao.Close();

                    conexao = server.conexao();
                    sql = "select * from Utilidade where utiUtilidade like @Pesquisa order by utiUtilidade;";
                    command = server.command(sql, conexao);
                    command.Parameters.AddWithValue("@Pesquisa", "%" + utilidade.gsUtilidade + "%");
                    leitor = command.ExecuteReader();

                    if(leitor.HasRows)
                    {
                        codsUtilidade = new Utilidade[quantidade];
                        int contador = 0;

                        while(leitor.Read())
                        {
                            codsUtilidade[contador] = new Utilidade();
                            codsUtilidade[contador].gsCodigo = Convert.ToInt32(leitor["utiCodigo"]);
                            codsUtilidade[contador].gsUtilidade = leitor["utiUtilidade"].ToString();

                            contador++;
                        }

                        dados = true;
                    }
                }

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

            return codsUtilidade;
        }

        // *****************************************************************
        // ***********    CADASTRO UTILIDADES ==> ESPÉCIE  *****************
        // *****************************************************************

        public void cadastraUtilidadesEspecie(Especie especie, int[] codUtilidades)
        {
            try
            {
                conexao = server.conexao();
                StringBuilder sql = new StringBuilder();

                for(int i = 0; i < codUtilidades.Length; i++)
                {
                    sql.AppendLine("insert into UtilEspecie(utilEsEspecie, utilEsUtilidade) values (" + especie.gsCodigo + "," + codUtilidades[i] + "); ");
                }

                command = server.command(sql.ToString(), conexao);
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
        // ***********    CONSULTA UTILIDADES ==> ESPÉCIE  *****************
        // *****************************************************************

        public SQLiteDataReader consultaUtilidadesEspecie(Especie especie)
        {
            try
            {
                conexao = server.conexao();
                string sql = "select utilEsCodigo, utilEsEspecie, utilEsUtilidade, utiUtilidade from UtilEspecie inner join Utilidade on utilEsUtilidade = utiCodigo where utilEsEspecie = @Especie;";
                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Especie", especie.gsCodigo);
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
        // ***********    REMOVE   UTILIDADES ==> DOENÇA   *****************
        // *****************************************************************

        public void removeUtilidadeEspecie(int codigo)
        {
            try
            {
                conexao = server.conexao();
                string sql = "delete from UtilEspecie where utilEsCodigo = @Codigo;";
                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Codigo", codigo);
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
    }
}
