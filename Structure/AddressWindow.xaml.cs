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
        private E3Project project;
        private List<int> planSheetIds;
        private Dictionary<string, Location> locationByName;

        public AddressWindow(E3Project project, Dictionary<string, Location> locationByName, List<int> planSheetIds)
        {
            InitializeComponent();
            this.project = project;
            this.locationByName = locationByName;
            this.planSheetIds = planSheetIds;
            List<string> locationNames = locationByName.Keys.ToList();
            locationNames.Sort(new ProELib.Strings.NaturalSortingComparer());
            AddressListBox.ItemsSource = locationNames;
        }

        private void DevicesButton_Click(object sender, RoutedEventArgs e)
        {
            string location = AddressListBox.SelectedItem as string;
            if (!String.IsNullOrEmpty(location))
                new DevicesWindow(project, locationByName[location]).Show(); 
        }

        private void SchemeButton_Click(object sender, RoutedEventArgs e)
        {
            string location = AddressListBox.SelectedItem as string;
            if (!String.IsNullOrEmpty(location))
                new StructureScheme(project, locationByName[location], planSheetIds); 
        }
    }
}
