using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyParser
{
    public partial class AuthCode : Form
    {
        public AuthCode()
        {
            InitializeComponent();
        }

        public string Code { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            Code = textBox1.Text;
            Close();
        }

    }
}
