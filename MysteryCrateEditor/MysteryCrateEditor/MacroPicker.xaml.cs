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
    /// Interaction logic for MacroPicker.xaml
    /// </summary>
    public partial class MacroPicker : Window
    {
        public MacroPicker()
        {
            InitializeComponent();

            DataContext = MacroStorage.Storage.Macros;
        }
    }
}
