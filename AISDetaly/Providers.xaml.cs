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

namespace AISDetaly
{
    /// <summary>
    /// Логика взаимодействия для Providers.xaml
    /// </summary>
    public partial class Providers : Window
    {
        string connectionString;
        SqlDataAdapter adapter;
        DataTable providersTable;
        public Providers()
        {
            InitializeComponent();
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        private void UpdateDB()
        {
            string sql = "SELECT * FROM Providers";
            providersTable = new DataTable();
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);
                connection.Open();
                adapter.Fill(providersTable);
                providersDG.ItemsSource = providersTable.DefaultView;
            }
            catch
            {
            }
            providersDG.SelectedItem = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDB();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Change(object sender, RoutedEventArgs e)
        {
            try
            {
                string providerName = ((DataRowView)providersDG.SelectedItems[0]).Row["name"].ToString();
                if (providerName != null)
                {
                    SqlConnection connection = new SqlConnection(connectionString);
                    connection.Open();
                    SqlCommand da = new SqlCommand("SELECT code FROM Providers WHERE name = N'" + providerName + "'", connection);
                    object id = da.ExecuteScalar();
                    App.Current.Properties["code"] = id;
                    ChangeProvider cd = new ChangeProvider();
                    cd.Show();
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("Выберите элемент");
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (providersDG.SelectedItems != null)
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string providerName = ((DataRowView)providersDG.SelectedItems[0]).Row["name"].ToString();
                SqlCommand da = new SqlCommand("SELECT code FROM Providers WHERE name = N'" + providerName + "'", connection);
                object id = da.ExecuteScalar();
                string sql = "ProviderDelete";
                using (connection)
                {
                    SqlCommand command = new SqlCommand(sql, connection);
                    //хранимая процедура
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Запись удалена");
                }
                UpdateDB();
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            object id = null;
            App.Current.Properties["code"] = id;
            ChangeProvider cd = new ChangeProvider();
            cd.Show();
            this.Close();
        }

        private void ToMain(object sender, RoutedEventArgs e)
        {
            MainPage mp = new MainPage();
            mp.Show();
            this.Close();
        }

        private void Details(object sender, RoutedEventArgs e)
        {
            DetailsList d = new DetailsList();
            d.Show();
            this.Close();
        }

        private void InStock(object sender, RoutedEventArgs e)
        {
            InStock ins = new InStock();
            ins.Show();
            this.Close();
        }

        private void Reports(object sender, RoutedEventArgs e)
        {
            Reports rep = new Reports();
            rep.Show();
            this.Close();
        }
    }
}
