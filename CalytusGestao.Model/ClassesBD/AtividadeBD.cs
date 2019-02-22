using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using CalytusGestao.Model.Classes;
using CalytusGestao.Model.Helpers;
using System.Windows.Forms;

namespace CalytusGestao.Model.ClassesBD
{
    public class AtividadeBD : Controle
    {
        // *****************************************************************
        // ***************** DECLARAÇÃO DOS OBJETOS   **********************
        // *****************************************************************

        Conexao server = new Conexao();

        // *****************************************************************
        // *********** CONSULTA DE TIPOS DE ATIVIDADES   *******************
        // *****************************************************************

        public Atividade[] consultaTipos(Atividade atividade)
        {
            Atividade[] atividades = null;

            try
            {
                conexao = server.conexao();
                string sql = "Select count(tipCodigo) from Tipo where tipTipo like @Tipo order by tipTipo;";
                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Tipo", "%" + atividade.gsTipo + "%");
                int quantidade = Convert.ToInt32(command.ExecuteScalar());

                if(quantidade > 0)
                {
                    conexao.Close();

                    atividades = new Atividade[quantidade];

                    conexao = server.conexao();
                    sql = "Select * from Tipo where tipTipo like @Tipo order by tipTipo;";
                    command = server.command(sql, conexao);
                    command.Parameters.AddWithValue("@Tipo", "%" + atividade.gsTipo + "%");
                    leitor = command.ExecuteReader();

                    int contador = 0;

                    while(leitor.Read())
                    {
                        atividades[contador] = new Atividade();

                        atividades[contador].gsCodTipo = Convert.ToInt32(leitor["tipCodigo"]);
                        atividades[contador].gsTipo = leitor["tipTipo"].ToString();

                        contador++;
                    }

                    leitor.Dispose();
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

            return atividades;
        }

        // *****************************************************************
        // *********** CONSULTA CÓDIGO DE TIPO DE ATIV.  *******************
        // *****************************************************************

        public int consultaCodTipo(Atividade atividade)
        {
            int codigo = 0;

            try
            {
                conexao = server.conexao();
                string sql = "Select * from Tipo where tipTipo = @Tipo;";
                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Tipo", atividade.gsTipo);
                leitor = command.ExecuteReader();

                if(leitor.HasRows)
                {
                    while(leitor.Read())
                    {
                        codigo = Convert.ToInt32(leitor["tipCodigo"]);
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
                leitor.Dispose();
                conexao.Close();
            }

            return codigo;
        }

        // *****************************************************************
        // ***********       CADASTRO DE ATIVIDADE       *******************
        // *****************************************************************

        public void cadastroAtividade(Atividade atividade, int codPlantacao)
        {
            try
            {
                conexao = server.conexao();
                string sql = "Insert into Atividade(atiPlantacao, atiTipo, atiCustos, atiGanhos, atiData, atiRelatorio) values (@Plantacao, @Tipo, @Custos, @Ganhos, @Data, Upper(@Relatorio));";
                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Plantacao", codPlantacao);
                command.Parameters.AddWithValue("@Tipo", atividade.gsCodTipo);
                command.Parameters.AddWithValue("@Custos", atividade.gsCustos);
                command.Parameters.AddWithValue("@Ganhos", atividade.gsGanhos);
                command.Parameters.AddWithValue("@Data", Helper.formataData(atividade.gsData));
                command.Parameters.AddWithValue("@Relatorio", atividade.gsRelatorio.ToUpper());
                command.ExecuteNonQuery();                

                resultado = true;

                if(resultado)
                {
                    conexao.Close();

                    PlantacaoBD plantacao = new PlantacaoBD();
                    plantacao.atualizaCustosGanhos(codPlantacao);

                    if(plantacao.getResultado())
                    {
                        resultado = true;
                    }
                    else
                    {
                        resultado = false;
                        erro = plantacao.getErro();
                    }
                }
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
        // ***********       CONSULTA DE ATIVIDADES      *******************
        // *****************************************************************

        public Atividade[] consultaAtividades(Atividade atividade, int codPlantacao)
        {
            Atividade[] atividades = null;

            try
            {
                conexao = server.conexao();
                string sql = "Select count(atiCodigo) from Atividade inner join Tipo on atiTipo = tipCodigo where atiPlantacao = @Plantacao and tipTipo like @Pesquisa;";
                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Plantacao", codPlantacao);
                command.Parameters.AddWithValue("@Pesquisa", "%" + atividade.gsTipo + "%");
                int quantidade = Convert.ToInt32(command.ExecuteScalar());

                if(quantidade > 0)
                {
                    conexao.Close();

                    atividades = new Atividade[quantidade];

                    conexao = server.conexao();
                    sql = "Select * from Atividade inner join Tipo on atiTipo = tipCodigo where atiPlantacao = @Plantacao and tipTipo like @Pesquisa order by atiData desc, tipTipo;";
                    command = server.command(sql, conexao);
                    command.Parameters.AddWithValue("@Plantacao", codPlantacao);
                    command.Parameters.AddWithValue("@Pesquisa", "%" + atividade.gsTipo + "%");
                    leitor = command.ExecuteReader();

                    int contador = 0;

                    while(leitor.Read())
                    {
                        atividades[contador] = new Atividade();

                        atividades[contador].gsCodigo = Convert.ToInt32(leitor["atiCodigo"]);
                        atividades[contador].gsCustos = Convert.ToDouble(leitor["atiCustos"]);
                        atividades[contador].gsGanhos = Convert.ToDouble(leitor["atiGanhos"]);
                        atividades[contador].gsData = leitor["atiData"].ToString().Substring(0,10);
                        atividades[contador].gsTipo = leitor["tipTipo"].ToString();

                        contador++;
                    }

                    leitor.Dispose();
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

            return atividades;
        }

        // *****************************************************************
        // ***********         REMOÇÃO DE ATIVIDADE      *******************
        // *****************************************************************

        public void removeAtividade(Atividade atividade, int codPlantacao)
        {
            try
            {
                conexao = server.conexao();
                string sql = "Delete from Atividade where atiCodigo = @Codigo;";
                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Codigo", atividade.gsCodigo);
                command.ExecuteNonQuery();

                resultado = true;

                if (resultado)
                {
                    conexao.Close();

                    PlantacaoBD plantacao = new PlantacaoBD();
                    plantacao.atualizaCustosGanhos(codPlantacao);

                    if (plantacao.getResultado())
                    {
                        resultado = true;
                    }
                    else
                    {
                        resultado = false;
                        erro = plantacao.getErro();
                    }
                }
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
        // ***********         CONSULTA ATIVIDADE        *******************
        // *****************************************************************

        public Atividade consultaAtividade(Atividade atividadeDados)
        {
            Atividade atividade = new Atividade();

            try
            {
                conexao = server.conexao();
                string sql = "Select * from Atividade inner join Tipo on atiTipo = tipCodigo where atiCodigo = @Codigo;";
                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Codigo", atividadeDados.gsCodigo);
                leitor = command.ExecuteReader();

                if(leitor.HasRows)
                {
                    while(leitor.Read())
                    {
                        atividade.gsCodigo = Convert.ToInt32(leitor["atiCodigo"]);
                        atividade.gsCustos = Convert.ToDouble(leitor["atiCustos"]);
                        atividade.gsGanhos = Convert.ToDouble(leitor["atiGanhos"]);
                        atividade.gsData = leitor["atiData"].ToString().Substring(0,10);
                        atividade.gsRelatorio = leitor["atiRelatorio"].ToString();
                        atividade.gsTipo = leitor["tipTipo"].ToString();
                        atividade.gsPlantacao = Convert.ToInt32(leitor["atiPlantacao"]);
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

            return atividade;
        }

        // *****************************************************************
        // ***********         ALTERAÇÃO DE ATIVIDADE    *******************
        // *****************************************************************

        public void alteraAtividade(Atividade atividade, int codPlantacao)
        {
            try
            {
                conexao = server.conexao();
                string sql = "Update Atividade set atiTipo = @Tipo, atiCustos = @Custos, atiGanhos = @Ganhos, atiData = @Data, atiRelatorio = Upper(@Relatorio) where atiCodigo = @Codigo;";
                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Tipo", atividade.gsCodTipo);
                command.Parameters.AddWithValue("@Custos", atividade.gsCustos);
                command.Parameters.AddWithValue("@Ganhos", atividade.gsGanhos);
                command.Parameters.AddWithValue("@Data", Helper.formataData(atividade.gsData));
                command.Parameters.AddWithValue("@Relatorio", atividade.gsRelatorio.ToUpper());
                command.Parameters.AddWithValue("@Codigo", atividade.gsCodigo);
                command.ExecuteNonQuery();

                resultado = true;

                if(resultado)
                {
                    conexao.Close();

                    PlantacaoBD plantacao = new PlantacaoBD();
                    plantacao.atualizaCustosGanhos(codPlantacao);

                    if(!plantacao.getResultado())
                    {
                        resultado = false;
                        erro = plantacao.getErro();
                    }
                }
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
