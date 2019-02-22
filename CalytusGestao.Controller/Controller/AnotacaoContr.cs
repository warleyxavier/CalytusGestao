using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalytusGestao.Model.Classes;
using CalytusGestao.Model.ClassesBD;

namespace CalytusGestao.Controller.Controller
{
    public class AnotacaoContr : Controle
    {
        Anotacao anotacao = null;
        AnotacaoBD anotacaoBD = null;

        
        // *****************************************************************
        // *********** VERIFICAÇÃO DE ERRO NO ACESSO AO BD  ****************
        // *****************************************************************

        public void verificaErro()
        {
            if(!anotacaoBD.getResultado())
            {
                this.resultado = false;
                this.erro = anotacaoBD.getErro();
            }
        }

        // *****************************************************************
        // ***********         CADASTRO DE ANOTAÇÃO         ****************
        // *****************************************************************

        public void cadastraAnotacao(string assunto, string anotacaoTexto)
        {
            try
            {
                anotacao = new Anotacao();
                anotacao.gsAssunto = assunto;
                anotacao.gsAnotacao = anotacaoTexto;

                anotacaoBD = new AnotacaoBD();
                anotacaoBD.cadatraAnotacao(anotacao);

                this.resultado = true;

                verificaErro();
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }
        }

        // *****************************************************************
        // ***********   CONSULTA DE ANOTAÇÕES => ASSUNTO   ****************
        // *****************************************************************

        public Anotacao[] consultaAnotacoes(string pesquisa)
        {
            Anotacao[] anotacoes = null;

            try
            {
                anotacao = new Anotacao();
                anotacao.gsAssunto = pesquisa;

                anotacaoBD = new AnotacaoBD();
                anotacoes = anotacaoBD.consultaAnotacoes(anotacao);

                this.dados = anotacaoBD.getDados();
                this.resultado = true;

                verificaErro();
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }

            return anotacoes;
        }

        // *****************************************************************
        // ***********       REMOVE ANOTAÇÃO ==> CÓDIGO     ****************
        // *****************************************************************

        public void removeAnotacao(int codigo)
        {
            try
            {
                anotacao = new Anotacao();
                anotacao.gsCodigo = codigo;

                anotacaoBD = new AnotacaoBD();
                anotacaoBD.removeAnotacao(anotacao);

                this.resultado = true;

                verificaErro();
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }
        }

        // *****************************************************************
        // *********       CONSULTA ANOTAÇÃO ==> CÓDIGO     ****************
        // *****************************************************************

        public Anotacao consultaAnotacao(int codigo)
        {
            try
            {
                anotacao = new Anotacao();
                anotacao.gsCodigo = codigo;

                anotacaoBD = new AnotacaoBD();
                anotacao = anotacaoBD.consultaAnotacao(anotacao);

                this.resultado = true;

                verificaErro();
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }

            return anotacao;
        }

        // *****************************************************************
        // *********           ATUALIZA ANOTAÇÃO            ****************
        // *****************************************************************

        public void atualizaAnotacao(int codigo, string assunto, string anotacaoTexto)
        {
            try
            {
                anotacao = new Anotacao();
                anotacao.gsCodigo = codigo;
                anotacao.gsAssunto = assunto;
                anotacao.gsAnotacao = anotacaoTexto;

                anotacaoBD = new AnotacaoBD();
                anotacaoBD.atualizaAnotacao(anotacao);

                this.resultado = true;

                verificaErro();
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }
        }
    }
}
