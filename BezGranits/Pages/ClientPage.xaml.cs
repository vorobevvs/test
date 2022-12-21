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
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        public ClientPage()
        {
            InitializeComponent();
            ClientGrid.ItemsSource = DB.GetContext().Client.ToList();
        }

        //Открытие окна добавления
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditClientWindow window = new AddEditClientWindow(new Client());
            window.WindowClosed += window_WindowClosed;
            window.Show();
            window.Title = "Добавление клиента";
            window.txtPassport.Text = "";
        }

        //Закрытие окна добавления/редактирования
        private void window_WindowClosed(object sender, EventArgs e)
        {
            ClientGrid.ItemsSource = DB.GetContext().Client.ToList();
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранного клиента?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                try
                {
                    DB.GetContext().Client.Remove(ClientGrid.SelectedItem as Client);
                    DB.GetContext().SaveChanges();
                    ClientGrid.ItemsSource = DB.GetContext().Client.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        //Открытие окна редактирования
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            AddEditClientWindow window = new AddEditClientWindow(ClientGrid.SelectedItem as Client);
            window.WindowClosed += window_WindowClosed;
            window.Show();
            window.Title = "Редактирование клиента";
        }

        //Поиск
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ClientGrid.ItemsSource = DB.GetContext().Client.Where(x => x.FName.StartsWith(txtSearch.Text)).ToList();
        }
    }
}
