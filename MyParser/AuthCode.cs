using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using VkNet.Exception;

namespace MyParser
{
    public partial class AuthCode : Form
    {
        public AuthCode(string login, string pwd, CaptchaNeededException ex)
        {
            InitializeComponent();      

            Api = new VkNet.VkApi();
            Login = login;
            Pwd = pwd;
            Ex = ex;

            var img = Ex.Img;
            WebClient wc = new WebClient();

            try
            {
                wc.DownloadFile(new Uri(img.AbsoluteUri), "./captcha.jpg");
                wc.Dispose();
            }
            catch(Exception ee)
            {
                MessageBox.Show( ee.Message);
                return;
            }
            pictureBox1.Image = Image.FromFile("./captcha.jpg");
            this.Show();
        }

        public string Code { get; set; }
        public VkNet.VkApi Api { get; set; }
        private bool isDragging = false;
        private Point lastCursor;
        private Point lastForm;
        string Login = string.Empty, Pwd = string.Empty;
        CaptchaNeededException Ex;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Code = textBox1.Text;
                Api.Authorize(new VkNet.ApiAuthParams
                {
                    ApplicationId = 6169126,
                    Settings = VkNet.Enums.Filters.Settings.All,
                    CaptchaKey = Code,
                    CaptchaSid = Ex.Sid,
                    Host = "91.73.131.254",
                    Port = 8080,
                    Login = Login,
                    Password = Pwd
                });                         
            }

            catch (Exception ee)
            {
                //MessageBox.Show("Произошла ошибка: " + ee.Message);
                this.pictureBox1.Image.Dispose();
                this.pictureBox1.Image = null ;
                Close();
                File.Delete("./captcha.jpg");
                AuthCode captcha = new AuthCode(Login, Pwd, Ex);
                captcha.Owner = this.Owner;
                Api = captcha.Api;
                
            }

            ((Form1)Application.OpenForms[0]).panel_auth.Visible = false;
            ((Form1)Application.OpenForms[0]).panel_parse.Location = new System.Drawing.Point(200, 131);
            ((Form1)Application.OpenForms[0]).panel_parse.Visible = true;

            Close();
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void roll_button_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
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
                Location = Point.Add(lastForm, new Size(Point.Subtract(Cursor.Position, new Size(lastCursor))));
            }
        }

        private void Form_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }
    }
}
