using keytransferserver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace keytransfertestclient
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            TcpClient client = new TcpClient();
            CommHandler communicationhandler = new CommHandler(client);
//            WinFormHandler formHandler = new WinFormHandler(communicationhandler);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(communicationhandler));
        }
    }
}
