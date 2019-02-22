using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalytusGestao.Model.Classes;
using CalytusGestao.Model.ClassesBD;

namespace CalytusGestao.Controller.Controller
{
    public class EspecieContr : Controle
    {
        Especie especie = null;
        EspecieBD especieBD = null;

        // *****************************************************************
        // *********** VERIFICAÇÃO DE ERRO NO ACESSO AO BD  ****************
        // *****************************************************************

        public void verificaErro()
        {
            if (!especieBD.getResultado())
            {
                this.resultado = false;
                this.erro = especieBD.getErro();
            }
        }

        // *****************************************************************
        // *************       CADASTRO DE ESPÉCIE       *******************
        // *****************************************************************

        public int cadastraEspecie(string nomePopular, string nomeCientifico, string caracteristicas, string imagem)
        {
            int codEspecie = 0;

            try
            {
                especie = new Especie();
                especie.gsNomePopular = nomePopular;
                especie.gsNomeCientifico = nomeCientifico;
                especie.gsCaractesristicas = caracteristicas;
                especie.gsImagem = (!imagem.Equals("")? @"C:\Users\Public\Documents\Excellence\CalytusGestao\Imagens\Especies\" + imagem : "");

                especieBD = new EspecieBD();
                codEspecie = especieBD.cadastraEspecie(especie);

                resultado = true;

                verificaErro();
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }

            return codEspecie;
        }

        // *****************************************************************
        // *************     CONSULTA ESPÉCIES ==> NOME  *******************
        // *****************************************************************

        public Especie[] consultaEspecies(string nome)
        {
            Especie[] especies = null;
            try
            {
                especie = new Especie();
                especie.gsNomePopular = nome;

                especieBD = new EspecieBD();
                especies = especieBD.consultaEspecies(especie);
                dados = especieBD.getDados();

                resultado = true;
                verificaErro();
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }
            return especies;
        }

        // *****************************************************************
        // *************     REMOVE ESPÉCIES ==> CÓDIGO  *******************
        // *****************************************************************

        public void removeEspecie(int codigo)
        {
            try
            {
                especie = new Especie();
                especie.gsCodigo = codigo;

                especieBD = new EspecieBD();
                especieBD.removeEspecie(especie);

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
        // *************    CONSULTA ESPÉCIE ==> CÓDIGO  *******************
        // *****************************************************************

        public Especie consultaEspecie(int codigo)
        {
            Especie especieDados = null;
            try
            {
                especie = new Especie();
                especie.gsCodigo = codigo;

                especieBD = new EspecieBD();
                especieDados = especieBD.consultaEspecie(especie);

                dados = especieBD.getDados();
                resultado = true;
                verificaErro();
            }
            catch (Exception e)
            {
                this.resultado = false;
                this.erro = e.Message;
            }
            return especieDados;
        }

        // *****************************************************************
        // *************    ALTERAR ESPÉCIE ==> CÓDIGO  ********************
        // *****************************************************************

        public void alterarEspecie(int codigo, string nomePopular, string nomeCientifico, string caracteristicas)
        {
            try
            {
                especie = new Especie();
                especie.gsCodigo = codigo;
                especie.gsNomePopular = nomePopular;
                especie.gsNomeCientifico = nomeCientifico;
                especie.gsCaractesristicas = caracteristicas;

                especieBD = new EspecieBD();
                especieBD.alterarEspecie(especie);

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
        // *********    ALTERAR IMAGEM DA ESPÉCIE ==> CÓDIGO  **************
        // *****************************************************************

        public void alteraImagemEsp(int codEspecie, string imagem)
        {
            try
            {
                especie = new Especie();
                especie.gsCodigo = codEspecie;
                especie.gsImagem = (!imagem.Equals("") ? imagem : null);

                especieBD = new EspecieBD();
                especieBD.alteraImagemEsp(especie);

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
