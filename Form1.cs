using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace Pinger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtInput.Text))
            {
                MessageBox.Show("Enter IP or Website");
            }
            else
            {
                toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
                label3.Text = "Running";
                label3.ForeColor = Color.Green;
                timer1 = new Timer { Interval = 1000, Enabled = true };
                timer1.Tick += new EventHandler(PingTest);
                txtResults.Clear();
                btnStart.Enabled = false;
                btnStop.Enabled = true;
            }
        }

        
        public void PingTest(object sender, EventArgs e)
        {
            try
            {
                // Ping's the local machine.
                string host = txtInput.Text;
                Ping pingSender = new Ping();
                PingOptions options = new PingOptions();
                // Create a buffer of 32 bytes of data to be transmitted. 
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);

                // Wait 2 seconds for a reply. 
                int timeout = 2000;

                PingReply p = pingSender.Send(host, timeout, buffer, options);

                if (p.Status == IPStatus.Success)
                {
                    txtResults.Text = "Ping to " + txtInput.Text.ToString() + " [" + p.Address.ToString() + "]" + " Successful" + " Response delay = " + p.RoundtripTime.ToString() + " ms" + "\n";
                }
                else
                {
                    txtResults.Text = p.Status.ToString();
                }
            }
            catch (PingException)
            {
                txtResults.Text = "Host unreachable";
                toolStripProgressBar1.Style = ProgressBarStyle.Continuous;
                label3.Text = "Idle";
                label3.ForeColor = Color.Red;
                btnStart.Enabled = true;
                btnStop.Enabled = false;
                timer1.Stop(); 
            }
        } 

        private void btnStop_Click(object sender, EventArgs e)
        {
            toolStripProgressBar1.Style = ProgressBarStyle.Continuous;
            label3.Text = "Idle";
            label3.ForeColor = Color.Red;
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            timer1.Stop(); 
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            // Visit website
            System.Diagnostics.Process.Start("http://www.bdekker.nl");
        }
    }
}
