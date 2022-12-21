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
    /// Логика взаимодействия для AddEditServiceWindow.xaml
    /// </summary>
    public partial class AddEditServiceWindow : Window
    {
        public AddEditServiceWindow(Service _service)
        {
            InitializeComponent();
            MyService = _service;
            DataContext = MyService;
        }

        public Service MyService { get; set; }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(txtName.Text))
                errors.AppendLine("Введите название");
            if (string.IsNullOrWhiteSpace(txtPrice.Text))
                errors.AppendLine("Введите стоимость");
            if (string.IsNullOrWhiteSpace(txtMinutes.Text))
                errors.AppendLine("Введите длительность занятия");
            if (errors.Length > 0)
                MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                try
                {


                    var time = ((int.Parse(txtMinutes.Text)) / 60).ToString() + ":" + ((int.Parse(txtMinutes.Text)) % 60).ToString();
                    MyService.Duration = TimeSpan.Parse(time);
                    if (MyService.Id == 0)
                    {
                        DB.GetContext().Service.Add(MyService);
                        MessageBox.Show("Услуга добавлена");
                    }
                    else
                        MessageBox.Show("Услуга отредактирована");
                    DB.GetContext().SaveChanges();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        public event EventHandler WindowClosed;
        private void Add_EditServiceWindow_Closed(object sender, EventArgs e)
        {
            if (WindowClosed != null)
                WindowClosed(this, EventArgs.Empty);
        }
    }
}
