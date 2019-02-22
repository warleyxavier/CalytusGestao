using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalytusGestao.Model.Classes;
using System.Data.SQLite;

namespace CalytusGestao.Model.ClassesBD
{
    public class LocalBD : Controle
    {
        // *****************************************************************
        // ***************** DECLARAÇÃO DOS OBJETOS   **********************
        // *****************************************************************

        Conexao server = new Conexao();

        // *****************************************************************
        // ***********     CONSULTA LOCAIS ==> DOENÇA     ******************
        // *****************************************************************

        public Local[] consultaLocais(Local local)
        {
            Local[] locais = null;

            try
            {
                conexao = server.conexao();

                string sql = "select count(locCodigo) from Local where locLocal like @Pesquisa;";
                 
                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Pesquisa", "%" + local.gsLocal + "%");

                int quantidade = Convert.ToInt32(command.ExecuteScalar());

                conexao.Close();

                if(quantidade > 0)
                {
                    locais = new Local[quantidade];

                    conexao = server.conexao();
                    sql = "select * from Local where locLocal like @Pesquisa order by locLocal;";

                    command = server.command(sql, conexao);
                    command.Parameters.AddWithValue("@Pesquisa", "%" + local.gsLocal + "%");

                    leitor = command.ExecuteReader();

                    int contador = 0;

                    while(leitor.Read())
                    {
                        locais[contador] = new Local();

                        locais[contador].gsCodigo = Convert.ToInt32(leitor["locCodigo"]);
                        locais[contador].gsLocal = leitor["locLocal"].ToString();

                        contador++;
                    }

                    dados = true;
                }

                base.resultado = true;
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

            return locais;
        }

        // *****************************************************************
        // ***********     CADASTRA LOCAIS ==> DOENÇA     ******************
        // *****************************************************************

        public void cadastraLocaisDoenca(Doenca doenca, int[] codLocais)
        {
            try
            {
                conexao = server.conexao();

                StringBuilder sql = new StringBuilder();

                for(int i = 0; i < codLocais.Length; i++)
                {
                    sql.AppendLine("insert into locDoenca(lodDoenca, lodLocal) values(" + doenca.gsCodigo +  ", " + codLocais[i] + ");");
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
        // ***********     CONSULTA LOCAIS ==> DOENÇA     ******************
        // *****************************************************************

        public SQLiteDataReader consultaLocaisDoenca(Doenca doenca)
        {
            try
            {
                conexao = server.conexao();

                string sql = "select lodCodigo, locCodigo, locLocal from Local inner join locDoenca on locCodigo = lodLocal where lodDoenca = @codDoenca;";

                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@codDoenca", doenca.gsCodigo);

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
        // ***********     REMOVE LOCAIS ==> DOENÇA     ********************
        // *****************************************************************

        public void removeLocaisDoenca(int codigo)
        {
            try
            {
                conexao = server.conexao();

                string sql = "delete from locDoenca where lodCodigo = @Codigo;";

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
