using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalytusGestao.Model.Classes;
using CalytusGestao.Model.ClassesBD;
using System.Windows.Forms;

namespace CalytusGestao.Controller.Controller
{
    public class AtividadeContr : Controle
    {
        // *****************************************************************
        // ***************** DECLARAÇÃO DOS OBJETOS   **********************
        // *****************************************************************

        Atividade atividade = null;
        AtividadeBD atividadeBD = null;

        // *****************************************************************
        // *********** VERIFICAÇÃO DE ERRO NO ACESSO AO BD  ****************
        // *****************************************************************

        public void verificaErro()
        {
            if (!atividadeBD.getResultado())
            {
                this.resultado = false;
                this.erro = atividadeBD.getErro();
            }
        }

        // *****************************************************************
        // *********** CONSULTA DE TIPOS DE ATIVIDADES   *******************
        // *****************************************************************

        public Atividade[] consultaTipos(string pesquisa)
        {
            Atividade[] atividades = null;

            try
            {
                atividade = new Atividade();
                atividade.gsTipo = pesquisa;

                atividadeBD = new AtividadeBD();
                atividades = atividadeBD.consultaTipos(atividade);

                dados = atividadeBD.getDados();
                resultado = true;

                verificaErro();
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }

            return atividades;
        }

        // *****************************************************************
        // *********** CONSULTA CÓDIGO DE TIPO DE ATIV.  *******************
        // *****************************************************************

        public int consultaCodTipo(string tipo)
        {
            int codigo = 0;

            try
            {
                atividade = new Atividade();
                atividade.gsTipo = tipo;

                atividadeBD = new AtividadeBD();
                codigo = atividadeBD.consultaCodTipo(atividade);

                dados = atividadeBD.getDados();
                resultado = true;

                verificaErro();
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }

            return codigo;
        }

        // *****************************************************************
        // ***********       CADASTRO DE ATIVIDADE       *******************
        // *****************************************************************

        public void cadastroAtividade(int codPlantacao, string data, double custos, double ganhos, int tipo, string relatorio)
        {
            try
            {
                atividade = new Atividade();
                atividade.gsData = data;
                atividade.gsCustos = custos;
                atividade.gsGanhos = ganhos;
                atividade.gsCodTipo = tipo;
                atividade.gsRelatorio = relatorio;

                atividadeBD = new AtividadeBD();
                atividadeBD.cadastroAtividade(atividade, codPlantacao);                

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
        // ***********       CONSULTA DE ATIVIDADES      *******************
        // *****************************************************************

        public Atividade[] consultaAtividades(string pesquisa, int codPlantacao)
        {
            Atividade[] atividades = null;

            try
            {
                atividade = new Atividade();
                atividade.gsTipo = pesquisa;

                atividadeBD = new AtividadeBD();
                atividades = atividadeBD.consultaAtividades(atividade, codPlantacao);

                dados = atividadeBD.getDados();
                resultado = true;
                verificaErro();
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }

            return atividades;
        }

        // *****************************************************************
        // ***********         REMOÇÃO DE ATIVIDADE      *******************
        // *****************************************************************

        public void removeAtividade(int codigo, int codPlantacao)
        {
            try
            {
                atividade = new Atividade();
                atividade.gsCodigo = codigo;

                atividadeBD = new AtividadeBD();
                atividadeBD.removeAtividade(atividade, codPlantacao);

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
        // ***********         CONSULTA ATIVIDADE        *******************
        // *****************************************************************

        public Atividade consultaAtividade(int codAtividade)
        {
            try
            {
                atividade = new Atividade();
                atividade.gsCodigo = codAtividade;

                atividadeBD = new AtividadeBD();
                atividade = atividadeBD.consultaAtividade(atividade);

                dados = atividadeBD.getDados();
                resultado = true;
                verificaErro();
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }

            return atividade;
        }

        // *****************************************************************
        // ***********         ALTERAÇÃO DE ATIVIDADE    *******************
        // *****************************************************************

        public void alteraAtividade(int codAtividade, int codTipo, string data, double custos, double ganhos, string relatorio, int codPlantacao)
        {
            try
            {
                atividade = new Atividade();
                atividade.gsCodigo = codAtividade;
                atividade.gsCodTipo = codTipo;
                atividade.gsData = data;
                atividade.gsGanhos = ganhos;
                atividade.gsCustos = custos;
                atividade.gsRelatorio = relatorio;

                atividadeBD = new AtividadeBD();
                atividadeBD.alteraAtividade(atividade, codPlantacao);

                resultado = true;
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
