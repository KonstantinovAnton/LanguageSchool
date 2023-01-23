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

namespace LanguagesSchool
{
    /// <summary>
    /// Логика взаимодействия для WindowAdminModeActivate.xaml
    /// </summary>
    public partial class WindowAdminModeActivate : Window
    {
        public WindowAdminModeActivate()
        {
            InitializeComponent();

            if (Classes.GlobalValues.isAdminMode)
                btnActivateMode.Content = "Деактивировать режим";
        }

        private void btnActivateMode_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxAdminCode.Text == "0000")
            {
                if (textBoxAdminCode.Text == "0000")
                {
                    if (Classes.GlobalValues.isAdminMode)
                    {
                        Classes.GlobalValues.isAdminMode = false;
                        MessageBox.Show("Режим администратора деактивирован", "Режим администратора", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        Classes.GlobalValues.isAdminMode = true;
                        MessageBox.Show("Режим администратора активирован", "Режим администратора", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    


                    MainWindow mainWindow = new MainWindow();

                    mainWindow.Show();

                    foreach (Window w in App.Current.Windows)
                    {
                        if (w != mainWindow)
                            w.Close();
                    }
                }

            }
            else
            {
                MessageBox.Show("Неверный код", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();

            mainWindow.Show();

            foreach (Window w in App.Current.Windows)
            {
                if (w != mainWindow)
                    w.Close();
            }
        }
    }
}
