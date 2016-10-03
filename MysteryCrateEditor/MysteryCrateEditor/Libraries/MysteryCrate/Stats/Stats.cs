﻿using MysteryCrateEditor.Libraries.MysteryCrate.Rewards;
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
                          select new ChanceStat(reward.GetChance().Chance);
            Chances = chances.ToList();
        }

        public List<ChanceStat> Chances { get; set; }

        public int getTotal()
        {
            var chanceInts = from chance in Chances
                             select chance.Chance;
            return chanceInts.Sum();
        }
    }
    public class ChanceStat
    {
        public ChanceStat(int chance)
        {
            Chance = chance;
        }
        public int Chance { get; set; }
        public float PercentChance { get; set; }
    }
}