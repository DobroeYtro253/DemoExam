using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo_ekzamen.Classes
{


    public class Helper
    {
        public static SqlConnection con { get; set; }
        public static int Role { get; set; }
        public static bool AddConnect()
        {
            try
            {
                String connectionString = @"Data Source = DESKTOP-FQEUVM8; Initial Catalog = Demo_ekzamen; Integrated Security = True";
                con = new SqlConnection(connectionString);
                con.Open();
                MessageBox.Show("Подключение к БД прошло успешно", "Подключение", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Ошибка подключения к БД", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public static bool CloseConnect()
        {
            try
            {
                con.Close();
            }
            catch (SqlException ex)
            {
                return false;
            }
            return true;
        }

    }
}
