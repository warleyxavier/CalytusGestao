using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalytusGestao.Model.Classes
{
    public class Anotacao
    {
        // *****************************************************************
        // ***************** DECLARAÇÃO DOS ATRIBUTOS **********************
        // *****************************************************************

        private int codigo;
        private string assunto;
        private string anotacao;
        private string data;


        // *****************************************************************
        // *********************** PROPRIEDADES ****************************
        // *****************************************************************

        public int gsCodigo
        {
            get => codigo;
            set => codigo = value;
        }

        public string gsAssunto
        {
            get => assunto;
            set => assunto = value;
        }
        public string gsAnotacao
        {
            get => anotacao;
            set => anotacao = value;
        }
        public string gsData
        {
            get => data;
            set => data = value;
        }
    }
}
