using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ProjectClient
{
    public partial class Form1 : Form
    {
       
        public delegate void StartToStart(int port);
        public event StartToStart Start;

        public delegate void Ststop();
        public event Ststop Stop;

        public delegate void SendState(string state);
        public event SendState Send;
        
       
        public Form1()
        {
            Listener listener= new Listener(this);
           
            InitializeComponent();
        }

        public int GetPort
        {
            get
            {
                int result = 0;
                int.TryParse(textBox1.Text, out result);
                return result;
            }
        }
        private void check() //проверка нажатия на checkbox
        {
            int power = checkBox1.Checked ? 1 : 0;
            int cycle = checkBox2.Checked ? 1 : 0;
            int crash = checkBox3.Checked ? 1 : 0;
            Send?.Invoke(String.Concat(power, cycle, crash)); 
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            check();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            check();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            check();
        }

        
        
        private void button1_Click(object sender, EventArgs e)
        {
            Start?.Invoke(GetPort);
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            Stop?.Invoke();
        }

    }
}
