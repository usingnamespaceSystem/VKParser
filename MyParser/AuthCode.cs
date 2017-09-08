using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyParser
{
    public partial class AuthCode : Form
    {
        public AuthCode(VkNet.VkApi api, string login, string pwd, long? sid)
        {
            InitializeComponent();
            pictureBox1.Image = Image.FromFile("./captcha.jpg");
            Api = new VkNet.VkApi();
            Login = login;
            Pwd = pwd;
            Sid = sid;
        }

        public string Code { get; set; }
        public VkNet.VkApi Api { get; set; }
        private bool isDragging = false;
        private Point lastCursor;
        private Point lastForm;
        string Login = string.Empty, Pwd = string.Empty;
        long? Sid;

        private void button1_Click(object sender, EventArgs e)
        {
            Code = textBox1.Text;
            Api.Authorize(new VkNet.ApiAuthParams
            {
                ApplicationId = 6169126,
                Login = Login,
                Password = Pwd,
                Settings = VkNet.Enums.Filters.Settings.All,
                Host = "31.14.133.91",
                Port = 8080,
                CaptchaKey = Code,
                CaptchaSid = Sid
            });
            Close();

        }

        private void close_button_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void roll_button_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;

            lastCursor = Cursor.Position;
            lastForm = this.Location;
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                this.Location = Point.Add(lastForm, new Size(Point.Subtract(Cursor.Position, new Size(lastCursor))));
            }
        }

        private void Form_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }
    }
}
