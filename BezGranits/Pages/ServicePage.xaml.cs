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

namespace BezGranits
{
    /// <summary>
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        public ServicePage()
        {
            InitializeComponent();
            ServiceGrid.ItemsSource = DB.GetContext().Service.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditServiceWindow window = new AddEditServiceWindow(new Service());
            window.WindowClosed += window_WindowClosed;
            window.Show();
            window.Title = "Добавление услуги";
        }

        private void window_WindowClosed(object sender, EventArgs e)
        {
            ServiceGrid.ItemsSource = DB.GetContext().Service.ToList();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            AddEditServiceWindow window = new AddEditServiceWindow(ServiceGrid.SelectedItem as Service);
            window.WindowClosed += window_WindowClosed;
            window.Show();
            window.Title = "Редактирование услуги";
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранную услугу", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                try
                {
                    DB.GetContext().Service.Remove(ServiceGrid.SelectedItem as Service);
                    DB.GetContext().SaveChanges();
                    ServiceGrid.ItemsSource = DB.GetContext().Service.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ServiceGrid.ItemsSource = DB.GetContext().Service.Where(x => x.Name.StartsWith(txtSearch.Text)).ToList();
        }
    }
}
