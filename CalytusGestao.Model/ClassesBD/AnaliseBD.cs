using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using CalytusGestao.Model.Helpers;
using CalytusGestao.Model.Classes;
using System.Windows.Forms;

namespace CalytusGestao.Model.ClassesBD
{
    public class AnaliseBD : Controle
    {
        // *****************************************************************
        // ***************** DECLARAÇÃO DOS OBJETOS   **********************
        // *****************************************************************

        Conexao server = new Conexao();

        // *****************************************************************
        // *****************    CADASTRO DE ANÁLISE   **********************
        // *****************************************************************

        public void cadastroAnalise(Analise analise)
        {
            try
            {
                conexao = server.conexao();
                string sql = "Insert into Analise(anaPlantacao, anaData, anaProfundidade, anaPh, anaFosforo, anaPotassio, anaCalcio, anaMagnesio, anaAluminio, anaCTCt1, anaCTCt2, anaIndSaturacao, anaPRem, anaImagem) values (@Plantacao, @Data, @Profundidade, @Ph, @Fosforo, @Potassio, @Calcio, @Magnesio, @Aluminio, @CTC1, @CTC2, @Saturacao, @PRem, @Imagem);";
                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Plantacao", analise.Plantacao);
                command.Parameters.AddWithValue("@Data", Helper.formataData(analise.Data));
                command.Parameters.AddWithValue("@Profundidade", analise.Profundidade);
                command.Parameters.AddWithValue("@Ph", analise.Ph);
                command.Parameters.AddWithValue("@Fosforo", analise.Fosforo);
                command.Parameters.AddWithValue("@Potassio", analise.Potassio);
                command.Parameters.AddWithValue("@Calcio", analise.Calcio);
                command.Parameters.AddWithValue("@Magnesio", analise.Magnesio);
                command.Parameters.AddWithValue("@Aluminio", analise.Aluminio);
                command.Parameters.AddWithValue("@CTC1", analise.Ctct);
                command.Parameters.AddWithValue("@CTC2", analise.CtcT);
                command.Parameters.AddWithValue("@Saturacao", analise.IndSaturacao);
                command.Parameters.AddWithValue("@PRem", analise.PRem);
                command.Parameters.AddWithValue("@Imagem", analise.Imagem);
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
        // *****************    CONSULTA DE ANÁLISES   *********************
        // *****************************************************************

        public Analise[] consultaAnalises(Analise analise)
        {
            Analise[] analises = null;

            try
            {
                conexao = server.conexao();
                string sql = "Select count(anaCodigo) from Analise where " + (!analise.Data.Equals("") ? "anaData = @Data and " : "") + "anaPlantacao = @Codigo;";
                command = server.command(sql, conexao);

                if (!analise.Data.Equals(""))
                {
                    command.Parameters.AddWithValue("@Data", Helper.formataData(analise.Data));
                }
                command.Parameters.AddWithValue("@Codigo", analise.Plantacao);

                int quantidade = Convert.ToInt32(command.ExecuteScalar());

                if(quantidade > 0)
                {
                    analises = new Analise[quantidade];

                    sql = "Select * from Analise where " + (!analise.Data.Equals("") ? "anaData = @Data and " : "") + "anaPlantacao = @Codigo order by anaData desc;";
                    command = server.command(sql, conexao);

                    if (!analise.Data.Equals(""))
                    {
                        command.Parameters.AddWithValue("@Data", Helper.formataData(analise.Data));
                    }
                    command.Parameters.AddWithValue("@Codigo", analise.Plantacao);

                    leitor = command.ExecuteReader();

                    int contador = 0;

                    while(leitor.Read())
                    {
                        analises[contador] = new Analise();

                        analises[contador].Codigo = Convert.ToInt32(leitor["anaCodigo"]);
                        analises[contador].Data = leitor["anaData"].ToString().Substring(0,10);
                        analises[contador].Profundidade = Convert.ToDouble(leitor["anaProfundidade"]);
                        analises[contador].Ph = Convert.ToDouble(leitor["anaPh"]);
                        analises[contador].Fosforo = Convert.ToDouble(leitor["anaFosforo"]);
                        analises[contador].Potassio = Convert.ToDouble(leitor["anaPotassio"]);
                        analises[contador].Calcio = Convert.ToDouble(leitor["anaCalcio"]);
                        analises[contador].Magnesio = Convert.ToDouble(leitor["anaMagnesio"]);
                        analises[contador].Aluminio = Convert.ToDouble(leitor["anaAluminio"]);
                        analises[contador].Ctct = Convert.ToDouble(leitor["anaCTCt1"]);
                        analises[contador].CtcT = Convert.ToDouble(leitor["anaCTCt2"]);
                        analises[contador].IndSaturacao = Convert.ToDouble(leitor["anaIndSaturacao"]);
                        analises[contador].PRem = Convert.ToDouble(leitor["anaPRem"]);
                        analises[contador].Imagem = leitor["anaImagem"].ToString();

                        contador++;
                    }

                    dados = true;
                    leitor.Dispose();
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

            return analises;
        }

        // *****************************************************************
        // *****************    REMOÇÃO DE ANÁLISE     *********************
        // *****************************************************************

        public void removeAnalise(Analise analise)
        {
            try
            {
                conexao = server.conexao();
                string sql = "Delete from Analise where anaCodigo = @Codigo;";
                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Codigo", analise.Codigo);
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
        // *****************    CONSULTA    ANÁLISE    *********************
        // *****************************************************************

        public Analise consultaAnalise(Analise analiseDds)
        {
            Analise analise = new Analise();

            try
            {
                conexao = server.conexao();
                string sql = "select * from Analise where anaCodigo = @Codigo;";
                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Codigo", analiseDds.Codigo);
                leitor = command.ExecuteReader();

                if (leitor.HasRows)
                {
                    while (leitor.Read())
                    {
                        analise.Codigo = Convert.ToInt32(leitor["anaCodigo"]);
                        analise.Data = leitor["anaData"].ToString().Substring(0, 10);
                        analise.Profundidade = Convert.ToDouble(leitor["anaProfundidade"]);
                        analise.Ph = Convert.ToDouble(leitor["anaPh"]);
                        analise.Fosforo = Convert.ToDouble(leitor["anaFosforo"]);
                        analise.Potassio = Convert.ToDouble(leitor["anaPotassio"]);
                        analise.Calcio = Convert.ToDouble(leitor["anaCalcio"]);
                        analise.Magnesio = Convert.ToDouble(leitor["anaMagnesio"]);
                        analise.Aluminio = Convert.ToDouble(leitor["anaAluminio"]);
                        analise.Ctct = Convert.ToDouble(leitor["anaCTCt1"]);
                        analise.CtcT = Convert.ToDouble(leitor["anaCTCt2"]);
                        analise.IndSaturacao = Convert.ToDouble(leitor["anaIndSaturacao"]);
                        analise.PRem = Convert.ToDouble(leitor["anaPRem"]);
                        analise.Imagem = leitor["anaImagem"].ToString();
                    }

                    dados = true;
                    leitor.Dispose();
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

            return analise;
        }

        // *****************************************************************
        // *****************       ALTERA IMAGEM       *********************
        // *****************************************************************

        public void alteraImagemAnalise(Analise analise)
        {
            try
            {
                conexao = server.conexao();
                string sql = "Update Analise set anaImagem = @Imagem where anaCodigo = @Codigo;";
                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Imagem", analise.Imagem);
                command.Parameters.AddWithValue("@Codigo", analise.Codigo);
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
        // *****************       ALTERA ANALISE      *********************
        // *****************************************************************

        public void alteraAnalise(Analise analise)
        {
            try
            {
                conexao = server.conexao();
                string sql = "Update Analise set anaData = @Data, anaProfundidade = @Profundidade, anaPh = @Ph, anaFosforo = @Fosforo, anaPotassio = @Potassio, anaCalcio = @Calcio, anaMagnesio = @Magnesio, anaAluminio = @Aluminio, anaCTCt1 = @ctc1, anaCTCt2 = @ctc2, anaIndSaturacao = @Saturacao, anaPRem = @PRem where anaCodigo = @Codigo;";
                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Data", analise.Data);
                command.Parameters.AddWithValue("@Profundidade", analise.Profundidade);
                command.Parameters.AddWithValue("@Ph", analise.Ph);
                command.Parameters.AddWithValue("@Fosforo", analise.Fosforo);
                command.Parameters.AddWithValue("@Potassio", analise.Potassio);
                command.Parameters.AddWithValue("@Calcio", analise.Calcio);
                command.Parameters.AddWithValue("@Magnesio", analise.Magnesio);
                command.Parameters.AddWithValue("@Aluminio", analise.Aluminio);
                command.Parameters.AddWithValue("@Ctc1", analise.Ctct);
                command.Parameters.AddWithValue("@Ctc2", analise.CtcT);
                command.Parameters.AddWithValue("@Saturacao", analise.IndSaturacao);
                command.Parameters.AddWithValue("@PRem", analise.PRem);
                command.Parameters.AddWithValue("@Codigo", analise.Codigo);
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
