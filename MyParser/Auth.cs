using System;
using System.ComponentModel;
using System.Net;
using System.Windows.Forms;
using VkNet;
using VkNet.Enums.Filters;
using ModernDev.InTouch.API;
using System.IO;

namespace MyParser
{
    class Auth
    {
        public VkApi Api { get; set; }

        public Auth(string login, string pwd)
        {
            Api = Authentication(login, pwd);
        }

        public bool AuthUsing2FA { get; set; } = false;
        int count_auth = 0;

        VkApi Authentication(string login, string pwd)
        {
            count_auth = 0;
            try
            {
                Api = new VkApi();

                Api.Authorize(new ApiAuthParams
                {
                    ApplicationId = 6169126,
                    Settings = Settings.All,
                    Host = "91.73.131.254",
                    Port = 8080,
                    Login = login,
                    Password = pwd
                });
            }
            catch (VkNet.Exception.CaptchaNeededException ex)
            {
                var img = ex.Img;
                WebClient wc = new WebClient();
                wc.DownloadFile(new Uri(img.AbsoluteUri), "./captcha.jpg");
                AuthCode captcha = new AuthCode(Api, login, pwd, ex.Sid);
                captcha.Show();
                Api = captcha.Api;
            }

            catch 
            {
                if (count_auth > 10)
                {
                    MessageBox.Show("Превышено количество попыток соединения");
                    return null;
                }

                Authentication(login, pwd);
                count_auth++;
            }

            
            return Api;
            
        }
    }
}
