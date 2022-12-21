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
    /// Логика взаимодействия для AddEditLessonWindow.xaml
    /// </summary>
    public partial class AddEditLessonWindow : Window
    {
        public AddEditLessonWindow(Lesson _lesson)
        {
            InitializeComponent();
            MyLesson = _lesson;
            DataContext = MyLesson;
            cmbEmployee.ItemsSource = DB.GetContext().Employee.OrderBy(x => x.FName).ToList();
            cmbService.ItemsSource = DB.GetContext().Service.OrderBy(x => x.Name).ToList();
            dtpStartTime.Value = DateTime.Now;
        }

        public Lesson MyLesson { get; set; }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(cmbService.Text))
                errors.AppendLine("Выберите урок");
            if (string.IsNullOrEmpty(cmbEmployee.Text))
                errors.AppendLine("Выберите педагога");
            if (string.IsNullOrEmpty(cmbLessonType.Text))
                errors.AppendLine("Выберите тип занятия");
            if (dtpStartTime.Value < DateTime.Now)
                errors.AppendLine("Введите корректные дату и время");
            if (errors.Length > 0)
                MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                try
                {
                    if (MyLesson.Id == 0)
                    {
                        DB.GetContext().Lesson.Add(MyLesson);
                        MessageBox.Show("Урок добавлен");
                    }
                    else
                        MessageBox.Show("Урок отредактирован");
                    DB.GetContext().SaveChanges();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        public event EventHandler WindowClosed;

        private void Window_Closed(object sender, EventArgs e)
        {
            if (WindowClosed != null)
                WindowClosed(this, EventArgs.Empty);
        }
    }
}
