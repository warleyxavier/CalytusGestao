using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalytusGestao.Model.Classes;
using System.Data.SQLite;

namespace CalytusGestao.Model.ClassesBD
{
    public class DoencaBD : Controle
    {
        Conexao server = new Conexao();
        
        // *****************************************************************
        // *************   CADASTRO DE DOENÇA   ==> NOME *******************
        // *****************************************************************

        public int cadastroDoenca(Doenca doenca)
        {
            int codigoDoenca = 0;

            try
            {
                conexao = server.conexao();

                string sql = "insert into Doenca(doeNome, doeCaracteristicas, doeDiagnostico, doeImagem) values (Upper(@Nome), Upper(@Caracteristicas), Upper(@Diagnostico), @Imagem);";

                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Nome", doenca.gsNome);
                command.Parameters.AddWithValue("@Caracteristicas", doenca.gsCaracteristicas);
                command.Parameters.AddWithValue("@Diagnostico", doenca.gsDiagnostico);
                command.Parameters.AddWithValue("@Imagem", (!doenca.gsImagem.Equals("") ? doenca.gsImagem : null));

                command.ExecuteNonQuery();

                conexao.Close();

                conexao = server.conexao();

                sql = "select max(doeCodigo) from Doenca";

                command = server.command(sql, conexao);

                codigoDoenca = Convert.ToInt32(command.ExecuteScalar());                

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

            return codigoDoenca;
        }

        // *****************************************************************
        // ***********   CONSULTA DE DOENÇAS =>  NOME   ********************
        // *****************************************************************

        public Doenca[] consultaDoencasNome(Doenca doenca)
        {
            Doenca[] doencas = null;

            try
            {
                conexao = server.conexao();

                string sql = "select count(doeCodigo) from Doenca where doeNome like @Pesquisa; ";

                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Pesquisa", "%" + doenca.gsNome + "%");

                int quantidade = Convert.ToInt32(command.ExecuteScalar());

                conexao.Close();

                if(quantidade > 0)
                {
                    this.dados = true;

                    doencas = new Doenca[quantidade];

                    conexao = server.conexao();

                    sql = "select * from Doenca where doeNome like @Pesquisa order by doeNome";

                    command = server.command(sql, conexao);
                    command.Parameters.AddWithValue("@Pesquisa", "%" + doenca.gsNome + "%");

                    leitor = command.ExecuteReader();

                    int contador = 0;

                    while(leitor.Read())
                    {
                        doencas[contador] = new Doenca();

                        doencas[contador].gsCodigo = Convert.ToInt32(leitor["doeCodigo"]);
                        doencas[contador].gsNome = leitor["doeNome"].ToString();
                        doencas[contador].gsCaracteristicas = leitor["doeCaracteristicas"].ToString();
                        doencas[contador].gsDiagnostico = leitor["doeDiagnostico"].ToString();
                        doencas[contador].gsImagem = leitor["doeImagem"].ToString();

                        contador++;
                    }
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

            return doencas;
        }

        // *****************************************************************
        // ***********   REMOVE DOENÇA ==> CÓDIGO       ********************
        // *****************************************************************

        public void removeDoenca(Doenca doenca)
        {
            conexao = null;

            try
            {
                conexao = server.conexao();

                string sql = "pragma foreign_keys = on; delete from Doenca where doeCodigo = @Codigo;";

                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Codigo", doenca.gsCodigo);

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
        // ***********   CONSULTA DOENÇA ==> CÓDIGO       ******************
        // *****************************************************************

        public Doenca consultaDoenca(Doenca doencaCodigo)
        {
            Doenca doenca = null;

            try
            {
                conexao = server.conexao();

                string sql = "select * from Doenca where doeCodigo = @Codigo;";

                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Codigo", doencaCodigo.gsCodigo);

                leitor = command.ExecuteReader();

                if(leitor.HasRows)
                {
                    doenca = new Doenca();

                    doenca.gsCodigo = doencaCodigo.gsCodigo;
                    doenca.gsNome = leitor["doeNome"].ToString();
                    doenca.gsCaracteristicas = leitor["doeCaracteristicas"].ToString();
                    doenca.gsDiagnostico = leitor["doeDiagnostico"].ToString();
                    doenca.gsImagem = leitor["doeImagem"].ToString();
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

            return doenca;
        }

        // *****************************************************************
        // ***********            ALTERA DOENÇA           ******************
        // *****************************************************************

        public void alteraDoenca(Doenca doenca)
        {
            try
            {
                conexao = server.conexao();

                string sql = "Update Doenca set doeNome = Upper(@Nome), doeCaracteristicas = Upper(@Caracteristicas), doeDiagnostico = Upper(@Diagnostico) where doeCodigo = @Codigo;";

                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Nome", doenca.gsNome);
                command.Parameters.AddWithValue("@Caracteristicas", doenca.gsCaracteristicas);
                command.Parameters.AddWithValue("@Diagnostico", doenca.gsDiagnostico);
                command.Parameters.AddWithValue("@Codigo", doenca.gsCodigo);

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
        // *************   CADASTRO DE DOENÇA   ==> NOME *******************
        // *****************************************************************

        public Doenca[] consultaDoencasLocal(Local local)
        {
            Doenca[] doencas = null;

            try
            {
                conexao = server.conexao();
                string sql = "select count(doeCodigo) from Doenca inner join locDoenca on doeCodigo = lodDoenca inner join Local on locCodigo = lodLocal where locLocal = @Pesquisa;";

                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Pesquisa", local.gsLocal);
                int quantidade = Convert.ToInt32(command.ExecuteScalar());

                conexao.Close();

                if(quantidade > 0)
                {
                    doencas = new Doenca[quantidade];

                    conexao = server.conexao();

                    sql = "select doeCodigo, doeNome, doeImagem from Doenca inner join locDoenca on doeCodigo = lodDoenca inner join Local on locCodigo = lodLocal where locLocal = @Pesquisa;";

                    command = server.command(sql, conexao);
                    command.Parameters.AddWithValue("@Pesquisa", local.gsLocal);
                    leitor = command.ExecuteReader();

                    int contador = 0;

                    while(leitor.Read())
                    {
                        doencas[contador] = new Doenca();

                        doencas[contador].gsCodigo = Convert.ToInt32(leitor["doeCodigo"]);
                        doencas[contador].gsNome = leitor["doeNome"].ToString();
                        doencas[contador].gsImagem = leitor["doeImagem"].ToString();

                        contador++;
                    }

                    dados = true;
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

            return doencas;
        }

        // *****************************************************************
        // ***********     ALTERA IMAGEM DA DOENÇA        ******************
        // *****************************************************************

        public void alteraImgDoenca(Doenca doenca)
        {
            try
            {
                conexao = server.conexao();
                string sql = "Update Doenca set doeImagem = @Imagem where doeCodigo = @Codigo;";
                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Imagem", doenca.gsImagem);
                command.Parameters.AddWithValue("@Codigo", doenca.gsCodigo);
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
