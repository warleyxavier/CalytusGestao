using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace CalytusGestao.Model.ClassesBD
{
    public class Conexao
    {
        // *****************************************************************
        // ***************** DECLARAÇÃO DOS ATRIBUTOS **********************
        // *****************************************************************

        private string stringConexao = @"Data Source = C:\Users\Public\Documents\Excellence\CalytusGestao\Banco\Banco.db";

        private string erro = null;
        private bool resultado;

        // *****************************************************************
        // *********************** PROPRIEDADES ****************************
        // *****************************************************************

        public string getErro()
        {
            return erro;
        }

        public bool getResultado()
        {
            return resultado;
        }

        // *****************************************************************
        // *******************   CRIAÇÃO DE CONEXÃO  ***********************
        // *****************************************************************

        public SQLiteConnection conexao()
        {
            SQLiteConnection conexao = null;

            try
            {
                conexao = new SQLiteConnection();
                conexao.ConnectionString = stringConexao;
                conexao.Open();
                resultado = true;
            }
            catch(SQLiteException e)
            {
                erro = e.Message;
                resultado = false;
            }

            return conexao;
            conexao.Close();
        }

        // *****************************************************************
        // *******************   CRIAÇÃO DE COMMAND  ***********************
        // *****************************************************************

        public SQLiteCommand command(string sql, SQLiteConnection conexao)
        {
            SQLiteCommand command = null;

            try
            {
                command = new SQLiteCommand();
                command.CommandText = sql;
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conexao;
            }
            catch (SQLiteException e)
            {
                erro = e.Message;
                resultado = false;
            }

            return command;
        }
    }
}
