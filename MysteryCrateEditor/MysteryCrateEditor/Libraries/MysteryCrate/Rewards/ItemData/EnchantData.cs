using MysteryCrateEditor.Libraries.MysteryCrate.Rewards.ArmorSets;
using MysteryCrateEditor.Libraries.MysteryCrate.Rewards.MinecraftJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCrateEditor.Libraries.MysteryCrate.Rewards.ItemData
{
    public class EnchantData
    {
        public MinecraftEnchants Enchant { get; set; }
        public int Strength { get; set; }
        public override string ToString()
        {
            return $"{(int)Enchant}:{Strength}";
        }

        public bool getEnchantCompatabilityWithSlot(ArmorSlot slot)
        {
            var enchantCompat = EnchantDataExtensions.GetEnchant(Enchant);
            switch (slot)
            {
                case ArmorSlot.Helmet:
                    if (enchantCompat.Head) return true;
                    else return false;
                case ArmorSlot.Chestplate:
                    if (enchantCompat.Chest) return true;
                    else return false;
                case ArmorSlot.Leggings:
                    if (enchantCompat.Leggings) return true;
                    else return false;
                case ArmorSlot.Boots:
                    if (enchantCompat.Boots) return true;
                    else return false;
                default:
                    return true;
            }
        }

        public EnchantData()
        {
            this.Enchant = MinecraftEnchants.AquaAffinity;
            this.Strength = 1;
        }

        public EnchantData(MinecraftEnchants Enchant, int Strength)
        {
            this.Enchant = Enchant;
            this.Strength = Strength;
        }


    }

    public static class EnchantDataExtensions
    {
        public static MojangEnchantData[] AsMojang(this IEnumerable<EnchantData> data)
        {
            List<MojangEnchantData> newData = new List<MojangEnchantData>();
            foreach(var enchant in data)
            {
                newData.Add(enchant.AsMojang());
            }
            return newData.ToArray();
        }

        public static MojangEnchantData AsMojang(this EnchantData data)
        {
            return new MojangEnchantData() { Id = (int)data.Enchant, Lvl = data.Strength };
        }


        public static EnchantCompatability GetEnchant(MinecraftEnchants enchant)
        {
            switch (enchant)
            {
                case MinecraftEnchants.Glow:
                    return new EnchantCompatability((int)MinecraftEnchants.Glow) {
                        Universal = true
                    };
                case MinecraftEnchants.Protection:
                    return new EnchantCompatability((int)MinecraftEnchants.Protection)
                    {
                        Armor = true
                    };
                case MinecraftEnchants.FireProtection:
                    return new EnchantCompatability((int)MinecraftEnchants.FireProtection)
                    {
                        Armor = true
                    };
                case MinecraftEnchants.FeatherFalling:
                    return new EnchantCompatability((int)MinecraftEnchants.FeatherFalling)
                    {
                        Boots = true
                    };
                case MinecraftEnchants.BlastProtection:
                    return new EnchantCompatability((int)MinecraftEnchants.BlastProtection)
                    {
                        Armor = true
                    };
                case MinecraftEnchants.ProjectileProtection:
                    return new EnchantCompatability((int)MinecraftEnchants.ProjectileProtection)
                    {
                        Armor = true
                    };
                case MinecraftEnchants.Respiration:
                    return new EnchantCompatability((int)MinecraftEnchants.Respiration)
                    {
                        Head = true
                    };
                case MinecraftEnchants.AquaAffinity:
                    return new EnchantCompatability((int)MinecraftEnchants.AquaAffinity)
                    {
                        Head = true
                    };
                case MinecraftEnchants.Thorns:
                    return new EnchantCompatability((int)MinecraftEnchants.Thorns)
                    {
                        Armor = true
                    };
                case MinecraftEnchants.DepthStrider:
                    return new EnchantCompatability((int)MinecraftEnchants.DepthStrider)
                    {
                        Boots = true
                    };
                case MinecraftEnchants.FrostWalker:
                    return new EnchantCompatability((int)MinecraftEnchants.FrostWalker)
                    {
                        Boots = true
                    };
                case MinecraftEnchants.Sharpness:
                    return new EnchantCompatability((int)MinecraftEnchants.Sharpness)
                    {
                        Sword = true,
                        Axe = true
                    };
                case MinecraftEnchants.SweepingEdge:
                    return new EnchantCompatability((int)MinecraftEnchants.SweepingEdge)
                    {
                        Sword = true
                    };
                case MinecraftEnchants.Smite:
                    return new EnchantCompatability((int)MinecraftEnchants.Smite)
                    {
                        Sword = true,
                        Axe = true
                    };
                case MinecraftEnchants.BaneofArthropods:
                    return new EnchantCompatability((int)MinecraftEnchants.BaneofArthropods)
                    {
                        Sword = true,
                        Axe = true
                    };
                case MinecraftEnchants.Knockback:
                    return new EnchantCompatability((int)MinecraftEnchants.Knockback)
                    {
                        Sword = true
                    };
                case MinecraftEnchants.FireAspect:
                    return new EnchantCompatability((int)MinecraftEnchants.FireAspect)
                    {
                        Sword = true
                    };
                case MinecraftEnchants.Looting:
                    return new EnchantCompatability((int)MinecraftEnchants.Looting)
                    {
                        Sword = true
                    };
                case MinecraftEnchants.Efficiency:
                    return new EnchantCompatability((int)MinecraftEnchants.Efficiency)
                    {
                        BlockBreakingTools = true,
                        Shears = true
                    };
                case MinecraftEnchants.SilkTouch:
                    return new EnchantCompatability((int)MinecraftEnchants.SilkTouch)
                    {
                        BlockBreakingTools = true,
                        Shears = true
                    };
                case MinecraftEnchants.Unbreaking:
                    return new EnchantCompatability((int)MinecraftEnchants.Unbreaking)
                    {
                        Universal = true
                    };
                case MinecraftEnchants.Fortune:
                    return new EnchantCompatability((int)MinecraftEnchants.Fortune)
                    {
                        BlockBreakingTools = true
                    };
                case MinecraftEnchants.Power:
                    return new EnchantCompatability((int)MinecraftEnchants.Power)
                    {
                        Bow = true
                    };
                case MinecraftEnchants.Punch:
                    return new EnchantCompatability((int)MinecraftEnchants.Punch)
                    {
                        Bow = true
                    };
                case MinecraftEnchants.Flame:
                    return new EnchantCompatability((int)MinecraftEnchants.Flame)
                    {
                        Bow = true
                    };
                case MinecraftEnchants.Infinity:
                    return new EnchantCompatability((int)MinecraftEnchants.Infinity)
                    {
                        Bow = true
                    };
                case MinecraftEnchants.LuckOfTheSea:
                    return new EnchantCompatability((int)MinecraftEnchants.LuckOfTheSea)
                    {
                        FishingRod = true
                    };
                case MinecraftEnchants.Lure:
                    return new EnchantCompatability((int)MinecraftEnchants.Lure)
                    {
                        FishingRod = true
                    };
                case MinecraftEnchants.Mending:
                    return new EnchantCompatability((int)MinecraftEnchants.Mending)
                    {
                        Universal = true
                    };
                default:
                    break;
            }
            return new EnchantCompatability(-1) { Universal = true };
        }
    }

    public class EnchantCompatability
    {
        public EnchantCompatability(int Id)
        {
            EnchantId = Id;
        }
        public int EnchantId { get; set; }

        public bool Universal { get { return Head && Chest && Leggings && Boots && Sword && Bow && FishingRod && Pickaxe && Axe && Shovel && Shears; }
            set { Head = value; Chest = value; Leggings = value; Boots = value; Sword = value; Bow = value; FishingRod = value; Pickaxe = value; Axe = value; Shovel = value; Shears = value; } }

        public bool Armor { get { return Head && Chest && Leggings && Boots; } set { Head = value; Chest = value; Leggings = value; Boots = value; } }
        public bool Head { get; set; }
        public bool Chest { get; set; }
        public bool Leggings { get; set; }
        public bool Boots { get; set; }

        public bool Weapons { get { return Sword && Bow; } set { Sword = value; Bow = value; } }
        public bool Sword { get; set; }
        public bool Bow { get; set; }

        public bool BlockBreakingTools { get { return Axe && Shovel && Pickaxe; } set{ Axe = value; Shovel = value; Pickaxe = value;  } }
        public bool FishingRod { get; set; }
        public bool Pickaxe { get; set; }
        public bool Axe { get; set; }
        public bool Shovel { get; set; }
        public bool Shears { get; set; }

    }
    public enum MinecraftEnchants
    {
        Glow = 300,
        Protection = 0,
        FireProtection = 1,
        FeatherFalling = 2,
        BlastProtection = 3,
        ProjectileProtection = 4,
        Respiration = 5,
        AquaAffinity = 6,
        Thorns = 7,
        DepthStrider = 8,
        FrostWalker = 9,
        Sharpness = 16,
        Smite = 17,
        BaneofArthropods = 18,
        Knockback = 19,
        FireAspect = 20,
        Looting = 21,
        SweepingEdge = 22,
        Efficiency = 32,
        SilkTouch = 33,
        Unbreaking = 34,
        Fortune = 35,
        Power = 48,
        Punch = 49,
        Flame = 50,
        Infinity = 51,
        LuckOfTheSea = 61,
        Lure = 62,
        Mending = 70
    }
}
