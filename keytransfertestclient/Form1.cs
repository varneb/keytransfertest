using keytransferserver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace keytransfertestclient
{
    internal partial class Form1 : Form
    {
        private CommHandler _communication;
        public Form1(CommHandler communication)
        {
            this._communication = communication;
            InitializeComponent();
            _communication.MessageReceived += CommHandler_MessageReceived;
        }
        private void CommHandler_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            string message = e.Message;
            // Handle the received message in your WinForm
            UpdateUIWithMessage(message);
        }
        private void UpdateUIWithMessage(string message)
        {
            // Update your WinForm UI controls with the received message
            this.textBox1.Text += message;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _ = _communication.ConnectToServerAsync("127.0.0.1", 5001);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _ = _communication.SendMessageAsync("hellp");
        }
    }
}
