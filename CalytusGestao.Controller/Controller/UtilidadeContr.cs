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
    public class UtilidadeContr : Controle
    {
        Utilidade utilidade = null;
        UtilidadeBD utilidadeBD = null;

        // *****************************************************************
        // *********** VERIFICAÇÃO DE ERRO NO ACESSO AO BD  ****************
        // *****************************************************************

        public void verificaErro()
        {
            if (!utilidadeBD.getResultado())
            {
                this.resultado = false;
                this.erro = utilidadeBD.getErro();
            }
        }

        // *****************************************************************
        // ****************   CONSULTA UTILIDADES        *******************
        // *****************************************************************

        public Utilidade[] consultaUtilidades(string pesquisa)
        {
            Utilidade[] codUtilidades = null;
            try
            {
                utilidade = new Utilidade();
                utilidade.gsUtilidade = pesquisa;

                utilidadeBD = new UtilidadeBD();
                codUtilidades = utilidadeBD.consultaUtilidades(utilidade);
                dados = utilidadeBD.getDados();

                resultado = true;

                verificaErro();
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }
            return codUtilidades;
        }

        // *****************************************************************
        // ***********    CADASTRO UTILIDADES ==> ESPÉCIE  *****************
        // *****************************************************************

        public void cadastraUtilidadeEspecie(int codEspecie, int[] codUtilidades)
        {
            try
            {
                Especie especie = new Especie();
                especie.gsCodigo = codEspecie;

                utilidadeBD = new UtilidadeBD();
                utilidadeBD.cadastraUtilidadesEspecie(especie, codUtilidades);

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
        // ***********    CONSULTA UTILIDADES ==> DOENÇA   *****************
        // *****************************************************************

        public SQLiteDataReader consultaUtilidadesEspecie(int codigo)
        {
            try
            {
                Especie especie = new Especie();
                especie.gsCodigo = codigo;

                utilidadeBD = new UtilidadeBD();
                leitor = utilidadeBD.consultaUtilidadesEspecie(especie);

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

        // *****************************************************************
        // ***********    REMOVE   UTILIDADES ==> DOENÇA   *****************
        // *****************************************************************

        public void removeUtilidadeEspecie(int codigo)
        {
            try
            {
                utilidadeBD = new UtilidadeBD();
                utilidadeBD.removeUtilidadeEspecie(codigo);

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
