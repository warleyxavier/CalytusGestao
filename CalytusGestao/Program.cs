using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CalytusGestao.View;
using System.IO;
using CalytusGestao.Model.ClassesBD;

namespace CalytusGestao
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // *****************************************************************
            // ******** VERIFICAÇÃO DA EXISTENCIA DAS PASTAS DO SISTEMA ********
            // *****************************************************************

            Banco banco = new Banco();

            if(!File.Exists(banco.getEndereco() + @"\Banco\Banco.db"))
            {
                banco.criaPastas();
                banco.criaBanco();

                if(!banco.getResultado())
                {
                    MessageBox.Show("Erro: " + banco.getErro(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }


            // *****************************************************************
            // ***************** INICIALIZAÇÃO DO SISTEMA **********************
            // *****************************************************************

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }
    }
}