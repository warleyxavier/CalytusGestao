using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalytusGestao.Model.Classes;
using CalytusGestao.Model.ClassesBD;

namespace CalytusGestao.Controller.Controller
{
    public class AnaliseContr : Controle
    {
        Analise analise = null;
        AnaliseBD analiseBD = null;

        // *****************************************************************
        // *********** VERIFICAÇÃO DE ERRO NO ACESSO AO BD  ****************
        // *****************************************************************

        public void verificaErro()
        {
            if (!analiseBD.getResultado())
            {
                this.resultado = false;
                this.erro = analiseBD.getErro();
            }
        }

        // *****************************************************************
        // *****************    CADASTRO DE ANÁLISE   **********************
        // *****************************************************************

        public void cadastroAnalise(int codPlantacao, string data, double profundidade, double ph, double fosforo, double potassio, double calcio, double magnesio, double aluminio, double CTCt, double CTCT, double v, double PRem, string imagem)
        {
            try
            {
                string endPasta = @"C:\Users\Public\Documents\Excellence\CalytusGestao\Imagens\Analises\";

                analise = new Analise();
                analise.Plantacao = codPlantacao;
                analise.Data = data;
                analise.Profundidade = profundidade;
                analise.Ph = ph;
                analise.Fosforo = fosforo;
                analise.Potassio = potassio;
                analise.Calcio = calcio;
                analise.Magnesio = magnesio;
                analise.Aluminio = aluminio;
                analise.Ctct = CTCt;
                analise.CtcT = CTCT;
                analise.IndSaturacao = v;
                analise.PRem = PRem;
                analise.Imagem = (!imagem.Equals("") ? endPasta + imagem : null);

                analiseBD = new AnaliseBD();
                analiseBD.cadastroAnalise(analise);

                resultado = true;
                verificaErro();
            }
            catch(Exception e)
            {
                resultado = true;
                erro = e.Message;
            }
        }

        // *****************************************************************
        // *****************    CONSULTA DE ANÁLISES   *********************
        // *****************************************************************

        public Analise[] consultaAnalises(string data, int codPlantacao)
        {
            Analise[] analises = null;

            try
            {
                analise = new Analise();
                analise.Data = data;
                analise.Plantacao = codPlantacao;

                analiseBD = new AnaliseBD();
                analises = analiseBD.consultaAnalises(analise);

                dados = analiseBD.getDados();
                resultado = true;
                verificaErro();
            }
            catch (Exception e)
            {
                resultado = true;
                erro = e.Message;
            }

            return analises;
        }

        // *****************************************************************
        // *****************    REMOÇÃO DE ANÁLISE     *********************
        // *****************************************************************

        public void removeAnalise(int codAnalise)
        {
            try
            {
                analise = new Analise();
                analise.Codigo = codAnalise;

                analiseBD = new AnaliseBD();
                analiseBD.removeAnalise(analise);

                resultado = true;
                verificaErro();
            }
            catch (Exception e)
            {
                resultado = true;
                erro = e.Message;
            }
        }

        // *****************************************************************
        // *****************    CONSULTA    ANÁLISE    *********************
        // *****************************************************************

        public Analise consultaAnalise(int codAnalise)
        {
            try
            {
                analise = new Analise();
                analise.Codigo = codAnalise;

                analiseBD = new AnaliseBD();
                analise = analiseBD.consultaAnalise(analise);

                dados = analiseBD.getDados();
                resultado = true;
                verificaErro();
            }
            catch (Exception e)
            {
                resultado = true;
                erro = e.Message;
            }

            return analise;
        }

        // *****************************************************************
        // *****************       ALTERA IMAGEM       *********************
        // *****************************************************************

        public void alteraImagemAnalise(int codAnalise, string imagem)
        {
            try
            {
                analise = new Analise();
                analise.Codigo = codAnalise;
                analise.Imagem = (!imagem.Equals("") ? imagem : null);

                analiseBD = new AnaliseBD();
                analiseBD.alteraImagemAnalise(analise);

                resultado = true;
                verificaErro();      
            }
            catch (Exception e)
            {
                resultado = true;
                erro = e.Message;
            }
        }

        // *****************************************************************
        // *****************       ALTERA ANALISE      *********************
        // *****************************************************************

        public void alteraAnalise(Analise analiseDds)
        {
            try
            {
                analiseBD = new AnaliseBD();
                analiseBD.alteraAnalise(analiseDds);

                resultado = true;
                verificaErro();
            }
            catch (Exception e)
            {
                resultado = true;
                erro = e.Message;
            }
        }
    }
}
