using System;
using System.ComponentModel;
using System.Net;
using System.Windows.Forms;
using VkNet;
using VkNet.Enums.Filters;

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

        VkApi Authentication(string login, string pwd)
        {
            try
            {
                Api = new VkApi();

                //Func<string> code = () =>
                //{
                //    BackgroundWorker bw = new BackgroundWorker();
                //    string value = string.Empty;
                //    AuthCode code_window = new AuthCode();
                //    code_window.Show();

                //    bw.DoWork += (s, e) =>
                //    {
                //        while (Application.OpenForms.Count > 1)
                //            Application.DoEvents();

                //        value = code_window.Code;
                //    };

                //    bw.RunWorkerAsync();

                //    while (bw.IsBusy) Application.DoEvents();

                //    return value;
                //};

                Api.Authorize(new ApiAuthParams
                {
                    ApplicationId = 6169126,
                    Login = login,
                    Password = pwd,
                    Settings = Settings.All,
                    Host = "31.14.133.91",
                    Port = 8080
                    //TwoFactorAuthorization = code
                });
            }
            catch (VkNet.Exception.CaptchaNeededException ex)
            {
                var img = ex.Img;
                WebClient wc = new WebClient();
                wc.DownloadFile(new Uri(img.AbsoluteUri), "./captcha.jpg");
                AuthCode captcha = new AuthCode(Api, login, pwd, ex.Sid);
                captcha.Show();

            }

            return Api;
            
        }


    }
}
