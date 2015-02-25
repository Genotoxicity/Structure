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
    /// Interaction logic for AddressUI.xaml
    /// </summary>
    partial class AddressWindow : Window
    {
        private ProjectEntities projectEntities;
        private Dictionary<string, Location> locationByName;

        public AddressWindow(ProjectEntities projectEntities, Dictionary<string, Location> locationByName)
        {
            InitializeComponent();
            this.projectEntities = projectEntities;
            this.locationByName = locationByName;
            List<string> locationNames = locationByName.Keys.ToList();
            locationNames.Sort(new ProELib.Strings.NaturalSortingComparer());
            AddressListBox.ItemsSource = locationNames;
        }

        private void DevicesButton_Click(object sender, RoutedEventArgs e)
        {
            string location = AddressListBox.SelectedItem as string;
            if (!String.IsNullOrEmpty(location))
                new DevicesWindow(projectEntities, locationByName[location]).Show(); 
        }
    }
}
