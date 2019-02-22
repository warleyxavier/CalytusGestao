using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace CalytusGestao.Model.Helpers
{
    public class Helper
    {
        // *****************************************************************
        // ********************* CRIPTOGRAFIA MD5 **************************
        // *****************************************************************

        public static string criptografia(string senha)
        {
            string novaSenha = "";

            MD5 md5 = MD5.Create();
            byte[] caracteres = md5.ComputeHash(Encoding.UTF8.GetBytes(senha));

            for(int i = 0; i < caracteres.Length; i++)
            {
                novaSenha += caracteres[i];
            }

            return novaSenha;
        }

        // *****************************************************************
        // ***********  FORMATA DATA FORMARTO BANCO DE DADOS  **************
        // *****************************************************************

        public static string formataData(string Data)
        {
            string novaData = Data.Substring(6, 4) + "-" + Data.Substring(3,2) + "-" + Data.Substring(0,2);

            return novaData;
        }

    }
}
