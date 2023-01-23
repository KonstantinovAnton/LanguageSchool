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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LanguagesSchool
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
           
            InitializeComponent();
            Base.EM = new Entities();
            frm.Navigate(new Pages.PageService());
        }

        private void btnAdminMode_Click(object sender, RoutedEventArgs e)
        {
            WindowAdminModeActivate windowAdminModeActivate = new WindowAdminModeActivate();

            windowAdminModeActivate.Show();

            foreach (Window w in App.Current.Windows)
            {
                if (w != windowAdminModeActivate)
                    w.Close();
            }
        }
    }
}
