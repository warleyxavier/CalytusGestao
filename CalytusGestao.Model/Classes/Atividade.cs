using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalytusGestao.Model.Classes
{
    public class Atividade
    {
        // *****************************************************************
        // ***************** DECLARAÇÃO DOS ATRIBUTOS **********************
        // *****************************************************************

        private int codigo;
        private int Plantacao;
        private double custos;
        private double ganhos;
        private string data;
        private string relatorio;
        private int codTipo;
        private string tipo;

        // *****************************************************************
        // *****************      PROPRIEDADES        **********************
        // *****************************************************************

        public int gsCodigo {
            get => codigo;
            set => codigo = value;
        }

        public double gsCustos {
            get => custos;
            set => custos = value;
        }

        public double gsGanhos {
            get => ganhos;
            set => ganhos = value;
        }

        public string gsRelatorio {
            get => relatorio;
            set => relatorio = value;
        }

        public int gsCodTipo {
            get => codTipo;
            set => codTipo = value;
        }

        public string gsTipo {
            get => tipo;
            set => tipo = value;
        }

        public string gsData {
            get => data;
            set => data = value;
        }

        public int gsPlantacao {
            get => Plantacao;
            set => Plantacao = value;
        }
    }
}
