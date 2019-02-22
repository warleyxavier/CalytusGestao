using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalytusGestao.Model.Classes
{
    public class Plantacao
    {
        // *****************************************************************
        // ***************** DECLARAÇÃO DOS ATRIBUTOS **********************
        // *****************************************************************

        private int codigo;
        private string identificacao;
        private int anoPlantacao;
        private int qtdPlantasPlantadas;
        private string localizacao;
        private string municipio;
        private double tamanhoArea;
        private int qtdPlantasAtual;
        private int idade;
        private double custos;
        private double ganhos;
        private string status;
        private string imagem;

        // *****************************************************************
        // *********************** PROPRIEDADES ****************************
        // *****************************************************************

        public int Codigo {
            get => codigo;
            set => codigo = value;
        }

        public string Identificacao {
            get => identificacao;
            set => identificacao = value;
        }

        public int AnoPlantacao {
            get => anoPlantacao;
            set => anoPlantacao = value;
        }

        public int QtdPlantasPlantadas {
           get => qtdPlantasPlantadas;
            set => qtdPlantasPlantadas = value;
        }

        public string Localizacao {
            get => localizacao;
            set => localizacao = value;
        }

        public string Municipio {
            get => municipio;
            set => municipio = value;
        }

        public double TamanhoArea {
            get => tamanhoArea;
            set => tamanhoArea = value;
        }

        public int QtdPlantasAtual {
            get => qtdPlantasAtual;
            set => qtdPlantasAtual = value;
        }

        public int Idade {
            get => idade;
            set => idade = value;
        }

        public double Custos {
            get => custos;
            set => custos = value;
        }

        public double Ganhos {
            get => ganhos;
            set => ganhos = value;
        }

        public string Status {
            get => status;
            set => status = value;
        }

        public string Imagem {
            get => imagem;
            set => imagem = value;
        }
    }
}
