using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace AISDetaly
{
    /// <summary>
    /// Логика взаимодействия для ChangeProvider.xaml
    /// </summary>
    public partial class ChangeProvider : Window
    {
        string connectionString;
        object id = App.Current.Properties["code"];
        bool isFound = false;
        public ChangeProvider()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        private void ToMain_Click(object sender, RoutedEventArgs e)
        {
            Providers dl = new Providers();
            dl.Show();
            this.Close();
        }

        private void AddNew_Click(object sender, RoutedEventArgs e)
        {


            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            if (!isFound)
            {

                string sql = "AddProvider";
                using (connection)
                {
                    SqlCommand command = new SqlCommand(sql, connection);
                    //хранимая процедура
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@name", nameTB.Text);
                    command.Parameters.AddWithValue("@employee", employeeTB.Text);
                    command.Parameters.AddWithValue("@city", cityTB.Text);
                    command.Parameters.AddWithValue("@phone", phoneTB.Text);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Запись сохранена");
                }
            }
            else
            {
                string sql = "ChangeProvider";
                using (connection)
                {
                    SqlCommand command = new SqlCommand(sql, connection);
                    //хранимая процедура
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", Convert.ToInt32(id));
                    command.Parameters.AddWithValue("@newname", nameTB.Text);
                    command.Parameters.AddWithValue("@newemployee", employeeTB.Text);
                    command.Parameters.AddWithValue("@newcity", cityTB.Text);
                    command.Parameters.AddWithValue("@newphone", phoneTB.Text);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Запись изменена");
                    id = null;
                }
            }


        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            if (id != null)
            {

                string request = "SELECT * FROM Providers WHERE code = " + Convert.ToInt32(id);
                SqlCommand comreq = new SqlCommand(request, connection);
                SqlDataReader sdr = comreq.ExecuteReader();
                sdr.Read();

                nameTB.Text = sdr[1].ToString();
                employeeTB.Text = sdr[2].ToString();
                cityTB.Text = sdr[3].ToString();
                phoneTB.Text = sdr[4].ToString();
            }

            SqlDataAdapter da = new SqlDataAdapter("SELECT name FROM Providers WHERE name =N'" + nameTB.Text + "'", connectionString);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
                isFound = false;
            else
                isFound = true;
        }
    }
}
