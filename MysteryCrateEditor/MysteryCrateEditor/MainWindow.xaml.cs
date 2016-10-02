using MysteryCrateEditor.Libraries.MysteryCrate.Rewards;
using MysteryCrateEditor.Libraries.MysteryCrate.Rewards.ItemData;
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
        List<Crate> crates;
        public MainWindow()
        {
            InitializeComponent();
            // Initialize our storage and load our crates from memory
            storage = new JSONStorage();
            loadData();
            updateUI();
        }

        private void loadData()
        {
            if (crates != null)
            {
                var tempCrates = storage.GetCrates();
                if (tempCrates != crates)
                {
                    // User has made changes to the crates, we should ask if they wish to save new changes
                    MessageBoxResult result = MessageBox.Show("Would you like to save your changes?", "Changes made", MessageBoxButton.YesNoCancel);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            // Save all of our crates and then reload from the configuration
                            foreach (Crate crate in crates)
                            {
                                storage.SaveCrate(crate);
                            }
                            crates = storage.GetCrates();
                            break;
                        case MessageBoxResult.No:
                            // Simply use the new crates as the user didn't request a save.
                            crates = tempCrates;
                            break;
                        case MessageBoxResult.Cancel:
                            // Do nothing!
                            return;
                    }
                }
            }
            else
            {
                crates = storage.GetCrates();
            }
        }

        private void updateUI()
        {
            int crateListIndex = CrateList.SelectedIndex;
            int crateRewardIndex = RewardsListBox.SelectedIndex;
            if(CrateList.ItemsSource != null)
            {
                // Set the items source to null so that the rest of the bindings will update
                // The better solution to this would be to make all of the subclasses observable
                // But for now this works
                CrateList.ItemsSource = null;
            }

            CrateList.ItemsSource = crates;
            // Redo our selected indexes
            if (CrateList.Items.Count > crateListIndex)
            {
                CrateList.SelectedIndex = crateListIndex;
            }
            if (RewardsListBox.Items.Count > crateRewardIndex)
            {
                RewardsListBox.SelectedIndex = crateRewardIndex;
            }
        }

        private void AddCrate(object sender, RoutedEventArgs e)
        {
            Crate newCrate = new Crate("New Crate");
            crates.Add(newCrate);
            storage.SaveCrate(newCrate);
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
            var result = MessageBox.Show("Deleting the crate is irreversable", "Are you sure?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
                return;
            if (CratePanel.DataContext is Crate)
            {
                storage.DeleteCrate((Crate)CratePanel.DataContext);
                crates.Remove((Crate)CratePanel.DataContext);
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
                    updateUI();
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
                updateUI();
            }
        }

        private void NewItem(object sender, RoutedEventArgs e)
        {
            if(RewardPanel.DataContext is Reward)
            {
                ((Reward)RewardPanel.DataContext).RewardTags.Add(new ItemTag());
                updateUI();
            }
        }

        private void Display(object sender, RoutedEventArgs e)
        {
            if(RewardPanel.DataContext is Reward)
            {
                ((Reward)RewardPanel.DataContext).RewardTags.Add(new DisplayTag());
                updateUI();
            }
        }

        private void RemoveSelected(object sender, RoutedEventArgs e)
        {
            if(RewardPanel.DataContext is Reward)
            {
                ((Reward)RewardPanel.DataContext).RewardTags.Remove((TagBase)RewardTagsBox.SelectedItem);
                updateUI();
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            CrateTypeComboBox.ItemsSource = Enum.GetValues(typeof(CrateType));
        }

        private void ExportCrate(object sender, RoutedEventArgs e)
        {
            if(CratePanel.DataContext is Crate)
            {
                Crate crate = (Crate)CratePanel.DataContext;
                // Should probably make a new window to show this in that allows the user to copy+paste it into their config.
                // Once more developed, I should also add a export all button.
                MessageBox.Show(crate.ToString());
            }
        }

        private void RemoveSelectedReward(object sender, RoutedEventArgs e)
        {
            if(RewardsListBox.SelectedItem is Reward)
            {
                // Removes the currently selected reward from the crate
                ((Crate)CrateList.SelectedItem).Rewards.Remove((Reward)RewardsListBox.SelectedItem);
                updateUI();
            }
        }

        private void NewChance(object sender, RoutedEventArgs e)
        {
            ((Reward)RewardPanel.DataContext).RewardTags.Add(new ChanceTag(1));
            updateUI();
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            List<Crate> tmpCrates = storage.GetCrates();
            if(crates != null)
            {
                if(tmpCrates != crates)
                {
                    MessageBoxResult result = MessageBox.Show("Would you like to save your changes?", "Changes made", MessageBoxButton.YesNoCancel);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            // Save all of our crates
                            foreach (Crate crate in crates)
                            {
                                storage.SaveCrate(crate);
                            }
                            break;
                        case MessageBoxResult.No:
                            // Do nothing! Muhaha!
                            break;
                        case MessageBoxResult.Cancel:
                            e.Cancel = true;
                            return;
                    }
                }
            }
        }

        private void AddLore(object sender, RoutedEventArgs e)
        {
            if(sender is Button)
            {
                Button sendyButton = (Button)sender;
                if(sendyButton.DataContext is ItemTag)
                {
                    if(((ItemTag)sendyButton.DataContext).EditLore == null)
                    {
                        ((ItemTag)sendyButton.DataContext).EditLore = new List<LoreContainer>();
                    }
                    ((ItemTag)sendyButton.DataContext).EditLore.Add(new LoreContainer() { Lore = "New Lore" });
                }
                updateUI();
            }
        }

        private void Macros(object sender, RoutedEventArgs e)
        {
            
        }

        private void AddRarity(object sender, RoutedEventArgs e)
        {
            if(((Crate)CratePanel.DataContext).Rarities == null)
            {
                // Make a new list of these because a few of my files never had them initialized ¯\_(ツ)_/¯
                ((Crate)CratePanel.DataContext).Rarities = new List<CrateRarity>();
            }
            ((Crate)CratePanel.DataContext).Rarities.Add(new CrateRarity() { Name = "New Rarity", Value = 0 });
            updateUI();
        }

        private void RemoveRarity(object sender, RoutedEventArgs e)
        {
            ((Crate)CratePanel.DataContext).Rarities.Remove((CrateRarity)RaritiesListBox.SelectedValue);
            updateUI();
        }

        private void AddEnchantButton(object sender, RoutedEventArgs e)
        {
            if(sender is Button)
            {
                Button sendyButton = (Button)sender;
                if(sendyButton.DataContext is ItemTag)
                {
                    ((ItemTag)sendyButton.DataContext).Enchants.Add(new EnchantData());
                    updateUI();
                }
            }
        }

        private void RemoveEnchantButton(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                Button sendyButton = (Button)sender;
                if (sendyButton.DataContext is ListBox)
                {
                    ListBox listy = (ListBox)sendyButton.DataContext;
                    ((ItemTag)listy.DataContext).Enchants.Remove((EnchantData)listy.SelectedItem);
                    updateUI();
                }
            }
        }

        private void EditMacros(object sender, RoutedEventArgs e)
        {
            MacroEditor editor = new MacroEditor();
            editor.ShowDialog();
        }

        private void InsertMacro(object sender, RoutedEventArgs e)
        {
            MacroPicker picker = new MacroPicker();
            picker.ShowDialog();
        }

        private void DuplicateSelectedReward(object sender, RoutedEventArgs e)
        {
            if (RewardsListBox.SelectedItem is Reward)
            {
                // Make a new version of Reward but copy all of the data over from the other one
                Reward reward = new Reward();
                reward.Name = "Duplicate ";
                Reward otherReward = (Reward)RewardsListBox.SelectedItem;
                reward.Name = otherReward.Name;
                reward.RewardTags = new List<TagBase>();
                foreach(var rewardTag in otherReward.RewardTags)
                {
                    reward.RewardTags.Add(rewardTag);
                }
                reward.Name = otherReward.Name + " (1)";
                ((Crate)CrateList.SelectedItem).Rewards.Add(reward);
                updateUI();
            }
        }

        private void EditLore(object sender, RoutedEventArgs e)
        {
            SimpleTextPage page = new SimpleTextPage();
            page.Text = (string)((Button)sender).DataContext;
            bool? test = page.ShowDialog();
            ((Button)sender).DataContext = page.Text;
        }

        private void RemoveLore(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            ListBox box = (ListBox)button.DataContext;
            ((ItemTag)box.DataContext).EditLore.RemoveAt(box.SelectedIndex);
            button.Parent.GetType();
            updateUI();
        }
    }
}
