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

namespace LanguagesSchool.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageService.xaml
    /// </summary>
    public partial class PageService : Page
    {
        public PageService()
        {
            InitializeComponent();
            listViewService.ItemsSource = Base.EM.Service.ToList();

            comboBoxSort.SelectedIndex = 0;
            comboBoxDiscount.SelectedIndex = 0;

            textBlockCountShow.Text = Base.EM.Service.ToList().Count.ToString();
            textBlockAllCount.Text = Base.EM.Service.ToList().Count.ToString();
        }

        
        // Метод, проверяющий режим администратора и отоброжающий кнопки
        private void showButton(Button btn)
        {
            if (Classes.GlobalValues.isAdminMode)
            {
                btn.Visibility = Visibility.Visible;
            }
            else
            {
                btn.Visibility = Visibility.Collapsed;
            }
        }

        private void btnEditService_Loaded(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            showButton(btn);
           
        }

        private void btnDeleteService_Loaded(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            showButton(btn);
        }

        private void btnSaleService_Loaded(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            showButton(btn);
        }

        private void btnAddService_Loaded(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            showButton(btn);
        }

        private void btnUpcomingEntries_Loaded(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            showButton(btn);
        }

        // Метод сортировки и фильтрации
        private void sortData()
        {
            List<Service> listService = new List<Service>();
           
            // Фильтрация по скидке

            switch(comboBoxDiscount.SelectedIndex)
            {
                case 0:
                    listService = Base.EM.Service.ToList();
                    break;
                case 1:
                    listService = Base.EM.Service.Where(x => x.Discount >= 0 && x.Discount < 5).ToList();
                    break;
                case 2:
                    listService = Base.EM.Service.Where(x => x.Discount >= 5 && x.Discount < 15).ToList();
                    break;
                case 3:
                    listService = Base.EM.Service.Where(x => x.Discount >= 15 && x.Discount < 30).ToList();
                    break;
                case 4:
                    listService = Base.EM.Service.Where(x => x.Discount >= 30 && x.Discount < 70).ToList();
                    break;
                case 5:
                    listService = Base.EM.Service.Where(x => x.Discount >= 70 && x.Discount < 100).ToList();
                    break;
            }

            // Фильтрация по имени

            if (textBoxSearchName.Text != " " && textBoxSearchName.Text != "") 
            {
                listService = listService.Where(x => x.Title.ToLower().Contains(textBoxSearchName.Text.ToLower())).ToList();
            }

            // Фильтрация по описанию

            if (textBoxSearchDescription.Text != " " && textBoxSearchDescription.Text != "")
            {
                listService = listService.Where(x => x.Title.ToLower().Contains(textBoxSearchDescription.Text.ToLower())).ToList();
            }

            // Сортировка по возрастанию и убыванию
            if (comboBoxSort.SelectedIndex == 1)
            {
                
                listService.Sort((x, y) => x.CurrentPrice.CompareTo(y.CurrentPrice));
                
            }
            else if(comboBoxSort.SelectedIndex == 2)
            {
                listService.Sort((x, y) => x.CurrentPrice.CompareTo(y.CurrentPrice));
                listService.Reverse();
            }

            // Отображение листа в ListView

            listViewService.ItemsSource = listService;

            // Вывод кол-ва туров

            textBlockCountShow.Text = listService.Count.ToString();
            textBlockAllCount.Text = Base.EM.Service.ToList().Count.ToString();
        }

        private void comboBoxSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sortData();
        }

        // Метод, проверяющий режим администратора и отоброжающий текст. блоки
        private void showTextBlock(TextBlock tb)
        {
            if (tb.Uid != null)
            {
                tb.Visibility = Visibility.Visible;
            }
            else
            {
                tb.Visibility = Visibility.Hidden;
            }
        }

        private void tbBasePrice_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            showTextBlock(textBlock);
        }

        private void tbDiscount_Loaded(object sender, RoutedEventArgs e)
        {
                TextBlock textBlock = (TextBlock)sender;
                showTextBlock(textBlock);
         }

        private void comboBoxDiscount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sortData();
        }

        private void textBoxSearchName_TextChanged(object sender, TextChangedEventArgs e)
        {
            sortData();
        }

        private void textBoxSearchDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            sortData();
        }

        private void btnEditService_Click(object sender, RoutedEventArgs e)
        {
           Button btn = (Button)sender;
            
           Classes.GlobalValues.idService = Convert.ToInt32(btn.Uid);

           NavigationService.Navigate(new Pages.PageEditService());
        }

        private void btnAddService_Click(object sender, RoutedEventArgs e)
        {
            Classes.GlobalValues.idService = -1;

            NavigationService.Navigate(new Pages.PageEditService());
        }


        // Удаление услуги
        private void btnDeleteService_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            int idService = Convert.ToInt32(btn.Uid);
            Service serviceDelete = Base.EM.Service.FirstOrDefault(x => x.ID == idService);

            if (MessageBox.Show("Удалить услугу " + serviceDelete.Title + "?", "Удаление услуги", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {

                try
                {
                    Base.EM.Service.Remove(serviceDelete);
                    Base.EM.SaveChanges();

                    MessageBox.Show("Услуга успешно удалена", "Удаление услуги",
                               MessageBoxButton.OK, MessageBoxImage.Information);

                    NavigationService.Navigate(new Pages.PageService());
                }
                catch
                {
                    MessageBox.Show("Услуга не была удалена. Попробуйте позже", "Удаление услуги",
                               MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
   
        }
    }
}
