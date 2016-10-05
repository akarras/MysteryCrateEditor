using MysteryCrateEditor.Libraries.MysteryCrate.Rewards;
using MysteryCrateEditor.Libraries.MysteryCrates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCrateEditor.Libraries.MysteryCrate.Stats
{
    public class Stats
    {
        public Stats(List<Reward> rewards)
        {
            var chances = from reward in rewards
                          select new ChanceStat(reward.Name,reward.GetChance().Chance);
            Chances = chances.ToList();
            int total = getTotal();
            foreach(var chance in Chances)
            {
                chance.PercentChance = (float)chance.Chance / (float)total;
            }
        }

        public List<ChanceStat> Chances { get; set; }

        public int Total { get; set; }

        public int getTotal()
        {
            var chanceInts = from chance in Chances
                             select chance.Chance;
            Total = chanceInts.Sum();
            return Total;
        }
    }
    public class ChanceStat
    {
        public ChanceStat(string name, int chance)
        {
            Name = name;
            Chance = chance;
        }
        public string Name { get; set; }
        public int Chance { get; set; }
        public float PercentChance { get; set; }
    }
}
