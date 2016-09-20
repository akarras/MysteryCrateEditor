using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCrateEditor.Libraries.MysteryCrate.Rewards
{
    public class ChanceTag : TagBase
    {
        [JsonProperty("Chance")]
        public int Chance;
        public ChanceTag(int chance) : base("chance")
        {
            Chance = chance;
        }
        public override string GetTagContents()
        {
            return Chance.ToString();
        }
    }
}
