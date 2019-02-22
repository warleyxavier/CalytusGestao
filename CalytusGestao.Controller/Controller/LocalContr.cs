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
    public class LocalContr : Controle
    {
        // *****************************************************************
        // ***************** DECLARAÇÃO DOS OBJETOS   **********************
        // *****************************************************************

        Local local = null;
        LocalBD localBD = null;

        // *****************************************************************
        // *********** VERIFICAÇÃO DE ERRO NO ACESSO AO BD  ****************
        // *****************************************************************

        public void verificaErro()
        {
            if (!localBD.getResultado())
            {
                this.resultado = false;
                this.erro = localBD.getErro();
            }
        }

        // *****************************************************************
        // ***********     CONSULTA LOCAIS ==> DOENÇA     ******************
        // *****************************************************************

        public Local[] consultaLocais(string pesquisa)
        {
            Local[] locais = null;

            try
            {
                local = new Local();
                local.gsLocal = pesquisa;

                localBD = new LocalBD();
                locais = localBD.consultaLocais(local);

                dados = localBD.getDados();
                resultado = true;

                verificaErro();
            }            
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }

            return locais;        
        }

        // *****************************************************************
        // ***********     CADASTRA LOCAIS ==> DOENÇA     ******************
        // *****************************************************************

        public void cadatraLocaisDoenca(int codDoenca, int[] codLocais)
        {
            try
            {
                if (codLocais!=null)
                {
                    Doenca doenca = new Doenca();
                    doenca.gsCodigo = codDoenca;

                    localBD = new LocalBD();
                    localBD.cadastraLocaisDoenca(doenca, codLocais);

                    this.resultado = true;

                    verificaErro();
                }
                else
                {
                    this.resultado = true;
                }                
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }
        }

        // *****************************************************************
        // ***********     CONSULTA LOCAIS ==> DOENÇA     ******************
        // *****************************************************************

        public SQLiteDataReader consultaLocaisDoenca(int codDoenca)
        {
            try
            {
                Doenca doenca = new Doenca();
                doenca.gsCodigo = codDoenca;

                localBD = new LocalBD();
                leitor = localBD.consultaLocaisDoenca(doenca);

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
        // ***********     REMOVE LOCAIS ==> DOENÇA     ********************
        // *****************************************************************

        public void removeLocalDoenca(int codigo)
        {
            try
            {
                localBD = new LocalBD();
                localBD.removeLocaisDoenca(codigo);

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
