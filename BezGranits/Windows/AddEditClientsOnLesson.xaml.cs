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

namespace BezGranits
{
    /// <summary>
    /// Логика взаимодействия для AddEditClientsOnLesson.xaml
    /// </summary>
    public partial class AddEditClientsOnLesson : Window
    {
        public AddEditClientsOnLesson(ClientsOnLesson _lesson, int c)
        {
            InitializeComponent();
            ClientGrid.ItemsSource = DB.GetContext().ClientsOnLesson.Where(x => x.IdLesson==c).ToList();
            MyLesson = _lesson;
            DataContext = MyLesson;
            cmbClient.ItemsSource = DB.GetContext().Client.ToList();
            MyLesson.IdLesson = c;
        }

        public ClientsOnLesson MyLesson { get; set; }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Убрать выбранного клиента с занятия?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                try
                {
                    DB.GetContext().ClientsOnLesson.Remove(ClientGrid.SelectedItem as ClientsOnLesson);
                    DB.GetContext().SaveChanges();
                    ClientGrid.ItemsSource = DB.GetContext().ClientsOnLesson.Where(x => x.IdLesson == MyLesson.IdLesson).ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }
        
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(cmbClient.Text))
                MessageBox.Show("Выберите клиента");
            else
                try
                {
                    DB.GetContext().ClientsOnLesson.Add(MyLesson);
                    DB.GetContext().SaveChanges();
                    ClientGrid.ItemsSource = DB.GetContext().ClientsOnLesson.Where(x => x.IdLesson == MyLesson.IdLesson).ToList();
        }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }
    }
}
