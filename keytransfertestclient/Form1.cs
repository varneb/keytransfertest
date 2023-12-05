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
            _communication.ConnectF += CommHandler_ConnectF;
        }
        //this function would be out of the form.
        private void CommHandler_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            //_ = ((CommHandler)sender).SendMessageAsync(e.Message);
            string message = e.Message;
            // Handle the received message in your WinForm
            UpdateUIWithMessage(message);
        }
        private void UpdateUIWithMessage(string message)
        {
            // Update your WinForm UI controls with the received message
            if (textBox1.InvokeRequired)
            {
                //textBox1.BeginInvoke(new Action(() => textBox1.Text = "Updated asynchronously from non-UI thread"));
                textBox1.Invoke(new Action(() => textBox1.Text += "Updated from non-UI thread" + message));

            }
            else
            {
                textBox1.Text += "Updated from UI thread" + message;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _ = _communication.ConnectToServerAsync("127.0.0.1", 5001);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _ = _communication.SendMessageAsync("hello");
        }
        private void CommHandler_ConnectF(object sender, EventArgs e)
        {
            MessageBox.Show("FFFFFFFFFFFFF");
        }
    }
}
