using MysteryCrateEditor.Libraries.MysteryCrate.Rewards;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCrateEditor.Libraries.MysteryCrates
{
    /// <summary>
    /// Provides all the information needed for a crate
    /// </summary>
    public class Crate : INotifyPropertyChanged
    {
        public Crate(String name)
        {
            // Initialize our variables
            Id = Guid.NewGuid();
            Rewards = new List<Reward>();
            Name = name;
            Shop = new CrateShop();
            Message = new CrateMessages();
            Effect = new CrateEffects();
            Rarities = new List<CrateRarity>();
        }
        public CrateType Type { get; set; }
        public Guid Id { get; set; }
        [JsonProperty(ItemTypeNameHandling = TypeNameHandling.Arrays)]
        public List<Reward> Rewards { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool Preview { get; set; }
        public CrateShop Shop { get; set; }
        public CrateMessages Message { get; set; }
        public CrateEffects Effect { get; set; }
        public bool RaritiesEnabled { get; set; }
        public List<CrateRarity> Rarities { get; set; }
        public void NotifyUpdate(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public override string ToString()
        {
            if (RaritiesEnabled)
            {
                // Iterate through all of the items and replace our rarities with them!
                foreach (var item in Rewards)
                {
                    ChanceTag chance = item.GetChance();
                    CrateRarity RarityTag = null;
                    // Get the best rarity match for our tag
                    foreach (var rarity in Rarities)
                    {
                        if(RarityTag == null)
                        {
                            RarityTag = rarity;
                        }
                        else
                        { 
                            if(RarityTag.Value >= rarity.Value)
                                RarityTag = rarity;
                        }
                    }
                    foreach(var tag in item.RewardTags)
                    {
                        if(tag is ItemTag)
                        {
                            var itemTag = (ItemTag)tag;
                            if (RarityTag != null)
                            {
                                itemTag.Lore.Add(RarityTag.Name);
                            }
                        }
                    }

                }
            }

            string reward = "";
            foreach (var item in Rewards)
            {
                reward += "            - \"" + item.GetReward() + "\"\n";
            }
            return reward;
        }
    }
    public enum CrateType
    {
        SUPPLYCRATE,
        MYSTRYCRATE,
        MENUCRATE,
        KEYCRATE,
        ROULETTEKEYCRATE,
        CSGOKEYCRATE
    }
    public class CrateRarity
    {
        public int Value { get; set; }
        public string Name { get; set; }
    }
    public class CrateShop
    {
        public bool Enabled { get; set; }
        public int Buy { get; set; }
    }
    public class CrateMessages
    {
        public string OnOpen { get; set; }
        public string Broadcast { get; set; }
    }
    public class CrateEffects
    {

    }
}
