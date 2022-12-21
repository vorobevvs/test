using Microsoft.Office.Interop.Excel;
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
using Excel = Microsoft.Office.Interop.Excel;

namespace BezGranits
{
    /// <summary>
    /// Логика взаимодействия для ReportsPage.xaml
    /// </summary>
    public partial class ReportsPage : System.Windows.Controls.Page
    {
        public ReportsPage()
        {
            InitializeComponent();
            string[] months = { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };
            cmbMonths.ItemsSource = months;
            cmbMonths.SelectedIndex = 0;
            GridReports.ItemsSource = DB.GetContext().Lesson.Where(x => x.StartTime.Month == cmbMonths.SelectedIndex + 1).Where(x => x.StartTime.Year - DateTime.Today.Year <= 12).ToList();
        }

        private void cmbMonths_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GridReports.ItemsSource = DB.GetContext().Lesson.Where(x => x.StartTime.Month == cmbMonths.SelectedIndex + 1).ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Экспортировать отчет в Excel?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Excel.Application excel = new Excel.Application();
                excel.Visible = true;
                Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Worksheet sheet1 = (Worksheet)workbook.Sheets[1];

                for (int j = 0; j < GridReports.Columns.Count; j++)
                {
                    Range myRange = (Range)sheet1.Cells[1, j + 1];
                    sheet1.Cells[1, j + 1].Font.Bold = true;
                    sheet1.Columns[j + 1].ColumnWidth = 15;
                    myRange.Value2 = GridReports.Columns[j].Header;
                }
                for (int i = 0; i < GridReports.Columns.Count; i++)
                {
                    for (int j = 0; j < GridReports.Items.Count; j++)
                    {
                        TextBlock b = GridReports.Columns[i].GetCellContent(GridReports.Items[j]) as TextBlock;
                        Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                        myRange.Value2 = b.Text;
                    }
                }
                sheet1.Columns.AutoFit();
            }
        }
    }
}
