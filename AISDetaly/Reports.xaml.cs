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
    /// Логика взаимодействия для Reports.xaml
    /// </summary>
    public partial class Reports : Window
    {
        string connectionString;
        SqlDataAdapter adapter;
        DataTable detailsTable;
        public Reports()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }        

        private void ToMain(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            mainPage.Show();
            this.Close();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void start_MouseEnter(object sender, MouseEventArgs e)
        {
            if(keyword.Text == "Ключевое слово")
            keyword.Text = string.Empty;
        }

        private void Details(object sender, RoutedEventArgs e)
        {
            DetailsList detailsList = new DetailsList();
            detailsList.Show();
            this.Close();
        }

        private void Provider(object sender, RoutedEventArgs e)
        {
            Providers providers = new Providers();
            providers.Show();
            this.Close();
        }

        private void Stock(object sender, RoutedEventArgs e)
        {
            InStock inStock = new InStock();
            inStock.Show();
            this.Close();
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds.Tables.Clear();
            dt.Columns.Clear();
            detailsDG.ItemsSource = null;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();


                string sql = "Search";
            DateTime stdt = Convert.ToDateTime(start.Text);
            DateTime endt = Convert.ToDateTime(end.Text);
            string request = keyword.Text;
            if (!string.IsNullOrEmpty(request)) { 
            }
                using (connection)
                {
                
                
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataAdapter da = new SqlDataAdapter(command);
                    //хранимая процедура
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@deliverydatestart", stdt);
                    command.Parameters.AddWithValue("@deliverydateend", endt);
                    command.Parameters.AddWithValue("@keyword", request);
                da.Fill(dt);
                
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Результатов нет");
                    connection.Close();
                }
                else
                {
                    
                    da.Fill(ds);
                    detailsDG.ItemsSource = ds.Tables[0].DefaultView;
                    connection.Close();
                }
                
                

            }
        }
    }
}
