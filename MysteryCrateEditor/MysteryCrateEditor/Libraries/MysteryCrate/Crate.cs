﻿using MysteryCrateEditor.Libraries.MysteryCrate.Rewards;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;

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
            Key = new ItemTag();
            MinimumRewards = 1;
            MaximumRewards = 1;
        }
        public CrateType Type { get; set; }
        [YamlIgnore]
        public Guid Id { get; set; }
        [JsonProperty(ItemTypeNameHandling = TypeNameHandling.Arrays),YamlIgnore]
        public List<Reward> Rewards { get; set; }
        public int MinimumRewards { get; set; }
        public int MaximumRewards { get; set; }
        [JsonIgnore]
        public List<String> RewardList { get; set; }
        public ItemTag Key { get; set; }
        public string Name { get; set; }
        private string _displayName;
        public string DisplayName
        {
            get
            {
                if (_displayName != null)
                {
                    return _displayName;
                }
                return "";
            }
            set
            {
                _displayName = value;
            }
        }
        public bool Preview { get; set; }
        public CrateShop Shop { get; set; }
        public CrateMessages Message { get; set; }
        public CrateEffects Effect { get; set; }
        public bool RaritiesEnabled { get; set; }
        [YamlIgnore]
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
                        // Check if the given rarity applies to our tag
                        if (chance.Chance >= rarity.Value)
                        {
                            if (RarityTag == null)
                            {

                                RarityTag = rarity;
                            }
                            else
                            {
                                // If the new rarity is better than the current rarity we should use that one instead
                                if(RarityTag.Value < rarity.Value)
                                    RarityTag.Value = rarity.Value;
                            }
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

        public YamlNode serializeYaml()
        {
            var crateNode = new YamlMappingNode();
            // This should be validated, as any null object will break the entire document
            // Add the properties of the crate
            var crateProps = new YamlMappingNode();
            crateProps.Add("type", Type.ToString());

            var displayName = new YamlScalarNode(DisplayName);
            displayName.Style = YamlDotNet.Core.ScalarStyle.SingleQuoted;
            crateProps.Add("displayName", displayName);

            crateProps.Add("preview", Preview.ToString().ToLower());

            // BUY NODE
            var buyNode = new YamlMappingNode();
            buyNode.Add("enabled", Shop.Enabled.ToString().ToLower());
            buyNode.Add("cost", Shop.Buy.ToString());
            crateProps.Add("buy", buyNode);
            
            // KEY NODE
            var key = new YamlMappingNode();
            key.Add("item", Key.Item);
            key.Add("enchantment", string.Join(";", Key.Enchants));
            // The item uses null to keep the empty one
            if (Key.Name != null)
            {
                key.Add("name", Key.Name);
            }
            else
            {
                key.Add("name", "");
            }
            var keyLore = new YamlSequenceNode();
            foreach(var lore in Key.EditLore)
            {
                keyLore.Add(lore.Lore);
            }
            key.Add("lore", keyLore);
            if (key != null)
            {
                crateProps.Add("key", key);
            }

            // MESSAGE NODE
            var messageNode = new YamlMappingNode();
            // Set our style to singlequoted for this section
            var onOpenNode = new YamlScalarNode(Message.OnOpen);
            onOpenNode.Style = YamlDotNet.Core.ScalarStyle.SingleQuoted;
            var broadcastNode = new YamlScalarNode(Message.Broadcast);
            broadcastNode.Style = YamlDotNet.Core.ScalarStyle.SingleQuoted;
            messageNode.Add("onOpen", onOpenNode);
            messageNode.Add("broadcast", broadcastNode);
            crateProps.Add("message", messageNode);
            
            // EFFECT NODE
            var effect = new YamlMappingNode();
            string openEffects = string.Join(", ", from openEffect in Effect.onOpenEffects
                                                   select openEffect.Effect);
            var openEff = new YamlScalarNode(openEffects);
            openEff.Style = YamlDotNet.Core.ScalarStyle.SingleQuoted;
            string dormantEffects = string.Join(", ", from dormantEffect in Effect.dormantEffects
                                                      select dormantEffect.Effect);
            var dormantEff = new YamlScalarNode(dormantEffects);
            dormantEff.Style = YamlDotNet.Core.ScalarStyle.SingleQuoted;
            effect.Add("onOpenEffects", openEff);
            effect.Add("dormantEffects", dormantEff);
            crateProps.Add("effect", effect);

            // REWARDS NODE
            var reward = new YamlMappingNode();
            reward.Add("minimumRewards", MinimumRewards.ToString());
            reward.Add("maximumRewards", MaximumRewards.ToString());
            // Add the list of crate rewards
            var rewards = new YamlSequenceNode();
            foreach (var thereward in Rewards)
            {
                // Updates the plaintext Lore with rarity
                thereward.UpdateLore(getRarity(thereward));
                var scalar = new YamlScalarNode(thereward.GetReward());
                scalar.Style = YamlDotNet.Core.ScalarStyle.SingleQuoted;
                rewards.Add(scalar);
            }
            if (rewards.Count() > 0)
            {
                reward.Add("rewards", rewards);
            }
            crateProps.Add("reward", reward);
            // Add the properties to the root crate node
            crateNode.Add(Name, crateProps);

            return crateNode;
        }

        private string getRarity(Reward item)
        {
            ChanceTag itemChance = item.GetChance();
            CrateRarity RarityTag = null;
            // Get the best rarity match for our tag
            foreach (var rarity in Rarities)
            {
                if (itemChance.Chance <= rarity.Value)
                {
                    if (RarityTag == null)
                    {
                        RarityTag = rarity;
                    }
                    else
                    {
                        // If the new rarity is worse than the current rarity we should use that one instead
                        if (RarityTag.Value >= rarity.Value)
                        {
                            RarityTag.Value = rarity.Value;
                        }
                    }
                }
            }
            if(RarityTag == null)
            {
                return "DunBad";
            }
            return RarityTag.Name;
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


    public enum CrateEffect
    {
        firework,
        explode,
        largeexplode,
        hugeexplosion,
        fireworksSpark,
        bubble,
        splash,
        wake,
        suspended,
        depthSuspend,
        crit,
        magicCrit,
        smoke,
        largesmoke,
        spell,
        instantSpell,
        mobSpell,
        mobSpellAmbient,
        witchMagic,
        dripWater,
        dripLava,
        angryVillager,
        happyVillager,
        townaura,
        note,
        portal,
        enchantmenttable,
        flame,
        lava,
        footstep,
        cloud,
        reddust,
        snowballpoof,
        snowshovel,
        slime,
        heart,
        barrier,
        iconcrack,
        blockcrack,
        blockdust,
        droplet,
        take,
        mobappearance
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
        private string _onOpen;
        private string _broadcast;
        public string OnOpen
        {
            get
            {
                if(_onOpen != null)
                {
                    return _onOpen;
                }
                return "";
            }
            set
            {
                _onOpen = value;
            }
        }
        public string Broadcast
        {
            get
            {
                if(_broadcast != null)
                {
                    return _broadcast;
                }
                return "";
            }
            set
            {
                _broadcast = value;
            }
        }
    }


    public class CrateEffects
    {
        public CrateEffects()
        {
            onOpenEffects = new List<CrateEffectWrapper>();
            dormantEffects = new List<CrateEffectWrapper>();
        }
        public List<CrateEffectWrapper> onOpenEffects { get; set; }
        public List<CrateEffectWrapper> dormantEffects { get; set; }
    }

    public class CrateEffectWrapper
    {
        public CrateEffectWrapper()
        {

        }
        public CrateEffectWrapper(CrateEffect effect)
        {
            Effect = effect;
        }
        public CrateEffect Effect { get; set; }
    }
}
