using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace CalytusGestao.Model.Classes
{
    public class Controle
    {
        // *****************************************************************
        // ****** DECLARAÇÃO DOS ATRIBUTOS BANCO DE DADOS ******************
        // *****************************************************************

        protected SQLiteConnection conexao = null;
        protected SQLiteCommand command = null;
        protected SQLiteDataReader leitor = null;

        // *****************************************************************
        // ***************** DECLARAÇÃO DOS ATRIBUTOS **********************
        // *****************************************************************

        protected bool resultado = false;
        protected string erro;
        protected bool dados = false;

        // *****************************************************************
        // *********************** PROPRIEDADES ****************************
        // *****************************************************************

        public bool getResultado()
        {
            return resultado;
        }

        public string getErro()
        {
            return erro;
        }

        public bool getDados()
        {
            return dados;
        }
    }
}
