using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCrateEditor.Libraries.MysteryCrate.Rewards
{
    public class ChanceTag : TagBase
    {
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
