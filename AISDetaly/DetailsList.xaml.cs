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
using System.Windows.Media.Media3D;

namespace AISDetaly
{
    /// <summary>
    /// Логика взаимодействия для DetailsList.xaml
    /// </summary>
    public partial class DetailsList : Window
    {
        string connectionString;
        SqlDataAdapter adapter;
        DataTable detailsTable;
        public DetailsList()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        private void UpdateDB()
        {
            string sql = "SELECT * FROM Details";
            detailsTable = new DataTable();
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);
                connection.Open();
                adapter.Fill(detailsTable);
                detailsDG.ItemsSource = detailsTable.DefaultView;
            }
            catch
            {
            }
            detailsDG.SelectedItem = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainPage mp = new MainPage();
            mp.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDB();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void Change(object sender, RoutedEventArgs e)
        {
            try
            {
                string detailName = ((DataRowView)detailsDG.SelectedItems[0]).Row["detail"].ToString();
                if (detailName != null)
                {
                    SqlConnection connection = new SqlConnection(connectionString);
                    connection.Open();
                    SqlCommand da = new SqlCommand("SELECT code FROM Details WHERE detail = N'" + detailName + "'", connection);
                    object id = da.ExecuteScalar();
                    App.Current.Properties["code"] = id;
                    ChangeDetail cd = new ChangeDetail();
                    cd.Show();
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("Выберите элемент");
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            object id = null;
            App.Current.Properties["code"] = id;
            ChangeDetail cd = new ChangeDetail();
            cd.Show();
            this.Close();
        }

        private void ToMain(object sender, RoutedEventArgs e)
        {
            MainPage mp = new MainPage();
            mp.Show();
            this.Close();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (detailsDG.SelectedItems != null)
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string detailName = ((DataRowView)detailsDG.SelectedItems[0]).Row["detail"].ToString();
                SqlCommand da = new SqlCommand("SELECT code FROM Details WHERE detail = N'" + detailName + "'", connection);
                object id = da.ExecuteScalar();
                string sql = "DetailDelete";
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


        private void Providers(object sender, RoutedEventArgs e)
        {
            Providers p = new Providers();
            p.Show();
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
