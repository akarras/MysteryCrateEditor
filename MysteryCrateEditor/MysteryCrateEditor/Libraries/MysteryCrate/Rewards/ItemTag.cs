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
        public string Item;
        [JsonProperty("Amount")]
        public int Amount = 1;
        [JsonProperty("Name")]
        public string Name;
        [JsonProperty("Lore")]
        public List<string> Lore;
        [JsonProperty("Enchants")]
        public List<EnchantData> Enchants;
        [JsonProperty("ColorData")]
        public ColorData Colors;
        public ItemTag() : base("item")
        {
            

        }

        public ItemTag(string tag) : base(tag)
        {

        }

        public ItemTag(string item, int amount) : base("item")
        {
            Item = item;
            Amount = amount;
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
}
