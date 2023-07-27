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

namespace AISDetaly
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Window
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Details(object sender, RoutedEventArgs e)
        {
            DetailsList dl = new DetailsList();
            dl.Show();
            this.Close();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            MainWindow mw= new MainWindow();
            mw.Show();
            this.Close();
        }

        private void Providers(object sender, RoutedEventArgs e)
        {
            Providers pr = new Providers();
            pr.Show();
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
            Reports rt = new Reports();
            rt.Show();
            this.Close();
        }

        private void NewSupply(object sender, RoutedEventArgs e)
        {
            NewSupply ns = new NewSupply();
            ns.Show();
            this.Close();
        }
    }
}
