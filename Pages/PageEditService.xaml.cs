using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Логика взаимодействия для PageEditService.xaml
    /// </summary>
    public partial class PageEditService : Page
    {

        Service service;

        public PageEditService()
        {
            InitializeComponent();

            if (Classes.GlobalValues.idService != -1)
             { 
               service = Base.EM.Service.FirstOrDefault(x => x.ID == Classes.GlobalValues.idService);
               loadData();
             }
            else
            {
                buttonSaveChanges.Content = "Добавить услугу";
            }

        }

        // Подгрузка данных из бд
        private void loadData()
        {

            textBoxNameService.Text = service.Title;
            textBoxPrice.Text = Convert.ToString(service.Cost);
            textBoxDuration.Text = Convert.ToString(service.DurationInSeconds / 60);
            textBoxDiscount.Text = Convert.ToString(service.Discount);
            textBoxDescription.Text = service.Description;

        }

        private void buttonGotoPageService_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.PageService());
        }

       
        // Валидация данных
        private bool isCorrectData()
        {
            if (textBoxNameService.Text == "" || textBoxNameService.Text == " ")
            {
                MessageBox.Show("Введите название услуги", "Некорректные данные",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            try
            {
                Convert.ToDecimal(textBoxPrice.Text);
            }
            catch
            {
                MessageBox.Show("Некорректная цена услуги", "Некорректные данные",
                 MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (Convert.ToDecimal(textBoxPrice.Text) <= 0)
            {
                MessageBox.Show("Цена услуги должна быть больше нуля", "Некорректные данные",
                MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            try
            {
                Convert.ToInt32(textBoxDuration.Text);
            }
            catch
            {
                MessageBox.Show("Введите корректную длительность услуги", "Некорректные данные",
              MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
                
            }
            if(Convert.ToInt32(textBoxDuration.Text) <= 0)
            {
                MessageBox.Show("Длительность должна быть больше нуля", "Некорректные данные",
              MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }


            if (textBoxDiscount.Text != "" && textBoxDiscount.Text != " ")
            {

                try
                {
                    Convert.ToInt32(textBoxDiscount.Text);
                }
                catch
                {
                    MessageBox.Show("Введите корректную скидку услуги", "Некорректные данные",
                 MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;

                }

                if (Convert.ToInt32(textBoxDiscount.Text) < 0 || Convert.ToInt32(textBoxDiscount.Text) >= 100)
                {
                    MessageBox.Show("Скидка должна быть не меньше нуля и меньше ста", "Некорректные данные",
                  MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }

            return true;
        }

        // Сохранение изменений
        private void buttonSaveChanges_Click(object sender, RoutedEventArgs e)
        {
           
                if (isCorrectData())
                {
                if (Classes.GlobalValues.idService != -1)
                {
                    try
                    {
                        service.Title = textBoxNameService.Text;
                        service.Cost = Convert.ToDecimal(textBoxPrice.Text);
                        service.DurationInSeconds = Convert.ToInt32(textBoxDuration.Text) * 60;
                        if (textBoxDiscount.Text != "" && textBoxDiscount.Text != " ")
                            service.Discount = Convert.ToInt32(textBoxDiscount.Text);
                        else
                            service.Discount = 0;

                        service.Description = textBoxDescription.Text;

                        Base.EM.SaveChanges();

                        MessageBox.Show("Услуга успешно изменена", "Изменение услуги", MessageBoxButton.OK, MessageBoxImage.Information);
                        NavigationService.Navigate(new Pages.PageService());
                    }
                    catch
                    {
                        MessageBox.Show("Услуга не была изменена. Попробуйте позже", "Изменение услуги", MessageBoxButton.OK, MessageBoxImage.Error);
                        NavigationService.Navigate(new Pages.PageService());
                    }
                }
                else
                {
                    try
                    {
                        Service addService = new Service();

                        addService.Title = textBoxNameService.Text;
                        addService.Cost = Convert.ToDecimal(textBoxPrice.Text);
                        addService.DurationInSeconds = Convert.ToInt32(textBoxDuration.Text) * 60;
                        if (textBoxDiscount.Text != "" && textBoxDiscount.Text != " ")
                            addService.Discount = Convert.ToInt32(textBoxDiscount.Text);
                        else
                            addService.Discount = 0;

                        addService.Description = textBoxDescription.Text;
                        if(path != "")
                            addService.MainImagePath = path;

                        Base.EM.Service.Add(addService);
                        Base.EM.SaveChanges();

                        MessageBox.Show("Услуга успешно добавлена", "Добавление услуги",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        NavigationService.Navigate(new Pages.PageService());
                    }
                    catch
                    {
                        MessageBox.Show("Услуга не была добавлена. Попробуйте позже", "Добавление услуги",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        NavigationService.Navigate(new Pages.PageService());
                    }

                }

            }
            }


        string path;


        // Добавление изображения
        private void buttonAddImage_Click(object sender, RoutedEventArgs e)
        {
            Service serviceImage = Base.EM.Service.FirstOrDefault(x => x.ID == Classes.GlobalValues.idService);
            

            OpenFileDialog OFD = new OpenFileDialog();  // создаем диалоговое окно
                                                        //OFD.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);  // выбор папки для открытия
            OFD.ShowDialog();  // открываем диалоговое окно  

            try
            {
                path = OFD.FileName;  // извлекаем полный путь к картинке
                string[] arrayPath = path.Split('\\');  // разделяем путь к картинке в массив
                path = "\\" + arrayPath[arrayPath.Length - 3] + "\\" + arrayPath[arrayPath.Length - 2] + "\\" + arrayPath[arrayPath.Length - 1];  // записываем в бд путь, начиная с имени папки

                MessageBox.Show("Изображение успешно добавлено", "Добавление изображение", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch
            {
                MessageBox.Show("Изображение не было добавлено. Попробуйте позже", "Добавление изображения", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
    }

