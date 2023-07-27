using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Configuration;
using System.Security.Cryptography;
using System.Security.Policy;

namespace AISDetaly
{
    /// <summary>
    /// Логика взаимодействия для NewSupply.xaml
    /// </summary>
    public partial class NewSupply : Window
    {
        string connectionString;
        public NewSupply()
        {
            InitializeComponent();
            //для подключения бд
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        private void ToMain_Click(object sender, RoutedEventArgs e)
        {
            InStock ins = new InStock();
            ins.Show();
            this.Close();
        }

        private void AddNew_Click(object sender, RoutedEventArgs e)
        {
            string sql = "AddSupply";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                //поиск ид детали по названию
                string query = "SELECT code FROM Details WHERE detail=N'" + detail.SelectedItem.ToString() + "'";
                SqlCommand cmd = new SqlCommand(query, connection);
                int detailId = Convert.ToInt32(cmd.ExecuteScalar());

                //id поставщика
                query = "SELECT code FROM Providers WHERE name=N'" + provider.SelectedItem.ToString() + "'";
                cmd = new SqlCommand(query, connection);
                int providerId = Convert.ToInt32(cmd.ExecuteScalar());

                SqlCommand command = new SqlCommand(sql, connection);


                //хранимая процедура
                try
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@delivery_date", Convert.ToDateTime(deliveryDate.Text));
                    command.Parameters.AddWithValue("@detail_code", detailId);
                    command.Parameters.AddWithValue("@provider_code", providerId);
                    command.Parameters.AddWithValue("@price", price.Text);
                    command.Parameters.AddWithValue("@count", count.Text);
                    command.ExecuteNonQuery();

                    cmd = new SqlCommand("UPDATE Leftovers SET count=count +" + count.Text + " WHERE detail_code=" + detailId, connection);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Поставка добавлена");

                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }

            InStock ins = new InStock();
            ins.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //в datepicker ставим по умолчанию текущую дату
            deliveryDate.Text = DateTime.Today.ToShortDateString();
            //заполнение комбобоксов
            List<string> detailList = new List<string>();
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT detail FROM Details", connection);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    detailList.Add(sqlDataReader.GetString(0));   
                }
            }

            List<string> providerList = new List<string>();
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT name FROM Providers", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    providerList.Add(reader.GetString(0));
                }
            }

            provider.ItemsSource = providerList;
            detail.ItemsSource= detailList;
        }
    }
}
