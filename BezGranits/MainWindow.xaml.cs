using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BezGranits
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //open pages in frame
        private void navigateFrame(Page pageInput)
        {
            MainFrame.Navigate(pageInput);
        }

        public MainWindow()
        {
            InitializeComponent();
            navigateFrame(new LessonPage());
        }

        private void btnFrameMenu_Click(object sender, RoutedEventArgs e)
        {
            if (stckNavigationButtons.Width == 200)
            {
                DoubleAnimation widthAnimation = new DoubleAnimation
                {
                    From = 200,
                    To = 0,
                    Duration = TimeSpan.FromSeconds(0.2),
                    AutoReverse = false
                };
                stckNavigationButtons.BeginAnimation(WidthProperty, widthAnimation);
            }
            else
            {
                DoubleAnimation widthAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = 200,
                    Duration = TimeSpan.FromSeconds(0.2),
                    AutoReverse = false
                };
                stckNavigationButtons.BeginAnimation(WidthProperty, widthAnimation);
            }
        }

        private void btnLogo_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://bezgranits-orsk.vsite.biz") { UseShellExecute = true });
        }

        private void btnLesson_Click(object sender, RoutedEventArgs e)
        {
            navigateFrame(new LessonPage());
            txtPageName.Text = "Расписание";
        }

        private void btnClient_Click(object sender, RoutedEventArgs e)
        {
            navigateFrame(new ClientPage());
            txtPageName.Text = "Клиенты";
        }

        private void btnEmployee_Click(object sender, RoutedEventArgs e)
        {
            navigateFrame(new EmployeePage());
            txtPageName.Text = "Сотрудники";
        }

        private void btnService_Click(object sender, RoutedEventArgs e)
        {
            navigateFrame(new ServicePage());
            txtPageName.Text ="Услуги";
        }

        private void btnReports_Click(object sender, RoutedEventArgs e)
        {
            navigateFrame(new ReportsPage());
            txtPageName.Text = "Отчёты";
        }
    }
}
