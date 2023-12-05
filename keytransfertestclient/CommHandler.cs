using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace keytransferserver
{
    internal class CommHandler
    {

        //test
        public event EventHandler ConnectF;

        public bool IsConnected { get; private set; } // Read-only property
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        protected TcpClient client;
        private byte[] buffer;

        public CommHandler(TcpClient client)
        {
            IsConnected = false;
            this.client = client;
            this.buffer = new byte[client.ReceiveBufferSize];
        }

        public async Task ConnectToServerAsync(string ipAddress, int port)
        {
            try
            {
                await client.ConnectAsync(ipAddress, port);
                IsConnected = true;

                // Now that you're connected, you can start receiving messages.
                await Task.Run(() =>ReceiveMessageAsync());
            }
            catch (Exception e)
            {
                ConnectF?.Invoke(this, new EventArgs());
                Console.WriteLine($"Error connecting to {ipAddress}:{port}: {e}");
            }
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
        private void DecodeMessage(string message)
        {
            // Your message decoding logic goes here.

            Console.WriteLine(message);
            OnMessageReceived(message);
            
        }
        protected virtual void OnMessageReceived(string message)
        {
            MessageReceived?.Invoke(this, new MessageReceivedEventArgs(message));
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
    public class MessageReceivedEventArgs : EventArgs
    {
        public string Message { get; }

        public MessageReceivedEventArgs(string message)
        {
            Message = message;
        }
    }
}
