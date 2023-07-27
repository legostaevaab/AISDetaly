using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AISDetaly
{
    /// <summary>
    /// Логика взаимодействия для ChangeDetail.xaml
    /// </summary>
    public partial class ChangeDetail : Window
    {
        string connectionString;
        object id = App.Current.Properties["code"];
        bool isFound = false;
        public ChangeDetail()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        private void ToMain_Click(object sender, RoutedEventArgs e)
        {
            DetailsList dl = new DetailsList();
            dl.Show();
            this.Close();
        }

        void UpdateStock(string detail)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using (connection)
            {

                SqlCommand command = new SqlCommand("AddToStock", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@detail", detail);
                command.ExecuteNonQuery();
            }
        }

        private void AddNew_Click(object sender, RoutedEventArgs e)
        {


            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            
            if (!isFound)
            {
                
                string sql = "AddDetail";
                using (connection)
                {
                    SqlCommand command = new SqlCommand(sql, connection);
                    //хранимая процедура
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@detail", detailTB.Text);
                    command.Parameters.AddWithValue("@material", materialTB.Text);
                    command.Parameters.AddWithValue("@color", colorTB.Text);
                    command.Parameters.AddWithValue("@other", otherTB.Text);
                    command.Parameters.AddWithValue("@size", sizeTB.Text);

                    command.ExecuteNonQuery();

                    

                    MessageBox.Show("Запись сохранена");
                }
                string detail = detailTB.Text;
                UpdateStock(detail);
            }
            else
            {
                string sql = "ChangeDetail";
                using (connection)
                {
                    SqlCommand command = new SqlCommand(sql, connection);
                    //хранимая процедура
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", Convert.ToInt32(id));
                    command.Parameters.AddWithValue("@newname", detailTB.Text);
                    command.Parameters.AddWithValue("@newmaterial", materialTB.Text);
                    command.Parameters.AddWithValue("@newcolor", colorTB.Text);
                    command.Parameters.AddWithValue("@newother", otherTB.Text);
                    command.Parameters.AddWithValue("@newsize", sizeTB.Text);

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
                
                string request = "SELECT * FROM Details WHERE code = " + Convert.ToInt32(id);
                SqlCommand comreq = new SqlCommand(request, connection);
                SqlDataReader sdr = comreq.ExecuteReader();
                sdr.Read();

                detailTB.Text = sdr[1].ToString();
                materialTB.Text = sdr[2].ToString();
                colorTB.Text = sdr[3].ToString();
                otherTB.Text = sdr[4].ToString();
                sizeTB.Text = sdr[5].ToString();
            }

            SqlDataAdapter da = new SqlDataAdapter("SELECT detail FROM Details WHERE detail =N'" + detailTB.Text + "'", connectionString);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
                isFound = false;
            else
                isFound = true;
        }
    }
}
