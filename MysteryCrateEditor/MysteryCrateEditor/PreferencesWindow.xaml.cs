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
using System.Windows.Shapes;

namespace MysteryCrateEditor
{
    /// <summary>
    /// Interaction logic for PreferencesWindow.xaml
    /// </summary>
    public partial class PreferencesWindow : Window
    {
        Preferences prefs;
        public PreferencesWindow()
        {
            InitializeComponent();
            prefs = Preferences.loadPreferences();
            DataContext = prefs;
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            // TODO Look into using Windows Vista+ style folder pickers instead.
            // Create a FolderBrowserDialog
            using (var folder = new System.Windows.Forms.FolderBrowserDialog())
            {
                // Prompt the user for a dialog
                var folderPickerResult = folder.ShowDialog();
                // Check that the user actually chose OK
                if (folderPickerResult == System.Windows.Forms.DialogResult.OK)
                {
                    // Load the preferences file
                    // Set the default location to the users selected path
                    prefs.DefaultLocation = folder.SelectedPath;
                    // Save the preferences
                    prefs.savePreferences();
                    DataContext = prefs;
                }
            }
        }

        private void BackupsChanged(object sender, TextChangedEventArgs e)
        {
            // Check that the textbox doesn't have a non number value
            int outNumber;
            if(int.TryParse(NumberOfBackupsTextBox.Text, out outNumber))
            {
                prefs.NumberOfBackups = outNumber;
                prefs.savePreferences(); 
            }
        }
    }
}
