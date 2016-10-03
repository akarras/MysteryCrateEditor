using MysteryCrateEditor.Libraries.MysteryCrate.Stats;
using MysteryCrateEditor.Libraries.MysteryCrates;
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
    /// Interaction logic for StatsPreview.xaml
    /// </summary>
    public partial class StatsPreview : Window
    {
        public Crate Crate { get; set; }
        public StatsPreview()
        {
            InitializeComponent();
        }

        private void StatsPageLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = Crate.getStats();
        }
    }
}
