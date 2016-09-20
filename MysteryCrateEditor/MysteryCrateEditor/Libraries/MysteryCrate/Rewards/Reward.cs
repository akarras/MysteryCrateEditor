using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCrateEditor.Libraries.MysteryCrate.Rewards
{
    public class Reward
    {
        public string Name { get; set; }
        public List<TagBase> RewardTags { get; set; }
        public Reward()
        {
            RewardTags = new List<TagBase>();
        }
        public Reward(string name)
        {
            Name = name;
            RewardTags = new List<TagBase>();
        }
        public Reward(ChanceTag chance, params TagBase[] tags)
        {
            RewardTags = new List<TagBase>();
            foreach(var tag in tags)
            {
                RewardTags.Add(tag);
            }
            RewardTags.Add(chance);
        }
        public string GetReward()
        {
            return string.Join(", ", RewardTags.Select(e => e.GetTag()));
        }
        
    }

    public static class RewardExtensions
    {
        public static string RewardName(this Reward reward)
        {
            string rewardName = "";
            foreach(var tag in reward.RewardTags)
            {
                if(tag is ItemTag)
                {
                    rewardName = (tag as ItemTag).Name;
                }
                else if(tag is DisplayTag)
                {
                    rewardName = (tag as DisplayTag).Name;
                }
            }
            return rewardName;
        }
        public static List<Reward> GroupRewardWithChance(this IEnumerable<ItemTag> items, ChanceTag chance)
        {
            List<Reward> rewards = new List<Reward>();
            foreach (var item in items)
            {
                rewards.Add(new Reward(chance, item));
            }
            return rewards;
        }
        public static ChanceTag GetChance(this Reward reward)
        {
            var rewards = from tag in reward.RewardTags
                          where tag is ChanceTag
                          select tag;
            return rewards.First() as ChanceTag;
        }
    }
}
