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
    /// Логика взаимодействия для InStock.xaml
    /// </summary>
    public partial class InStock : Window
    {
        string connectionString;
        SqlDataAdapter adapter;
        DataTable detailsTable;
        public InStock()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT Details.detail, Leftovers.count FROM Details, Leftovers WHERE Details.code=Leftovers.detail_code";
            detailsTable = new DataTable();
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);
                connection.Open();
                adapter.Fill(detailsTable);
                leftoversDG.ItemsSource = detailsTable.DefaultView;
            }
            catch
            {
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void ToMain(object sender, RoutedEventArgs e)
        {
            MainPage mp = new MainPage();
            mp.Show();
            this.Close();
        }

        private void Provider(object sender, RoutedEventArgs e)
        {
            Providers pr = new Providers();
            pr.Show();
            this.Close();
        }

        private void Reports(object sender, RoutedEventArgs e)
        {
            Reports rep = new Reports();
            rep.Show();
            this.Close();
        }

        private void Details(object sender, RoutedEventArgs e)
        {
            DetailsList detailsList= new DetailsList();
            detailsList.Show();
            this.Close();
        }

        private void NewSupply(object sender, RoutedEventArgs e)
        {
            NewSupply ns = new NewSupply();
            ns.Show();
            this.Close();
        }

        private void detailsDG_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
