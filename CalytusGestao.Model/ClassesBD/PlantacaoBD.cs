using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalytusGestao.Model.Classes;
using System.Data.SQLite;

namespace CalytusGestao.Model.ClassesBD
{
    public class PlantacaoBD : Controle
    {
        // *****************************************************************
        // ***************** DECLARAÇÃO DOS OBJETOS   **********************
        // *****************************************************************

        Conexao server = new Conexao();

        // *****************************************************************
        // ***************     CADASTRO DE PLANTAÇÃO     *******************
        // *****************************************************************

        public void cadastroPlantacao(Plantacao plantacao)
        {
            try
            {
                conexao = server.conexao();
                string sql = "insert into Plantacao(plaIdentificacao, plaAno, plaQtdPlantas, plaLocalizacao, plaMunicipio, plaTamanhoArea, plaQtdAtualPlantas, plaCustos, plaGanhos, plaStatus, plaImagem) values (Upper(@Identificacao), Upper(@Ano), Upper(@QtdPlantas), Upper(@Localizacao), Upper(@Municipio), Upper(@TamanhoArea), Upper(@QtdAtualPlantas), Upper(@Custos), Upper(@Ganhos), Upper(@Status), Upper(@Imagem));";
                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Identificacao", plantacao.Identificacao.ToUpper());
                command.Parameters.AddWithValue("@Ano", plantacao.AnoPlantacao);
                command.Parameters.AddWithValue("@QtdPlantas", plantacao.QtdPlantasPlantadas);
                command.Parameters.AddWithValue("@Localizacao", plantacao.Localizacao.ToUpper());
                command.Parameters.AddWithValue("@Municipio", plantacao.Municipio.ToUpper());
                command.Parameters.AddWithValue("@TamanhoArea", plantacao.TamanhoArea);
                command.Parameters.AddWithValue("@QtdAtualPlantas", plantacao.QtdPlantasPlantadas);
                command.Parameters.AddWithValue("@Custos", 0);
                command.Parameters.AddWithValue("@Ganhos", 0);
                command.Parameters.AddWithValue("@Status", plantacao.Status);
                command.Parameters.AddWithValue("@Imagem", (!plantacao.Imagem.Equals("") ? plantacao.Imagem : null));
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
        // ***************     CONSULTA DE PLANTAÇÕES    *******************
        // *****************************************************************

        public Plantacao[] consultaPlantacoes(Plantacao plantacao)
        {
            Plantacao[] plantacoes = null;

            try
            {
                conexao = server.conexao();
                string sql = "select count(plaCodigo) from Plantacao where plaIdentificacao like @Pesquisa;";
                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Pesquisa", "%" + plantacao.Identificacao.ToUpper() + "%");
                int quantidade = Convert.ToInt32(command.ExecuteScalar());

                if(quantidade > 0)
                {
                    conexao.Close();
                    plantacoes = new Plantacao[quantidade];

                    conexao.Open();
                    sql = "select * from Plantacao where plaIdentificacao like @Pesquisa order by plaIdentificacao; ";
                    command = server.command(sql, conexao);
                    command.Parameters.AddWithValue("@Pesquisa", "%" + plantacao.Identificacao.ToUpper() + "%");
                    leitor = command.ExecuteReader();

                    int contador = 0;

                    while(leitor.Read())
                    {
                        plantacoes[contador] = new Plantacao();
                        plantacoes[contador].Codigo = Convert.ToInt32(leitor["plaCodigo"]);
                        plantacoes[contador].Identificacao = leitor["plaIdentificacao"].ToString();
                        plantacoes[contador].AnoPlantacao = Convert.ToInt32(leitor["plaAno"]);
                        plantacoes[contador].QtdPlantasPlantadas = Convert.ToInt32(leitor["plaQtdPlantas"]);
                        plantacoes[contador].TamanhoArea = Convert.ToDouble(leitor["plaTamanhoArea"]);
                        plantacoes[contador].Localizacao = leitor["plaLocalizacao"].ToString();
                        plantacoes[contador].Municipio = leitor["plaMunicipio"].ToString();
                        plantacoes[contador].Custos = Convert.ToDouble(leitor["plaCustos"]);
                        plantacoes[contador].Ganhos = Convert.ToDouble(leitor["plaGanhos"]);
                        plantacoes[contador].Idade = DateTime.Now.Year - Convert.ToInt32(leitor["plaAno"]);
                        plantacoes[contador].Status = leitor["plaStatus"].ToString();
                        plantacoes[contador].Imagem = leitor["plaImagem"].ToString();

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

            return plantacoes;
        }

        // *****************************************************************
        // ***************     REMOÇÃO DE PLANTAÇÕES     *******************
        // *****************************************************************

        public void removePlantacao(Plantacao plantacao)
        {
            try
            {
                conexao = server.conexao();
                string sql = "pragma foreign_keys = on; delete from Plantacao where plaCodigo = @Codigo;";
                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Codigo", plantacao.Codigo);
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
        // ***************       CONSULTA PLANTAÇÃO      *******************
        // *****************************************************************

        public Plantacao consultaPlantacao(Plantacao docPlantacao)
        {
            Plantacao plantacao = null;

            try
            {
                conexao = server.conexao();
                string sql = "Select * from Plantacao where plaCodigo = @Codigo; ";
                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Codigo", docPlantacao.Codigo);
                leitor = command.ExecuteReader();

                if(leitor.HasRows)
                {
                    while(leitor.Read())
                    {
                        plantacao = new Plantacao();

                        plantacao.Codigo = Convert.ToInt32(leitor["plaCodigo"]);
                        plantacao.Identificacao = leitor["plaIdentificacao"].ToString();
                        plantacao.AnoPlantacao = Convert.ToInt32(leitor["plaAno"]);
                        plantacao.Idade = DateTime.Now.Year - Convert.ToInt32(leitor["plaAno"]);
                        plantacao.QtdPlantasPlantadas = Convert.ToInt32(leitor["plaQtdPlantas"]);
                        plantacao.QtdPlantasAtual = Convert.ToInt32(leitor["plaQtdAtualPlantas"]);
                        plantacao.TamanhoArea = Convert.ToDouble(leitor["plaTamanhoArea"]);
                        plantacao.Status = leitor["plaStatus"].ToString();
                        plantacao.Municipio = leitor["plaMunicipio"].ToString();
                        plantacao.Localizacao = leitor["plaLocalizacao"].ToString();
                        plantacao.Custos = Convert.ToDouble(leitor["plaCustos"]);
                        plantacao.Ganhos = Convert.ToDouble(leitor["plaGanhos"]);
                        plantacao.Imagem = leitor["plaImagem"].ToString();

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

            return plantacao;
        }

        // *****************************************************************
        // ***************      ALTERAÇÃO DE IMAGEM      *******************
        // *****************************************************************

        public void alteraImagem(Plantacao plantacao)
        {
            try
            {
                conexao = server.conexao();
                string sql = "Update Plantacao set plaImagem = @Imagem where plaCodigo = @Codigo;";
                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Imagem", (!plantacao.Imagem.Equals("") ? plantacao.Imagem : null));
                command.Parameters.AddWithValue("@Codigo", plantacao.Codigo);
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
        // ***************      ALTERAÇÃO DE PLANTAÇÃO   *******************
        // *****************************************************************

        public void alteraPlantacao(Plantacao plantacao)
        {
            try
            {
                conexao = server.conexao();
                string sql = "Update Plantacao set plaIdentificacao = Upper(@Identificacao), plaAno = @Ano, plaQtdPlantas = @QtdPlantas, plaQtdAtualPlantas = @QtdAtual, plaTamanhoArea = @Tamanho, plaMunicipio = Upper(@Municipio), plaLocalizacao = Upper(@Localizacao), plaStatus = Upper(@Status) where plaCodigo = @Codigo; ";
                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Identificacao", plantacao.Identificacao.ToUpper());
                command.Parameters.AddWithValue("@Ano", plantacao.AnoPlantacao);
                command.Parameters.AddWithValue("@QtdPlantas", plantacao.QtdPlantasPlantadas);
                command.Parameters.AddWithValue("@QtdAtual", plantacao.QtdPlantasAtual);
                command.Parameters.AddWithValue("@Tamanho", plantacao.TamanhoArea);
                command.Parameters.AddWithValue("@Localizacao", plantacao.Localizacao.ToUpper());
                command.Parameters.AddWithValue("@Municipio", plantacao.Municipio.ToUpper());
                command.Parameters.AddWithValue("@Status", plantacao.Status);
                command.Parameters.AddWithValue("@Codigo", plantacao.Codigo);
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

        // ***************************************************************************************
        // ***************      CONSULTA DE ESPÉCIES CULTIVADAS NA PLANTAÇÃO   *******************
        // ***************************************************************************************

        public SQLiteDataReader consultaEspPlantacao(Plantacao plantacao)
        {
            try
            {
                conexao = server.conexao();
                string sql = "select plECodigo,espCodigo,espNomePopular from PlantacaoEspecie inner join Especie on plEEspecie = espCodigo where plEPlantacao = @Codigo;";
                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Codigo", plantacao.Codigo);
                leitor = command.ExecuteReader();

                if(leitor.HasRows)
                {
                    dados = true;
                }

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

        // ***************************************************************************************
        // ***************      REMOÇÃO DE ESPÉCIE CULTIVADA NA PLANTAÇÃO      *******************
        // ***************************************************************************************

        public void remocaoEspPlantacao(Plantacao plantacao)
        {
            try
            {
                conexao = server.conexao();
                string sql = "delete from PlantacaoEspecie where plECodigo = @Codigo;";
                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Codigo", plantacao.Codigo);
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

        // ***************************************************************************************
        // ***************     CADASTRO DE ESPÉCIE CULTIVADA NA PLANTAÇÃO      *******************
        // ***************************************************************************************

        public void cadastroEspPlantacao(int[] codsEspecies, Plantacao plantacao)
        {
            try
            {
                conexao = server.conexao();

                StringBuilder sql = new StringBuilder();

                for(int i = 0; i < codsEspecies.Length; i++)
                {
                    sql.AppendLine("Insert into PlantacaoEspecie(plEPlantacao, plEEspecie) values(" + plantacao.Codigo + ", " + codsEspecies[i] + ");");
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

        // ***************************************************************************************
        // ***************     ATUALIZAÇÃO DE CUSTOS E GANHOS DA PLANTAÇÃO     *******************
        // ***************************************************************************************

        public void atualizaCustosGanhos(int codPlantacao)
        {
            try
            {
                conexao = server.conexao();
                string sql = "Update Plantacao set plaCustos = (select sum(atiCustos) from Atividade where atiPlantacao = @Plantacao), plaGanhos = (select sum(atiGanhos) from Atividade where atiPlantacao = @Plantacao) where plaCodigo = @Plantacao;";
                command = server.command(sql, conexao);
                command.Parameters.AddWithValue("@Plantacao", codPlantacao);
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
