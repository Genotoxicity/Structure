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
        public AddressWindow(Dictionary<string, Location> locationByName)
        {
            InitializeComponent();
            List<string> locationNames = locationByName.Keys.ToList();
            locationNames.Sort(new ProELib.Strings.NaturalSortingComparer());
            AddressListBox.ItemsSource = locationNames;
        }
    }
}
