using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalytusGestao.Model.Classes;
using CalytusGestao.Model.ClassesBD;
using System.Data.SQLite;

namespace CalytusGestao.Controller.Controller
{
    public class PlantacaoContr : Controle
    {
        // *****************************************************************
        // ***************** DECLARAÇÃO DOS OBJETOS   **********************
        // *****************************************************************

        Plantacao plantacao = null;
        PlantacaoBD plantacaoBD = null;

        // *****************************************************************
        // *********** VERIFICAÇÃO DE ERRO NO ACESSO AO BD  ****************
        // *****************************************************************

        public void verificaErro()
        {
            if (!plantacaoBD.getResultado())
            {
                this.resultado = false;
                this.erro = plantacaoBD.getErro();
            }
        }

        // *****************************************************************
        // ******************   CADASTRO DE PLATAÇÃO  **********************
        // *****************************************************************

        public void cadastroPlantacao(string identificacao, int ano, int qtdPlantas, double tamArea, string localizacao, string municipio, string status, string imagem)
        {
            try
            {
                plantacao = new Plantacao();
                plantacao.Identificacao = identificacao;
                plantacao.AnoPlantacao = ano;
                plantacao.QtdPlantasPlantadas = qtdPlantas;
                plantacao.TamanhoArea = tamArea;
                plantacao.Localizacao = localizacao;
                plantacao.Municipio = municipio;
                plantacao.Status = status;
                plantacao.Imagem = imagem;

                plantacaoBD = new PlantacaoBD();
                plantacaoBD.cadastroPlantacao(plantacao);

                resultado = true;
                verificaErro();
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }
        }

        // *****************************************************************
        // ***************     CONSULTA DE PLANTAÇÕES    *******************
        // *****************************************************************

        public Plantacao[] consultaPlantacoes(string pesquisa)
        {
            Plantacao[] plantacoes = null;

            try
            {
                plantacao = new Plantacao();
                plantacao.Identificacao = pesquisa;

                plantacaoBD = new PlantacaoBD();
                plantacoes = plantacaoBD.consultaPlantacoes(plantacao);

                resultado = true;
                dados = plantacaoBD.getDados();
                verificaErro();
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }

            return plantacoes;
        }

        // *****************************************************************
        // ***************     REMOÇÃO DE PLANTAÇÕES     *******************
        // *****************************************************************

        public void removePlantacao(int codigo)
        {
            try
            {
                plantacao = new Plantacao();
                plantacao.Codigo = codigo;

                plantacaoBD = new PlantacaoBD();
                plantacaoBD.removePlantacao(plantacao);

                resultado = true;
                verificaErro();
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }
        }

        // *****************************************************************
        // ***************       CONSULTA PLANTAÇÃO      *******************
        // *****************************************************************

        public Plantacao consultaPlantacao(int codigo)
        {
            Plantacao plantacaoDados = null;

            try
            {
                plantacao = new Plantacao();
                plantacao.Codigo = codigo;

                plantacaoBD = new PlantacaoBD();
                plantacaoDados = plantacaoBD.consultaPlantacao(plantacao);

                dados = plantacaoBD.getDados();
                resultado = true;
                verificaErro();
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }

            return plantacaoDados;
        }

        // *****************************************************************
        // ***************      ALTERAÇÃO DE IMAGEM      *******************
        // *****************************************************************

        public void alteraImagem(int codigo, string imagem)
        {
            try
            {
                plantacao = new Plantacao();
                plantacao.Codigo = codigo;
                plantacao.Imagem = imagem;

                plantacaoBD = new PlantacaoBD();
                plantacaoBD.alteraImagem(plantacao);

                resultado = true;
                verificaErro();
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }
        }

        // *****************************************************************
        // ***************      ALTERAÇÃO DE PLANTAÇÃO   *******************
        // *****************************************************************

        public void alteraPlantacao(int codigo, string identificacao, int ano, int qtdPlantas, int qtdAtual, double tamanho, string municipio, string localizacao, string status)
        {
            try
            {
                plantacao = new Plantacao();
                plantacao.Codigo = codigo;
                plantacao.Identificacao = identificacao;
                plantacao.AnoPlantacao = ano;
                plantacao.QtdPlantasPlantadas = qtdPlantas;
                plantacao.QtdPlantasAtual = qtdAtual;
                plantacao.TamanhoArea = tamanho;
                plantacao.Municipio = municipio;
                plantacao.Localizacao = localizacao;
                plantacao.Status = status;

                plantacaoBD = new PlantacaoBD();
                plantacaoBD.alteraPlantacao(plantacao);

                resultado = true;
                verificaErro();
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }
        }

        // ***************************************************************************************
        // ***************      CONSULTA DE ESPÉCIES CULTIVADAS NA PLANTAÇÃO   *******************
        // ***************************************************************************************

        public SQLiteDataReader consultaEspPlantacao(int codigo)
        {
            try
            {
                plantacao = new Plantacao();
                plantacao.Codigo = codigo;

                plantacaoBD = new PlantacaoBD();
                leitor = plantacaoBD.consultaEspPlantacao(plantacao);

                dados = plantacaoBD.getDados();
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

        // ***************************************************************************************
        // ***************      REMOÇÃO DE ESPÉCIE CULTIVADA NA PLANTAÇÃO      *******************
        // ***************************************************************************************

        public void remocaoEspPlantacao(int codigo)
        {
            try
            {
                plantacao = new Plantacao();
                plantacao.Codigo = codigo;

                plantacaoBD = new PlantacaoBD();
                plantacaoBD.remocaoEspPlantacao(plantacao);

                resultado = true;
                verificaErro();
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }
        }

        // ***************************************************************************************
        // ***************     CADASTRO DE ESPÉCIE CULTIVADA NA PLANTAÇÃO      *******************
        // ***************************************************************************************

        public void cadastroEspPlantacao(int[] cods, int codPlantacao)
        {
            try
            {
                plantacao = new Plantacao();
                plantacao.Codigo = codPlantacao;

                plantacaoBD = new PlantacaoBD();
                plantacaoBD.cadastroEspPlantacao(cods, plantacao);

                resultado = true;
                verificaErro();
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }
        }

        // ***************************************************************************************
        // ************      CONSULTA NOMES DE ESPÉCIES CULTIVADAS NA PLANTAÇÃO   ****************
        // ***************************************************************************************

        public string consultaNomesEspPlantacao(int codigo)
        {
            string nomes = "";

            try
            {
                plantacao = new Plantacao();
                plantacao.Codigo = codigo;

                plantacaoBD = new PlantacaoBD();
                leitor = plantacaoBD.consultaEspPlantacao(plantacao);

                if(leitor.HasRows)
                {
                    while(leitor.Read())
                    {
                        nomes += leitor["espNomePopular"].ToString() + " - ";
                    }
                }

                dados = plantacaoBD.getDados();
                resultado = true;

                verificaErro();
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }
            finally
            {
                leitor.Dispose();
            }

            return nomes;
        }
    }
}
