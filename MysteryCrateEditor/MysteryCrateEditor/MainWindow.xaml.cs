using MysteryCrateEditor.Libraries.MysteryCrates;
using MysteryCrateEditor.Libraries.Storage;
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

namespace MysteryCrateEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IStorage storage;
        public MainWindow()
        {
            InitializeComponent();
            // Initialize our storage and load our crates from memory
            storage = new JSONStorage();
            updateUI();
        }

        private void updateUI()
        {
            CrateList.ItemsSource = storage.GetCrates();
        }

        private void CrateListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count > 0)
            {
                CratePanel.DataContext = e.AddedItems[0];
            }
        }

        private void AddCrate(object sender, RoutedEventArgs e)
        {
            storage.SaveCrate(new Crate("New Crate"));
            updateUI();
        }

        private void SaveCrate(object sender, RoutedEventArgs e)
        {
            storage.SaveCrate((Crate)CratePanel.DataContext);
            updateUI();
        }
    }
}
