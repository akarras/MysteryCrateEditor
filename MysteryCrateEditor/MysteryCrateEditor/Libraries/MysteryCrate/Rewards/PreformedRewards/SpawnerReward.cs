using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCrateEditor.Libraries.MysteryCrate.Rewards.PreformedRewards
{
    public class SpawnerExtensions
    {
        public static Reward SpawnerReward(ChanceTag chance, Mobs mob)
        {
            var tagList = new List<TagBase>();
            string mobName = char.ToUpper(mob.ToString()[0])+mob.ToString().Substring(1);
            tagList.Add(new DisplayTag() { Item = "mob_spawner", Amount = 1, Name = $"&e{mobName} Spawner" });
            tagList.Add(new CommandTag($"spawner {mob} 1 %player%"));
            return new Reward(chance, tagList.ToArray());
        }
    }
    public enum Mobs
    {
        snowman,
        cow,
        pig,
        skeleton,
        bat,
        rabbit,
        silverfish,
        giant,
        enderdragon,
        zombiepigman,
        ocelot,
        horse,
        lavaslime,
        mooshroom,
        squid,
        witch,
        entityhorse,
        creeper,
        enderman,
        cavespider,
        magmacube,
        chicken,
        snowgolem,
        slime,
        blaze,
        endermite,
        pigzombie,
        zombie,
        mushroomcow,
        irongolem,
        villagergolem,
        guardian,
        ghast,
        sheed,
        wolf,
        villager,
        wither,
        spider
        
    }
}
