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
    /// Interaction logic for SimpleTextPage.xaml
    /// </summary>
    public partial class SimpleTextPage : Window
    {
        public string Text { get; set; }

        public SimpleTextPage()
        {
            InitializeComponent();
            this.Loaded += SimpleTextPage_Loaded;
        }

        private void SimpleTextPage_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
        }

        private void FinishEditing(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
