﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
            Api = new VkApi();

            Func<string> code = () =>
            {
                BackgroundWorker bw = new BackgroundWorker();
                string value = string.Empty;
                AuthCode code_window = new AuthCode();
                code_window.Show();

                bw.DoWork += (s, e) =>
                {
                    while (Application.OpenForms.Count > 1)
                        Application.DoEvents();

                    value = code_window.Code;
                };

                bw.RunWorkerAsync();

                while (bw.IsBusy) Application.DoEvents();

                return value;
            };

            Api.Authorize(new ApiAuthParams
            {
                ApplicationId = 6169126,
                Login = "faust_32@inbox.ru",
                Password = "553861p",
                Settings = Settings.All,
                //TwoFactorAuthorization = code
            });

            return Api;
        }

    }
}