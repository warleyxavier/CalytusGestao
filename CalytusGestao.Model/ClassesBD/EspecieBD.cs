using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalytusGestao.Model.Classes;
using System.Data.SQLite;

using System.Windows.Forms;

namespace CalytusGestao.Model.ClassesBD
{
    public class EspecieBD : Controle
    {
        Conexao server = new Conexao();

        // *****************************************************************
        // *************       CADASTRO DE ESPÉCIE       *******************
        // *****************************************************************

        public int cadastraEspecie(Especie especie)
        {
            int codEspecie = 0;
            try
            {
                conexao = server.conexao();
                string sql = "insert into Especie(espNomeCientifico, espNomePopular, espCaracteristicas, espImagem) values (Upper(@NomeCientifico), Upper(@NomePopular), Upper(@Caracteristicas), @Imagem);";
                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@NomeCientifico", especie.gsNomeCientifico.ToUpper());
                command.Parameters.AddWithValue("@NomePopular", especie.gsNomePopular.ToUpper());
                command.Parameters.AddWithValue("@Caracteristicas", especie.gsCaractesristicas.ToUpper());
                command.Parameters.AddWithValue("@Imagem", (!especie.gsImagem.Equals("") ? especie.gsImagem : null));
                command.ExecuteNonQuery();

                conexao.Close();

                conexao = server.conexao();
                sql = "select max(espCodigo) from Especie;";
                command = server.command(sql, conexao);
                codEspecie = Convert.ToInt32(command.ExecuteScalar());

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
            return codEspecie;
        }

        // *****************************************************************
        // *************     CONSULTA ESPÉCIES ==> NOME  *******************
        // *****************************************************************

        public Especie[] consultaEspecies(Especie especie)
        {
            Especie[] especies = null;
            try
            {
                conexao = server.conexao();
                string sql = "select count(espCodigo) from Especie where espNomeCientifico like @Pesquisa or espNomePopular like @Pesquisa";
                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Pesquisa", "%" + especie.gsNomePopular.ToUpper() + "%");
                int quantidade = Convert.ToInt32(command.ExecuteScalar());

                if(quantidade > 0)
                {
                    conexao.Close();
                    especies = new Especie[quantidade];

                    conexao = server.conexao();
                    sql = "select * from Especie where espNomeCientifico like @Pesquisa or espNomePopular like @Pesquisa order by espNomePopular";
                    command = server.command(sql, conexao);
                    command.Parameters.AddWithValue("@Pesquisa", "%" + especie.gsNomePopular.ToUpper() + "%");
                    leitor = command.ExecuteReader();

                    if(leitor.HasRows)
                    {
                        int contador = 0;

                        while(leitor.Read())
                        {
                            especies[contador] = new Especie();

                            especies[contador].gsCodigo = Convert.ToInt32(leitor["espCodigo"]);
                            especies[contador].gsNomePopular = leitor["espNomePopular"].ToString();
                            especies[contador].gsNomeCientifico = leitor["espNomeCientifico"].ToString();
                            especies[contador].gsCaractesristicas = leitor["espCaracteristicas"].ToString();
                            especies[contador].gsImagem = leitor["espImagem"].ToString();

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
            return especies;
        }

        // *****************************************************************
        // *************     REMOVE ESPÉCIES ==> CÓDIGO  *******************
        // *****************************************************************

        public void removeEspecie(Especie especie)
        {
            try
            {
                conexao = server.conexao();
                string sql = "pragma foreign_keys = on; delete from Especie where espCodigo = @Codigo;";
                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Codigo", especie.gsCodigo);
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
        // *************    CONSULTA ESPÉCIE ==> CÓDIGO  *******************
        // *****************************************************************

        public Especie consultaEspecie(Especie especieCod)
        {
            Especie especie = null;
            try
            {
                conexao = server.conexao();
                string sql = "select * from Especie where espCodigo = @Codigo;";
                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Codigo", especieCod.gsCodigo);
                leitor = command.ExecuteReader();

                if (leitor.HasRows)
                {
                    especie = new Especie();

                    especie.gsCodigo = Convert.ToInt32(leitor["espCodigo"]);
                    especie.gsNomePopular = leitor["espNomePopular"].ToString();
                    especie.gsNomeCientifico = leitor["espNomeCientifico"].ToString();
                    especie.gsCaractesristicas = leitor["espCaracteristicas"].ToString();
                    especie.gsImagem = leitor["espImagem"].ToString();

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
            return especie;
        }

        // *****************************************************************
        // *************    ALTERAR ESPÉCIE ==> CÓDIGO  ********************
        // *****************************************************************

        public void alterarEspecie(Especie especie)
        {
            try
            {
                conexao = server.conexao();
                string sql = "Update Especie set espNomePopular = Upper(@NomePopular), espNomeCientifico = Upper(@NomeCientifico), espCaracteristicas = Upper(@Caracteristicas) where espCodigo = @Codigo;";
                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@NomePopular", especie.gsNomePopular.ToUpper());
                command.Parameters.AddWithValue("@NomeCientifico", especie.gsNomeCientifico.ToUpper());
                command.Parameters.AddWithValue("@Caracteristicas", especie.gsCaractesristicas.ToUpper());
                command.Parameters.AddWithValue("@Codigo", especie.gsCodigo);
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
        // *********    ALTERAR IMAGEM DA ESPÉCIE ==> CÓDIGO  **************
        // *****************************************************************

        public void alteraImagemEsp(Especie especie)
        {
            try
            {
                conexao = server.conexao();
                string sql = "Update Especie set espImagem = @Imagem where espCodigo = @Codigo;";
                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Imagem", especie.gsImagem);
                command.Parameters.AddWithValue("@Codigo", especie.gsCodigo);
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
