using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalytusGestao.Model.Classes;
using CalytusGestao.Model.ClassesBD;
using CalytusGestao.Model.Helpers;

namespace CalytusGestao.Controller.Controller
{
    public class DoencaContr
    {
        Doenca doenca = null;
        DoencaBD doencaBD = null;

        // *****************************************************************
        // ***************** DECLARAÇÃO DOS ATRIBUTOS **********************
        // *****************************************************************

        private string erro = null;
        private bool resultado = false;
        private bool dados = false;

        // *****************************************************************
        // *********************** PROPRIEDADES ****************************
        // *****************************************************************

        public string getErro()
        {
            return this.erro;
        }

        public bool getResultado()
        {
            return this.resultado;
        }

        public bool getDados()
        {
            return this.dados;
        }

        // *****************************************************************
        // *********** VERIFICAÇÃO DE ERRO NO ACESSO AO BD  ****************
        // *****************************************************************

        public void verificaErro()
        {
            if (!doencaBD.getResultado())
            {
                this.resultado = false;
                this.erro = doencaBD.getErro();
            }
        }

        // *****************************************************************
        // ******************   CADASTRO DE DOENÇA    **********************
        // *****************************************************************

        public int cadastroDoenca(string nome, string caracteristicas, string diagnostico, string imagem)
        {
            int codDoenca = 0;

            try
            {
                doenca = new Doenca();
                doenca.gsNome = nome;
                doenca.gsCaracteristicas = caracteristicas;
                doenca.gsDiagnostico = diagnostico;
                doenca.gsImagem = (imagem.Equals("") ? "" : @"C:\Users\Public\Documents\Excellence\CalytusGestao\Imagens\Doenças\" + imagem);

                doencaBD = new DoencaBD();
                codDoenca = doencaBD.cadastroDoenca(doenca);

                this.resultado = true;

                verificaErro();
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }

            return codDoenca;
        }

        // *****************************************************************
        // ***********   CONSULTA DE DOENÇAS =>  NOME   ********************
        // *****************************************************************

        public Doenca[] consultaDoencas(string pesquisa, string tipo)
        {
            Doenca[] doencas = null;

            try
            {
                doenca = new Doenca();
                doencaBD = new DoencaBD();

                if (tipo.Equals("Nome"))
                {
                    doenca.gsNome = pesquisa;
                    doencas = doencaBD.consultaDoencasNome(doenca);
                }
                else
                {
                    Local local = new Local();
                    local.gsLocal = pesquisa;

                    doencas = doencaBD.consultaDoencasLocal(local);
                }
                this.dados = doencaBD.getDados();
                this.resultado = true;

                verificaErro();
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }

            return doencas;
        }

        // *****************************************************************
        // ***********   REMOVE DOENÇA ==> CÓDIGO       ********************
        // *****************************************************************

        public void removeDoenca(int codigo)
        {
            try
            {
                doenca = new Doenca();
                doenca.gsCodigo = codigo;

                doencaBD = new DoencaBD();
                doencaBD.removeDoenca(doenca);

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
        // ***********   CONSULTA DOENÇA ==> CÓDIGO       ******************
        // *****************************************************************

        public Doenca consultaDoenca(int codigo)
        {
            try
            {
                doenca = new Doenca();
                doenca.gsCodigo = codigo;

                doencaBD = new DoencaBD();
                doenca = doencaBD.consultaDoenca(doenca);

                this.resultado = true;

                verificaErro();
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = "aqui" + e.Message;
            }

            return doenca;
        }

        // *****************************************************************
        // ***********            ALTERA DOENÇA           ******************
        // *****************************************************************

        public void alteraDoenca(int codigo, string nome, string caracteristicas, string diagnostico)
        {
            try
            {
                doenca = new Doenca();
                doenca.gsCodigo = codigo;
                doenca.gsNome = nome;
                doenca.gsCaracteristicas = caracteristicas;
                doenca.gsDiagnostico = diagnostico;

                doencaBD = new DoencaBD();
                doencaBD.alteraDoenca(doenca);

                this.resultado = true;

                verificaErro();
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = "aqui" + e.Message;
            }
        }

        // *****************************************************************
        // ***********     ALTERA IMAGEM DA DOENÇA        ******************
        // *****************************************************************

        public void alteraImgDoenca(int codDoenca, string imagem)
        {
            try
            {
                doenca = new Doenca();
                doenca.gsCodigo = codDoenca;
                doenca.gsImagem = (!imagem.Equals("") ? imagem : null);

                doencaBD = new DoencaBD();
                doencaBD.alteraImgDoenca(doenca);

                resultado = true;
                verificaErro();
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = "aqui" + e.Message;
            }
        }
    }
}
