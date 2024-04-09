using AccountingOfGoods.Windows;
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

namespace AccountingOfGoods.Pages
{
    /// <summary>
    /// Логика взаимодействия для SupportPage.xaml
    /// </summary>
    public partial class SupportPage : Page
    {
        public SupportPage()
        {
            InitializeComponent();
            Support.MouseLeftButtonDown += Support_MouseLeftButtonDown;

        }
        private void Support_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            SupportMessageWindow newWindow = new SupportMessageWindow();
            newWindow.Closed += NewWindow_Closed;
            newWindow.Show();

        }
        private void NewWindow_Closed(object sender, EventArgs e)
        {
            Window.GetWindow(this).Close();
        }
    }
}
