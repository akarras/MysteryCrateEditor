using MysteryCrateEditor.Libraries.MysteryCrate.Rewards.ItemData;
using MysteryCrateEditor.Libraries.MysteryCrate.Rewards.MinecraftJson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCrateEditor.Libraries.MysteryCrate.Rewards
{
    public class ItemTag : TagBase
    {
        [JsonProperty("Item")]
        public string Item { get; set; }
        [JsonProperty("Amount")]
        public int Amount { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Lore")]
        public List<string> Lore { get; set; }
        public List<LoreContainer> EditLore { get; set; }
        [JsonProperty("Enchants")]
        public List<EnchantData> Enchants { get; set; }
        [JsonProperty("ColorData")]
        public ColorData Colors { get; set; }
        public ItemTag() : base("item")
        {
            // Initialize variables
            Item = "New Item";
            Amount = 1;
            Lore = new List<string>();
            Enchants = new List<EnchantData>();
        }

        public ItemTag(string item, int amount) : base("item")
        {
            // Initialize variables
            Item = item;
            Amount = amount;
            EditLore = new List<LoreContainer>();
            Lore = new List<string>();
            Enchants = new List<EnchantData>();
        }

        public string getGiveCommand(string MinecraftUser, int Durability = 0)
        {
            var minecraftItem = new MojangItem();
            minecraftItem.Display = new Display() { Lore = this.Lore?.ToArray(), Name = Name };
            minecraftItem.Enchants = Enchants?.AsMojang();
            //var mineString = JObject.FromObject(minecrafItem).ToString();
            //mineString = mineString.Replace("\r\n", "");
            //mineString = mineString.Replace("  ", "");
            var jsonSerializer = new JsonSerializer();
            var writer = new StringWriter();
            using(JsonTextWriter jsonWriter = new JsonTextWriter(writer))
            {
                jsonWriter.QuoteName = false;
                jsonSerializer.Serialize(jsonWriter, minecraftItem);
            }
            
            return $"give {MinecraftUser} {Item} {Amount} {Durability} {writer.ToString()}";
        }

        private string getLoreString()
        {
            return string.Join("%line%", Lore);
        }

        private string getDataString()
        {
            List<string> dataParts = new List<string>();
            if (Enchants != null)
                dataParts.AddRange(Enchants.Select(e => e.ToString()));
            if (Colors != null)
                dataParts.Add(Colors.ToString());
            return string.Join(";", dataParts);
        }

        private string getItemFormattedString(string input)
        {
            return input.Replace(' ', '_');
        }

        

        public override string GetTagContents()
        {
            // Copy the edit lore to the string array container
            if (EditLore != null)
            {
                foreach (LoreContainer lore in EditLore)
                {
                    Lore.Add(lore.Lore);
                }
            }


            List<string> parts = new List<string>();
            parts.Add(Item);
            parts.Add(Amount.ToString());
            if (Name != null)
                parts.Add(getItemFormattedString(Name));
            else
                parts.Add("-");
            if (Lore != null)
                parts.Add(getItemFormattedString(getLoreString()));
            else
                parts.Add("-");
            if (Enchants != null|Colors != null)
                parts.Add(getDataString());
            string item = string.Join(" ", parts);
            item = item.Replace(" - ", " ");
            item = item.Replace(" -", "");
            return item;
        }
    }
    public class LoreContainer
    {
        public string Lore { get; set; }
    }
}
