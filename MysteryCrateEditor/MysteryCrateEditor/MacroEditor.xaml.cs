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
    /// Interaction logic for Macros.xaml
    /// </summary>
    public partial class MacroEditor : Window
    {
        public MacroEditor()
        {
            InitializeComponent();
            // More testing needs to be done on the macro storage library
            MacroStorage.Storage.Macros.Add(new Libraries.MysteryCrate.Rewards.RewardMacro());
        }
    }
}
