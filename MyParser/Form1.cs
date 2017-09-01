using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using VkNet;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.RequestParams;
using Microsoft.Office.Interop.Excel;
using System.Net;
using Microsoft.Office.Core;
using System.IO;
using System.Diagnostics;

namespace MyParser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public VkApi Api { get; set; }

        Microsoft.Office.Interop.Excel.Application app;
        Workbook wb;
        Worksheet ws;

        private void auth_Click(object sender, EventArgs e)
        {
            try
            {
                Auth logIn = new Auth(login.Text, pwd.Text);
                Api = logIn.Api;
            }
            catch
            {
                MessageBox.Show("Ошибка при входе");
            }
        }

        private void download_Click(object sender, EventArgs e)
        {
            try
            {
                Regex uri = new Regex(@".+_(\d+)");
                string album_id = uri.Matches(AlbumName.Text)[0].Groups[1].ToString();

                OpenFileDialog fd = new OpenFileDialog();
                string path;

                int rowIdx = 1, id_col = 0, opt_col = 0, rozn_col = 0, desc_col = 0, img_col = 0, url_col = 0;

                if (fd.ShowDialog(this) == DialogResult.OK)
                {
                    path = fd.InitialDirectory + fd.FileName;
                    app = new Microsoft.Office.Interop.Excel.Application();
                    wb = app.Workbooks.Open(path);
                    ws = wb.Worksheets[1];

                    while (ws.Cells[rowIdx, 2].Value != null)
                    {
                        rowIdx++;
                    }

                    for (int i = 1; i < 20; i++)
                    {
                        if ((ws.Cells[1, i] as Range).Value == "Миниатюра")
                            img_col = i;

                        else if ((ws.Cells[1, i] as Range).Value == "Ссылка_изображения")
                            url_col = i;

                        else if ((ws.Cells[1, i] as Range).Value == "Уникальный_идентификатор")
                            id_col = i;

                        else if ((ws.Cells[1, i] as Range).Value == "Оптовая_цена")
                            opt_col = i;

                        else if ((ws.Cells[1, i] as Range).Value == "Цена")
                            rozn_col = i;

                        else if ((ws.Cells[1, i] as Range).Value == "Описание")
                            desc_col = i;
                    }

                    var photos_in_album = Api.Photo.Get(new PhotoGetParams
                    {
                        AlbumId = PhotoAlbumType.Id(Convert.ToInt64(album_id))
                    });

                    ws.Rows[1].EntireRow.RowHeight = ws.Rows[2].EntireRow.RowHeight;

                    int count = 0;

                    foreach (var photo in photos_in_album)
                    {
                        if (count > 5) return;

                        string[] all_description = new string[2];
                        all_description = photo.Text.Split(new string[] { "\n" }, StringSplitOptions.None);
                        Regex price = new Regex(@"(\d+)р.");
                        string opt = price.Matches(all_description[0])[0].Groups[1].ToString();
                        string rozn = price.Matches(all_description[1])[0].Groups[1].ToString();
                        string description = all_description[2];

                        ws.Rows[rowIdx].EntireRow.RowHeight = ws.Rows[2].EntireRow.RowHeight;

                        using (WebClient client = new WebClient())
                        {
                            string path_to_image = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), rowIdx.ToString() + ".jpg");
                            client.DownloadFile(new Uri(photo.Photo604.ToString()), path_to_image);

                            ws.Shapes.AddPicture(path_to_image, MsoTriState.msoFalse, MsoTriState.msoCTrue, ws.Columns[img_col].Left, (rowIdx - 1) * ws.Rows[2].EntireRow.RowHeight + 2, photo.Width / (photo.Height / ws.Rows[2].EntireRow.RowHeight), ws.Rows[2].EntireRow.RowHeight - 2);

                            if (File.Exists(path_to_image))
                                File.Delete(path_to_image);
                        }

                        ws.Cells[rowIdx, id_col] = Convert.ToInt32(ws.Cells[rowIdx - 1, id_col].Value) + 1;
                        ws.Cells[rowIdx, url_col] = photo.Photo604.ToString();
                        ws.Cells[rowIdx, opt_col] = opt;
                        ws.Cells[rowIdx, rozn_col] = rozn;
                        ws.Cells[rowIdx, desc_col] = description;
                        rowIdx++;
                        count++;
                    }

                    ws.Rows[1].EntireRow.AutoFit();
                }
            }
            catch
            {
                MessageBox.Show("Неверный файл или строка");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                foreach (var process in Process.GetProcessesByName("EXCEL"))
                {
                    process.Kill();
                }
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (var shape in app.ActiveSheet.Shapes)
                shape.Delete();

            wb.Save();
            wb.Close(0);
            app.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
        }
    }
}
