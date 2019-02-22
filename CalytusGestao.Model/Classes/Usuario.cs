using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalytusGestao.Model.Classes
{
    public class Usuario
    {
        // *****************************************************************
        // ***************** DECLARAÇÃO DOS ATRIBUTOS **********************
        // *****************************************************************

        private int codigo;
        private string nome = null;
        private string usuario = null;
        private string senha = null;
        private string tipo = null;

        private string erro = null;
        private bool resultado;

        // *****************************************************************
        // *********************** PROPRIEDADES ****************************
        // *****************************************************************

        public int gsCodigo
        {
            set { this.codigo = value; }
            get { return this.codigo; }
        }

        public string gsNome {
            get => nome;
            set => nome = value;
        }

        public string gsUsuario {
            get => usuario;
            set => usuario = value;
        }

        public string gsSenha {
            get => senha;
            set => senha = value;
        }

        public string gsTipo
        {
            get => tipo;
            set => tipo = value;
        }

        public string getErro()
        {
            return this.erro;
        }

        public bool getResultado()
        {
            return this.resultado;
        }
    }
}
