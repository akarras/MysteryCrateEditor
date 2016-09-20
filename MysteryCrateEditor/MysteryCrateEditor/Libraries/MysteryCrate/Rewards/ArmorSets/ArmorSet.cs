using MysteryCrateEditor.Libraries.MysteryCrate.Rewards.ItemData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCrateEditor.Libraries.MysteryCrate.Rewards.ArmorSets
{
    public class ColorSet
    {
        public ColorSet(ColorData baseColor)
        {
            HeadColor = baseColor;
            ChestColor = baseColor;
            LegColor = baseColor;
            BootsColor = baseColor;
        }
        public ColorData GetColorForSlot(ArmorSlot slot)
        {
            switch (slot)
            {
                case ArmorSlot.Helmet:
                    return HeadColor;
                case ArmorSlot.Chestplate:
                    return ChestColor;
                case ArmorSlot.Leggings:
                    return LegColor;
                case ArmorSlot.Boots:
                    return BootsColor;
                default:
                    break;
            }
            return HeadColor;
        }

        public ColorData HeadColor { get; set; }
        public ColorData ChestColor { get; set; }
        public ColorData LegColor { get; set; }
        public ColorData BootsColor { get; set; }
    }
    public class ArmorSet : ItemTag
    {
        public ArmorTypes ArmorType;
        public ColorSet ColorData;
        public new ColorData Colors {
            get {
                return ColorData.ChestColor;
            }
            set
            {
                ColorData = new ColorSet(value);
            }
        }
        public ArmorSet(ArmorTypes type) : base()
        {
            ArmorType = type;
            Amount = 1;
        }

        public ArmorSet(ArmorTypes type, string name) : base()
        {
            Name = name;
            ArmorType = type;
            Amount = 1;
        }

        public ArmorSet(ArmorTypes type, string name, List<string> lore) : base()
        {
            Lore = lore;
            Name = name;
            ArmorType = type;
            Amount = 1;
        }

        public List<ItemTag> GenerateArmorPieces()
        {
            List<ItemTag> armorPieces = new List<ItemTag>();
            armorPieces.Add(GenerateArmor(ArmorSlot.Helmet));
            armorPieces.Add(GenerateArmor(ArmorSlot.Chestplate));
            armorPieces.Add(GenerateArmor(ArmorSlot.Leggings));
            armorPieces.Add(GenerateArmor(ArmorSlot.Boots));
            return armorPieces;
        }

        public ItemTag GenerateArmor(ArmorSlot slot)
        {
            ItemTag subItem = new ItemTag(this.Item, this.Amount);
            //Set the item type based on the slot
            subItem.Item = $"{ArmorType.ToString()}_{slot.ToString()}".ToLower();
            subItem.Name = Name.Replace("{slot}", $"{slot}");
            List<string> loreList = new List<string>();
            if (Lore != null)
            {
                foreach (var loreItem in Lore)
                {
                    loreList.Add(loreItem.Replace("{slot}", $"{slot}"));
                }
                subItem.Lore = loreList;
            }
            if (ColorData != null)
            {
                subItem.Colors = ColorData.GetColorForSlot(slot);
            }

            subItem.Enchants = Enchants?.Where(e => e.getEnchantCompatabilityWithSlot(slot)).ToList();
            return subItem;
        }
        
    }
    public enum ArmorTypes
    {
        Leather,
        Gold,
        Iron,
        Diamond
    }
    public enum ArmorSlot
    {
        Helmet,
        Chestplate,
        Leggings,
        Boots
    }
}
