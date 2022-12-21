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
    /// Логика взаимодействия для EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : Page
    {
        public EmployeePage()
        {
            InitializeComponent();
            EmployeeGrid.ItemsSource = DB.GetContext().Employee.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditEmployeeWindow window = new AddEditEmployeeWindow(new Employee());
            window.WindowClosed += window_WindowClosed;
            window.Show();
            window.Title = "Добавление сотрудника";
        }

        //Закрытие окна добавления/редактирования
        private void window_WindowClosed(object sender, EventArgs e)
        {
            EmployeeGrid.ItemsSource = DB.GetContext().Employee.ToList();
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранного сторудника?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                try
                {
                    DB.GetContext().Employee.Remove(EmployeeGrid.SelectedItem as Employee);
                    DB.GetContext().SaveChanges();
                    EmployeeGrid.ItemsSource = DB.GetContext().Employee.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        //Открытие окна редактирования
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            AddEditEmployeeWindow window = new AddEditEmployeeWindow(EmployeeGrid.SelectedItem as Employee);
            window.WindowClosed += window_WindowClosed;
            window.Show();
            window.Title = "Редактирование сотрудника";
        }

        //Поиск
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            EmployeeGrid.ItemsSource = DB.GetContext().Employee.Where(x => x.FName.StartsWith(txtSearch.Text)).ToList();
        }
    }
}
