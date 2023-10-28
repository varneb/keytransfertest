using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace keytransferserver
{
    internal class Program
    {
        private const int portNo = 5001;
        private const string ipAddress = "127.0.0.1";

        static void Main(string[] args)
        {
            System.Net.IPAddress localAdd = System.Net.IPAddress.Parse(ipAddress);

            TcpListener listener = new TcpListener(localAdd, portNo);

            Console.WriteLine("SkipGame Server");
            Console.WriteLine("Listening to ip {0} port: {1}", ipAddress, portNo);
            Console.WriteLine("Server is ready.");

            // Start listening to incoming connection requests
            listener.Start();
            // infinite loop.
            while (true)
            {
                // AcceptTcpClient - Blocking call
                // Execute will not continue until a connection is established

                // create an instance of PlayerActions so the server will be able to 
                // serve multiple clients at the same time.
                TcpClient client = listener.AcceptTcpClient();
                CommHandler handler = new CommHandler(client);

                TcpClient clientB = listener.AcceptTcpClient();
                CommHandler handlerB = new CommHandler(clientB);
                _ = handler.SendMessageAsync("otherclientconnected ");
                //Console.ReadLine();
            }
        }
    }
}
