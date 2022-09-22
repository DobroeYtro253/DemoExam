using Demo_ekzamen.Classes;
using Demo_ekzamen.layouts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo_ekzamen
{
    public partial class Form1 : Form
    {
        String log, pass, text = "";
        int attempt = 3;
        public Form1()
        {
            Helper.AddConnect();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
        }

        private void buttonGuest_Click(object sender, EventArgs e)
        {
            Helper.Role = 0;
            Katalog ka = new Katalog();
            this.Hide();
            ka.ShowDialog();
            this.Show();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            log = textBoxLogin.Text;
            pass = textBoxPassword.Text;
            SqlCommand com = Helper.con.CreateCommand();
            string command = $"SELECT * FROM [User] WHERE UserLogin = '{log}' AND UserPassword = '{pass}';";
            com.CommandText = command;
            SqlDataReader read = com.ExecuteReader();
            read.Read();
           
            if (textBoxCaptha.Text == text)
            {
               
                try
                {
                    Helper.Role = (int)read["UserRole"];
                    MessageBox.Show("Вход в систему выполнен успешно", "Вход", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    read.Close();
                    Katalog ka = new Katalog();
                    this.Hide();
                    ka.ShowDialog();
                    this.Show();
                    pictureboxCaptha.Visible = false;
                    textBoxCaptha.Visible = false;
                    labelCaptha.Visible = false;
                    textBoxCaptha.Text = "";
                    text = "";
                }
                catch (Exception ex)
                {
                    attempt--;
                    MessageBox.Show($"Введите правильно логин и пароль. Осталось {attempt} попыток", "Вход", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (attempt == 0)
                    {
                        this.Close();
                    }
                    read.Close();
                    textBoxCaptha.Text = "";
                    pictureboxCaptha.Visible = true;
                    textBoxCaptha.Visible = true;
                    labelCaptha.Visible = true;

                    pictureboxCaptha.Image = CreateImage(pictureboxCaptha.Width, pictureboxCaptha.Height);
                    return;
                }
            }
            else
            {
                MessageBox.Show("введите капчу", "Вход", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                read.Close();
                return;
            }

            read.Close();

        }

        private Bitmap CreateImage(int Width, int Height)
        {
            Random rnd = new Random();

            Bitmap result = new Bitmap(Width, Height);

            int x = rnd.Next(0, Width - 120);

            Brush[] colors = { Brushes.Black,
                     Brushes.Red,
                     Brushes.Green,
                     Brushes.Blue };

            Graphics g = Graphics.FromImage((Image)result);

            g.Clear(Color.Gray);

            text = String.Empty;
            string ALF = "1234567890QWERTYUIOPASDFGHJKLZXCVBNM";
            String[] txt = new String[4];
            Random r = new Random();
            for (int i = 0; i < 4; ++i)
            {
                txt[i] = Convert.ToString(ALF[rnd.Next(ALF.Length)]);
                g.DrawString(txt[i], new Font("Blackoak Std", rnd.Next(10, 16)), colors[rnd.Next(colors.Length)], new PointF(x , rnd.Next(15, Height - 15)));
                x += 30;
                text += txt[i];
            }

           
            for (int i = 0; i < Width; ++i)
                for (int j = 0; j < Height; ++j)
                    if (rnd.Next() % 20 == 0)
                        result.SetPixel(i, j, Color.White);

            return result;
        }
    }
}
