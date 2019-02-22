using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CalytusGestao.Model.Helpers
{
    public class Midia
    {
        // *****************************************************************
        // ************** REALIZA A CÓPIA DE ARQUIVOS **********************
        // *****************************************************************

        public static void copiaArquivo(string origem, string destino)
        {
            File.Copy(origem, destino, true);
        }

        // *****************************************************************
        // ************** RETORNA O NOME DE UM ARQUIVO *********************
        // *****************************************************************

        public static string nomeArquivoEndereco(string endereco)
        {
            string nomeArquivo = "";

            int indice = endereco.LastIndexOf(@"\") + 1;

            nomeArquivo = endereco.Substring(indice, endereco.Length - indice);

            return nomeArquivo;
        }

        // *****************************************************************
        // **************     REMOVE UM ARQUIVO        *********************
        // *****************************************************************

        public static void removeArquivo(string endereco)
        {
            File.Delete(endereco);
        }

    }
} 
