using MysteryCrateEditor.Libraries.MysteryCrate.Rewards;
using MysteryCrateEditor.Libraries.MysteryCrates;
using MysteryCrateEditor.Libraries.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            int crateListIndex = CrateList.SelectedIndex;
            CrateList.ItemsSource = storage.GetCrates();
            if (CrateList.Items.Count > crateListIndex)
            {
                CrateList.SelectedIndex = crateListIndex;
            }
        }

        private void AddCrate(object sender, RoutedEventArgs e)
        {
            storage.SaveCrate(new Crate("New Crate"));
            updateUI();
        }

        private void SaveCrate(object sender, RoutedEventArgs e)
        {
            if (CratePanel.DataContext is Crate)
            {
                storage.SaveCrate((Crate)CratePanel.DataContext);
                updateUI();
            }
        }

        private void DeleteCrate(object sender, RoutedEventArgs e)
        {
            if (CratePanel.DataContext is Crate)
            {
                storage.DeleteCrate((Crate)CratePanel.DataContext);
                updateUI();
            }
        }

        private void AddNewReward(object sender, RoutedEventArgs e)
        {
            if(CratePanel.DataContext != null)
            {
                if(CratePanel.DataContext is Crate) 
                {
                    ((Crate)CratePanel.DataContext).Rewards.Add(new Reward("New Reward"));
                }
            }
        }

        private void SaveAll_Click(object sender, RoutedEventArgs e)
        {
            List<Crate> crates = (List<Crate>)CrateList.Items.SourceCollection;
            foreach(Crate crate in CrateList.Items.SourceCollection)
            {
                storage.SaveCrate(crate);
            }
            updateUI();
        }

        private void NewCommand(object sender, RoutedEventArgs e)
        {
            if(RewardPanel.DataContext is Reward)
            {
                ((Reward)RewardPanel.DataContext).RewardTags.Add(new CommandTag());
            }
        }

        private void TagContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // Use reflection to set the item source of our list box
            PropertyInfo[] info = e.NewValue.GetType().GetProperties();
            List<string> infos = new List<string>();
            foreach(PropertyInfo inf in info)
            {
                infos.Add(inf.GetValue(e.NewValue).ToString());
            }
            if (sender is ListBox)
            {
                
                ((ListBox)sender).ItemsSource = infos;
            }
        }

        private void NewItem(object sender, RoutedEventArgs e)
        {
            if(RewardPanel.DataContext is Reward)
            {
                ((Reward)RewardPanel.DataContext).RewardTags.Add(new ItemTag());
            }
        }

        private void Display(object sender, RoutedEventArgs e)
        {
            if(RewardPanel.DataContext is Reward)
            {
                ((Reward)RewardPanel.DataContext).RewardTags.Add(new DisplayTag());
            }
        }

        private void RemoveSelected(object sender, RoutedEventArgs e)
        {
            if(RewardPanel.DataContext is Reward)
            {
                ((Reward)RewardPanel.DataContext).RewardTags.Remove((TagBase)RewardTagsBox.SelectedItem);
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            CrateTypeComboBox.ItemsSource = Enum.GetValues(typeof(CrateType));
        }
    }
}
