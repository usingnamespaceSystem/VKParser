using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using VkNet;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.RequestParams;
using System.Linq;


namespace MyParser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            panel_parse.Visible = false;
        }

        public VkApi Api { get; set; }
        private bool isDragging = false;
        private System.Drawing.Point lastCursor;
        private System.Drawing.Point lastForm;
        Microsoft.Office.Interop.Excel.Application app;
        Workbook wb;
        Worksheet ws;
        AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();

        private void auth_Click(object sender, EventArgs e)
        {
            autoComplete.Add(login.Text);

            Auth logIn = new Auth(login.Text, pwd.Text);         
            Api = logIn.Api;
            
            //panel_auth.Visible = false;
            //panel_parse.Location = new System.Drawing.Point(200, 131);
            //panel_parse.Visible = true;
        }


        private void download_Click(object sender, EventArgs e)
        {
            if (Api == null)
                return;

            try
            {
                Regex uri = new Regex(@".+_(\d+)");
                string album_id = uri.Matches(AlbumName.Text)[0].Groups[1].ToString();

                OpenFileDialog fd = new OpenFileDialog();
                string path;

                int rowIdx = 1, id_col = 0, opt_col = 0, rozn_col = 0, desc_col = 0, img_col = 0, url_col = 0;
                string opt = string.Empty, rozn = string.Empty;

                if (fd.ShowDialog(this) == DialogResult.OK)
                {
                    path = fd.InitialDirectory + fd.FileName;

                    try
                    {
                        app = new Microsoft.Office.Interop.Excel.Application();
                        wb = app.Workbooks.Open(path);
                        ws = wb.Worksheets[1];
                    }

                    catch
                    {
                        MessageBox.Show("Произошел сбой программы Excel, повторите попытку");
                        return;
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

                    if (img_col == 0 || url_col == 0 || id_col == 0 || opt_col == 0 || rozn_col == 0 || desc_col == 0)
                    {
                        MessageBox.Show("Не найден один из заголовков, проверьте правильность заполнения Excel-файла");
                        return;
                    }

                    while (ws.Cells[rowIdx, id_col].Value != null)
                    {
                        rowIdx++;
                    }

                    VkNet.Utils.VkCollection<VkNet.Model.Attachments.Photo> photos_in_album;

                    try
                    {
                        photos_in_album = Api.Photo.Get(new PhotoGetParams
                        {
                            AlbumId = PhotoAlbumType.Id(Convert.ToInt64(album_id))
                        });
                    }

                    catch
                    {
                        MessageBox.Show("Произошел сбой при загрузке фото, повторите попытку");
                        return;
                    }

                    ws.Rows[1].EntireRow.RowHeight = ws.Rows[2].EntireRow.RowHeight;

                    int count = 0;

                    foreach (var photo in photos_in_album)
                    {
                        try
                        {
                            if (count > 5) return;

                            string[] all_description = new string[2];
                            all_description = photo.Text.Split(new string[] { "\n" }, StringSplitOptions.None);

                            try
                            {
                                Regex o_price = new Regex(@".+пт (\d+)р.");
                                Regex r_price = new Regex(@".+оз (\d+)р.");
                                opt = o_price.Matches(all_description[0])[0].Groups[1].ToString();
                                rozn = r_price.Matches(all_description[0])[0].Groups[1].ToString();
                            }
                            catch
                            {
                                MessageBox.Show("Произошел сбой при поиске цены товара. Товар " + ws.Cells[rowIdx, id_col] + " добавлен без стоимости");
                            }

                            string description = string.Empty;

                            for (int i = 1; i < all_description.Length; i++)
                            {
                                description += all_description[i];
                            }

                            ws.Rows[rowIdx].EntireRow.RowHeight = ws.Rows[2].EntireRow.RowHeight;

                            using (WebClient client = new WebClient())
                            {
                                try
                                {
                                    string path_to_image = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), rowIdx.ToString() + ".jpg");
                                    client.DownloadFile(new Uri(photo.Photo604.ToString()), path_to_image);

                                    ws.Shapes.AddPicture(path_to_image, MsoTriState.msoFalse, MsoTriState.msoCTrue, ws.Columns[img_col].Left, (rowIdx - 1) * ws.Rows[2].EntireRow.RowHeight + 2, photo.Width / (photo.Height / ws.Rows[2].EntireRow.RowHeight), ws.Rows[2].EntireRow.RowHeight - 2);

                                    if (File.Exists(path_to_image))
                                        File.Delete(path_to_image);
                                }
                                catch
                                {
                                    MessageBox.Show("Произошел сбой при загрузке миниатюры. Товар " + ws.Cells[rowIdx, id_col] + " добавлен, но без миниатюры");
                                }
                            }

                            ws.Cells[rowIdx, id_col] = Convert.ToInt32(ws.Cells[rowIdx - 1, id_col].Value) + 1;
                            ws.Cells[rowIdx, url_col] = photo.Photo604.ToString();
                            ws.Cells[rowIdx, opt_col] = opt;
                            ws.Cells[rowIdx, rozn_col] = rozn;
                            ws.Cells[rowIdx, desc_col] = description;
                            rowIdx++;
                            count++;
                            wb.Save();

                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            MessageBox.Show("Описание не подходит под шаблон");
                            continue;
                        }
                    }

                    ws.Rows[1].EntireRow.AutoFit();
                    wb.Activate();
                    }

                app.Visible = true;
            }

            catch (WebException)
            {
                MessageBox.Show("Разрыв соединения");
            }
            //catch
            //{
            //    MessageBox.Show("Неверный файл или строка");
            //}
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                using (StreamWriter tw = new StreamWriter(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ParserHistory.prs")))
                {
                    foreach (string s in autoComplete)
                    {
                        tw.WriteLine(s);
                    }
                }

                foreach (var process in Process.GetProcessesByName("EXCEL"))
                {
                    process.Kill();
                }

                if (File.Exists("./captcha.jpg"))
                    File.Delete("./captcha.jpg");
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var shape in app.ActiveSheet.Shapes)
                    shape.Delete();
            }
            catch { MessageBox.Show("Не удалось удалить миниатюры"); }
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            try
            {
                wb.Close(0);
                app.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                System.Windows.Forms.Application.Exit();
            }
            catch
            {
                System.Windows.Forms.Application.Exit();
            }
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
                this.Location = System.Drawing.Point.Add(lastForm, new Size(System.Drawing.Point.Subtract(Cursor.Position, new Size(lastCursor))));
            }
        }

        private void Form_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string path_to_file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ParserHistory.prs");

            if (!File.Exists(path_to_file))
                File.Create(path_to_file);
            else
            {
                using (StreamReader tw = new StreamReader(path_to_file))
                {
                    string content = tw.ReadToEnd();
                    string[] strings = content.Split('\n');
                    string[] strings_wo_dups = strings.Distinct().ToArray();

                    autoComplete.AddRange(strings_wo_dups);
                }

                login.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                login.AutoCompleteSource = AutoCompleteSource.CustomSource;
                login.AutoCompleteCustomSource = autoComplete;
            }
        }
    }
}
