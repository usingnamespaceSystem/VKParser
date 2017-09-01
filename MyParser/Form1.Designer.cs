namespace MyParser
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.login = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pwd = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel_auth = new System.Windows.Forms.Panel();
            this.AlbumName = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panel_parse = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.close_button = new System.Windows.Forms.Button();
            this.roll_button = new System.Windows.Forms.Button();
            this.panel_auth.SuspendLayout();
            this.panel_parse.SuspendLayout();
            this.SuspendLayout();
            // 
            // login
            // 
            this.login.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.login.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.login.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.login.Location = new System.Drawing.Point(52, 19);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(143, 13);
            this.login.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(139)))), ((int)(((byte)(199)))));
            this.label1.Location = new System.Drawing.Point(3, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Логин";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(139)))), ((int)(((byte)(199)))));
            this.label2.Location = new System.Drawing.Point(3, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Пароль";
            // 
            // pwd
            // 
            this.pwd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pwd.Location = new System.Drawing.Point(52, 45);
            this.pwd.Name = "pwd";
            this.pwd.PasswordChar = '*';
            this.pwd.Size = new System.Drawing.Size(143, 13);
            this.pwd.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(139)))), ((int)(((byte)(199)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button1.Location = new System.Drawing.Point(64, 68);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Войти";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.auth_Click);
            // 
            // panel_auth
            // 
            this.panel_auth.BackColor = System.Drawing.Color.Transparent;
            this.panel_auth.Controls.Add(this.label1);
            this.panel_auth.Controls.Add(this.button1);
            this.panel_auth.Controls.Add(this.login);
            this.panel_auth.Controls.Add(this.label2);
            this.panel_auth.Controls.Add(this.pwd);
            this.panel_auth.Location = new System.Drawing.Point(218, 128);
            this.panel_auth.Name = "panel_auth";
            this.panel_auth.Size = new System.Drawing.Size(200, 100);
            this.panel_auth.TabIndex = 5;
            // 
            // AlbumName
            // 
            this.AlbumName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AlbumName.Location = new System.Drawing.Point(17, 29);
            this.AlbumName.Name = "AlbumName";
            this.AlbumName.Size = new System.Drawing.Size(200, 13);
            this.AlbumName.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(139)))), ((int)(((byte)(199)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(17, 55);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Загрузить";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.download_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(139)))), ((int)(((byte)(199)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(94, 55);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(123, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "Удалить миниатюры";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel_parse
            // 
            this.panel_parse.BackColor = System.Drawing.Color.Transparent;
            this.panel_parse.Controls.Add(this.label3);
            this.panel_parse.Controls.Add(this.button3);
            this.panel_parse.Controls.Add(this.AlbumName);
            this.panel_parse.Controls.Add(this.button2);
            this.panel_parse.Location = new System.Drawing.Point(12, 25);
            this.panel_parse.Name = "panel_parse";
            this.panel_parse.Size = new System.Drawing.Size(232, 90);
            this.panel_parse.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(139)))), ((int)(((byte)(199)))));
            this.label3.Location = new System.Drawing.Point(14, 8);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label3.Size = new System.Drawing.Size(145, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Введите ссылку на альбом";
            // 
            // close_button
            // 
            this.close_button.BackColor = System.Drawing.Color.Transparent;
            this.close_button.FlatAppearance.BorderSize = 0;
            this.close_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.close_button.Image = ((System.Drawing.Image)(resources.GetObject("close_button.Image")));
            this.close_button.Location = new System.Drawing.Point(597, 5);
            this.close_button.Name = "close_button";
            this.close_button.Size = new System.Drawing.Size(15, 15);
            this.close_button.TabIndex = 9;
            this.close_button.UseVisualStyleBackColor = false;
            this.close_button.Click += new System.EventHandler(this.close_button_Click);
            // 
            // roll_button
            // 
            this.roll_button.BackColor = System.Drawing.Color.Transparent;
            this.roll_button.FlatAppearance.BorderSize = 0;
            this.roll_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roll_button.Image = ((System.Drawing.Image)(resources.GetObject("roll_button.Image")));
            this.roll_button.Location = new System.Drawing.Point(576, 5);
            this.roll_button.Name = "roll_button";
            this.roll_button.Size = new System.Drawing.Size(15, 15);
            this.roll_button.TabIndex = 10;
            this.roll_button.UseVisualStyleBackColor = false;
            this.roll_button.Click += new System.EventHandler(this.roll_button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MyParser.Properties.Resources.back_parse;
            this.ClientSize = new System.Drawing.Size(618, 373);
            this.Controls.Add(this.roll_button);
            this.Controls.Add(this.close_button);
            this.Controls.Add(this.panel_parse);
            this.Controls.Add(this.panel_auth);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "MyParser";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form_MouseUp);
            this.panel_auth.ResumeLayout(false);
            this.panel_auth.PerformLayout();
            this.panel_parse.ResumeLayout(false);
            this.panel_parse.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox login;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox pwd;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel_auth;
        private System.Windows.Forms.TextBox AlbumName;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel_parse;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button close_button;
        private System.Windows.Forms.Button roll_button;
    }
}

