using System;
using System.Collections.Generic;
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
using System.Data;
using System.Data.SqlClient;

namespace AISDetaly
{
    
    public partial class MainWindow : Window
    {
        //подключение бд
        string connectionString;
        public MainWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          //поверка на соответствие лоигна и пароля
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT login FROM Users WHERE login ='" + loginTB.Text + "' and password ='" + passwordTB.Password + "'", connectionString);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Неверный логин или пароль");
                connection.Close();
            }
            else
            {
                MainPage mp = new MainPage();
                mp.Show();
                connection.Close();
                this.Close();
                
            }

            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void loginTB_MouseEnter(object sender, MouseEventArgs e)
        {
            if (loginTB.Text == "Имя пользователя")
                loginTB.Text = "";
        }

        private void loginTB_MouseLeave(object sender, MouseEventArgs e)
        {
            if (loginTB.Text == "")
                loginTB.Text = "Имя пользователя";
        }

        private void passwordTB_MouseEnter(object sender, MouseEventArgs e)
        {
            if (passwordTB.Password == "Пароль")
                passwordTB.Password = "";
        }

        private void passwordTB_MouseLeave(object sender, MouseEventArgs e)
        {
            if (passwordTB.Password == "")
                passwordTB.Password = "Пароль";
        }
    }
}
