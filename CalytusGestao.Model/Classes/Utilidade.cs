using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalytusGestao.Model.Classes
{
    public class Utilidade
    {
        // *****************************************************************
        // ***************** DECLARAÇÃO DOS ATRIBUTOS **********************
        // *****************************************************************

        private int codigo;
        private string utilidade;

        // *****************************************************************
        // *********************** PROPRIEDADES ****************************
        // *****************************************************************

        public int gsCodigo {
            get => codigo;
            set => codigo = value;
        }

        public string gsUtilidade {
            get => utilidade;
            set => utilidade = value;
        }
    }
}
