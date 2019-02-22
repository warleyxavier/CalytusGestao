using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalytusGestao.Model.Classes
{
    public class Especie
    {
        // *****************************************************************
        // ***************** DECLARAÇÃO DOS ATRIBUTOS **********************
        // *****************************************************************

        private int codigo;
        private string nomePopular;
        private string nomeCientifico;
        private string caractesristicas;
        private string imagem;

        // *****************************************************************
        // *********************** PROPRIEDADES ****************************
        // *****************************************************************

        public int gsCodigo {
            get => codigo;
            set => codigo = value;
        }

        public string gsNomePopular {
            get => nomePopular;
            set => nomePopular = value;
        }

        public string gsNomeCientifico {
            get => nomeCientifico;
            set => nomeCientifico = value;
        }

        public string gsCaractesristicas {
            get => caractesristicas;
            set => caractesristicas = value;
        }

        public string gsImagem {
            get => imagem;
            set => imagem = value;
        }

        // *****************************************************************
        // *************         CÁLCULO DE CALAGEM        *****************
        // *****************************************************************

        public double calcCalagem(Analise analise)
        {
            double resultado = 0;

            if(analise.IndSaturacao <= 40)
            {
                resultado = (analise.CtcT * (40 - analise.IndSaturacao)) / 100;
            }

            return resultado;
        }

        // ************************************************************************************************
        // ***********************************         ADUBAÇÃO        ************************************
        // ************************************************************************************************

        // *****************************************************************
        // *************             FÓSFORO               *****************
        // *****************************************************************

        public int calcFosforo(int produtividade, double fosforo, string solo)
        {
            int resultado = 0;

            double nivelCritico = 0;

            if(produtividade.Equals(20))
            {
                nivelCritico = 4.3;
            }
            else if (produtividade.Equals(30))
            {
                nivelCritico = 4.3;
            }
            else if (produtividade.Equals(40))
            {
                nivelCritico = 4.4;
            }
            else if (produtividade.Equals(50))
            {
                nivelCritico = 4.5;
            }

            if(fosforo > nivelCritico)
            {
                if(solo.Equals("Arenoso"))
                {
                    resultado = 100;
                }
                else if(solo.Equals("Textura Média"))
                {
                    resultado = 125;
                }
                else if(solo.Equals("Argiloso"))
                {
                    resultado = 150;
                }
            }
            else if(fosforo < nivelCritico/2)
            {
                resultado = 600;
            }
            else
            {
                resultado = 300;
            }

            return resultado;
        }

        // *****************************************************************
        // *************             POTÁSSIO               ****************
        // *****************************************************************

        public double calcPotassio(double potassio, int produtividade)
        {
            double resultado = 0;

            double nivelCritico = 0;

            if (produtividade.Equals(20))
            {
                nivelCritico = 45;
            }
            else if (produtividade.Equals(30))
            {
                nivelCritico = 60;
            }
            else if (produtividade.Equals(40))
            {
                nivelCritico = 75;
            }
            else if (produtividade.Equals(50))
            {
                nivelCritico = 90;
            }

            if(potassio < nivelCritico)
            {
                double diferenca = nivelCritico - potassio;
                resultado = diferenca * 1.8;
            }

            return resultado;
        }
    }
}
