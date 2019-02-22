using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalytusGestao.Model.Classes
{
    public class Doenca
    {
        // *****************************************************************
        // ***************** DECLARAÇÃO DOS ATRIBUTOS **********************
        // *****************************************************************

        private int codigo;
        private string nome;
        private string caracteristicas;
        private string diagnostico;
        private string imagem;

        // *****************************************************************
        // *********************** PROPRIEDADES ****************************
        // *****************************************************************

        public int gsCodigo {
            get => codigo;
            set => codigo = value;
        }

        public string gsNome {
            get => nome;
            set => nome = value;
        }

        public string gsCaracteristicas {
            get => caracteristicas;
            set => caracteristicas = value;
        }

        public string gsDiagnostico {
            get => diagnostico;
            set => diagnostico = value;
        }

        public string gsImagem {
            get => imagem;
            set => imagem = value;
        }    


    }
}
