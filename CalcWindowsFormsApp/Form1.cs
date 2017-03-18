using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FSMCalc;

namespace CalcWindowsFormsApp
{
    public partial class Form1 : Form
    {
        Brain brain = new Brain();
        public void ChangeDisplay(string m)
        {
            textBox1.Text = m;
        }
        public Form1()
        {
            InitializeComponent();
            brain.invoker = ChangeDisplay;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            brain.Processor(b.Text[0]);
        }
    }
}
