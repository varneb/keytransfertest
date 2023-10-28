using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace keytransferserver
{
    internal class CommHandler
    {
        public bool IsConnected { get; private set; } // Read-only property

        protected TcpClient client;
        private byte[] buffer;

        public CommHandler(TcpClient client)
        {
            IsConnected = false;
            this.client = client;
            this.buffer = new byte[client.ReceiveBufferSize];


            //////////////////////////
            StartRead();
        }
        public void StartRead()
        {
            IsConnected = true;
            _ = ReceiveMessageAsync();

        }
        private async Task ReceiveMessageAsync()
        {
            try
            {
                while (IsConnected)
                {
                    int bytesRead = await client.GetStream().ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead > 0)
                    {
                        string msg = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        DecodeMessage(msg);
                    }
                    else
                    {
                        IsConnected = false; // Update IsConnected when the connection is lost
                    }
                }
            }
            catch (Exception e)
            {
                // Handle exceptions, such as network errors or protocol violations.
                Console.WriteLine(e.ToString());
                IsConnected = false;
            }
            finally
            {
                // Perform cleanup or other necessary tasks.
                Close();
            }
        }
        public async Task SendMessageAsync(string msg)
        {
            //Thread.Sleep(3000);
            try
            {
                Console.WriteLine("server -> " + msg);
                byte[] toSendBuffer = Encoding.UTF8.GetBytes(msg + "!");
                await client.GetStream().WriteAsync(toSendBuffer, 0, toSendBuffer.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private async void DecodeMessage(string message)
        {
            // Your message decoding logic goes here.
            Console.WriteLine(message);
            await SendMessageAsync(message);
        }

        private void Close()
        {
            // Cleanup and close resources here.
            if (client != null)
            {
                client.Close();
            }
        }
    }
}
