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

            
            //GOAL: have a class created here that will be subscribed to the commhandler, and will create the different forms.
            //this means that the first form will also get created there?
            //basically the idea is that the class will dispatch the message events to the relevant form. but i dont know if its
            //even necessary as when i close forms i unsubscribe them from the message event, and yeah definitely 
            //having a separate class manage what forms to create and when is nice
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(communicationhandler));
        }
    }
}
