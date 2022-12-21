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
    /// Логика взаимодействия для AddEditWindow.xaml
    /// </summary>
    public partial class AddEditClientWindow : Window
    {
        public AddEditClientWindow(Client _client)
        {
            InitializeComponent();
            MyClient = _client;
            DataContext = MyClient;
            dpBirth.SelectedDate = DateTime.Today;
        }

        public Client MyClient { get; set; }

        //Сохранение пользователя
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
                errors.AppendLine("Введите ФИО");
            if (dpBirth.SelectedDate < DateTime.Parse("01.01.1950"))
                errors.AppendLine("Выберите корректную дату рождения");
            if (string.IsNullOrEmpty(cmbSex.Text))
                errors.AppendLine("Выберите пол");
            if (string.IsNullOrWhiteSpace(txtPassport.Text))
                errors.AppendLine("Введите серию номер пасспорта");
            if (errors.Length > 0)
                MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                try
                {
                    if (MyClient.Id == 0)
                    {
                        DB.GetContext().Client.Add(MyClient);
                        MessageBox.Show("Клиент добавлен");
                    }
                    else
                        MessageBox.Show("Клиент отредактирован");
                    DB.GetContext().SaveChanges();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        //Закрытие окна
        public event EventHandler WindowClosed;
        private void Add_EditWindow_Closed(object sender, EventArgs e)
        {
            if (WindowClosed != null)
                WindowClosed(this, EventArgs.Empty);
        }
    }
}
