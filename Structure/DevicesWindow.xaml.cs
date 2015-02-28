using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ProELib;

namespace Structure
{
    /// <summary>
    /// Interaction logic for DevicesWindow.xaml
    /// </summary>
    public partial class DevicesWindow : Window
    {
        public DevicesWindow(E3Project project, Location location)
        {
            InitializeComponent();
            Title = location.Name;
            FillDevicesListBox(project, location);
        }

        private void FillDevicesListBox(E3Project project, Location location)
        {
            NormalDevice device = project.NormalDevice;
            foreach (int id in location.DeviceIds)
            {
                device.Id = id;
                DevicesListBox.Items.Add(device.Name);
            }
        }
    }
}
