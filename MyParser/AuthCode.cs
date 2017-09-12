using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace MyParser
{
    public partial class AuthCode : Form
    {
        public AuthCode(VkNet.VkApi api, string login, string pwd, long? sid)
        {
            InitializeComponent();

            if (File.Exists("./captcha.jpg"))
                pictureBox1.Image = Image.FromFile("./captcha.jpg");
            else
                pictureBox1.Image = Image.FromFile("./captcha_new.jpg");

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
            try
            {
                Code = textBox1.Text;
                Api.Authorize(new VkNet.ApiAuthParams
                {
                    ApplicationId = 6169126,
                    Settings = VkNet.Enums.Filters.Settings.All,
                    CaptchaKey = Code,
                    CaptchaSid = Sid,
                    Host = "91.73.131.254",
                    Port = 8080,
                    Login = Login,
                    Password = Pwd
                });
                Close();
            }
            catch (VkNet.Exception.CaptchaNeededException ex)
            {
                var img = ex.Img;
                WebClient wc = new WebClient();
                wc.DownloadFile(new Uri(img.AbsoluteUri), "./captcha_new.jpg");
                AuthCode captcha = new AuthCode(Api, Login, Pwd, ex.Sid);
                captcha.Show();
                Api = captcha.Api;
                Close();
            }
            catch{}
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
