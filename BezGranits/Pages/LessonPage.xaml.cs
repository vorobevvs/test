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
    /// Логика взаимодействия для LessonPage.xaml
    /// </summary>
    public partial class LessonPage : Page
    {
        public LessonPage()
        {
            InitializeComponent();
            cmbSearch.SelectedIndex = 0;
            LessonGrid.ItemsSource = DB.GetContext().Lesson.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditLessonWindow window = new AddEditLessonWindow(new Lesson());
            window.WindowClosed += window_WindowClosed;
            window.Show();
            window.Title = "Добавление урока";
        }

        private void window_WindowClosed(object sender, EventArgs e)
        {
            LessonGrid.ItemsSource = DB.GetContext().Lesson.ToList();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            AddEditLessonWindow window = new AddEditLessonWindow(LessonGrid.SelectedItem as Lesson);
            window.WindowClosed += window_WindowClosed;
            window.Show();
            window.Title = "Редактирование урока";
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            switch(cmbSearch.SelectedIndex)
            {
                case 0: LessonGrid.ItemsSource = DB.GetContext().Lesson.Where(x => x.Service.Name.StartsWith(txtSearch.Text)).ToList(); break;
                case 1: LessonGrid.ItemsSource = DB.GetContext().Lesson.Where(x => x.Employee.FName.StartsWith(txtSearch.Text)).ToList(); break;
                case 3: LessonGrid.ItemsSource = DB.GetContext().Lesson.Where(x => x.StartTime.ToString().StartsWith(txtSearch.Text)).ToList(); break;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранный урок", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                try 
                { 
                    DB.GetContext().Lesson.Remove(LessonGrid.SelectedItem as Lesson);
                    DB.GetContext().SaveChanges();
                    LessonGrid.ItemsSource = DB.GetContext().Lesson.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        private void btnAddCllientOnLesson_Click(object sender, RoutedEventArgs e)
        {
            AddEditClientsOnLesson window = new AddEditClientsOnLesson(new ClientsOnLesson(), (LessonGrid.SelectedItem as Lesson).Id);
            window.Show();
        }

        private void cmbSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cmbSearch.SelectedIndex)
            {
                case 0: LessonGrid.ItemsSource = DB.GetContext().Lesson.Where(x => x.Service.Name.StartsWith(txtSearch.Text)).ToList(); break;
                case 1: LessonGrid.ItemsSource = DB.GetContext().Lesson.Where(x => x.Employee.FName.StartsWith(txtSearch.Text)).ToList(); break;
                case 3: LessonGrid.ItemsSource = DB.GetContext().Lesson.Where(x => x.StartTime.ToString().StartsWith(txtSearch.Text)).ToList(); break;
            }
        }
    }
}
