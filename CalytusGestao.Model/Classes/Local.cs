using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalytusGestao.Model.Classes
{
    public class Local
    {
        // *****************************************************************
        // ***************** DECLARAÇÃO DOS ATRIBUTOS **********************
        // *****************************************************************

        private int codigo;
        private string local;

        // *****************************************************************
        // *********************** PROPRIEDADES ****************************
        // *****************************************************************

        public int gsCodigo {
            get => codigo;
            set => codigo = value;
        }
        public string gsLocal {
            get => local;
            set => local = value;
        }        
    }
}
