using keytransferserver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keytransfertestclient
{
    internal class WinFormHandler
    {

        private CommHandler commHandler;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        public WinFormHandler(CommHandler commHandler)
        {
            this.commHandler = commHandler;
            commHandler.MessageReceived += CommHandler_MessageReceived;
        }

        private void CommHandler_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            // Handle the received message in this WinForm class.
            string message = e.Message;
            // Your custom message handling logic here.


            // Trigger the event to notify the WinForm
            OnMessageReceived(message);

        }
        protected virtual void OnMessageReceived(string message)
        {
            MessageReceived?.Invoke(this, new MessageReceivedEventArgs(message));
        }
    }
}
