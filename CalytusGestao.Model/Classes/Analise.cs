using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalytusGestao.Model.Classes
{
    public class Analise
    {
        // *****************************************************************
        // ***************** DECLARAÇÃO DOS ATRIBUTOS **********************
        // *****************************************************************

        private int codigo;
        private int plantacao;
        private double profundidade;
        private string data;
        private double ph;
        private double fosforo;
        private double potassio;
        private double calcio;
        private double magnesio;
        private double aluminio;
        private double ctct;
        private double ctcT;
        private double indSaturacao;
        private double pRem;
        private string imagem;

        // *****************************************************************
        // *********************** PROPRIEDADES ****************************
        // *****************************************************************

        public int Codigo {
            get => codigo;
            set => codigo = value;
        }

        public double Profundidade {
            get => profundidade;
            set => profundidade = value;
        }

        public string Data {
            get => data;
            set => data = value;
        }

        public double Ph {
            get => ph;
            set => ph = value;
        }

        public double Fosforo {
            get => fosforo;
            set => fosforo = value;
        }

        public double Potassio {
            get => potassio;
            set => potassio = value;
        }

        public double Calcio {
            get => calcio;
            set => calcio = value;
        }

        public double Magnesio {
            get => magnesio;
            set => magnesio = value;
        }

        public double Aluminio {
            get => aluminio;
            set => aluminio = value;
        }

        public double Ctct {
            get => ctct;
            set => ctct = value;
        }

        public double CtcT {
            get => ctcT;
            set => ctcT = value;
        }

        public double IndSaturacao {
            get => indSaturacao;
            set => indSaturacao = value;
        }

        public double PRem {
            get => pRem;
            set => pRem = value;
        }

        public int Plantacao {
            get => plantacao;
            set => plantacao = value;
        }

        public string Imagem {
            get => imagem;
            set => imagem = value;
        }
    }
}
