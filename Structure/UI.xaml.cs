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
using ProELib;

namespace Structure
{
    public partial class UI : Window
    {
        private E3ApplicationInfo applicationInfo;
        public UI()
        {
            applicationInfo = new E3ApplicationInfo();
            InitializeComponent();
            MinHeight = Height;
            MinWidth = Width;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            if (applicationInfo.Status == SelectionStatus.Selected)
                richTextBox.AppendText(applicationInfo.MainWindowTitle);
            else
            {
                richTextBox.AppendText(applicationInfo.StatusReasonDescription);
                ActionButton.IsEnabled = false;
            }
        }
        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            new Script().Start(applicationInfo.ProcessId);
            Cursor = Cursors.Arrow;
        }
    }
}
