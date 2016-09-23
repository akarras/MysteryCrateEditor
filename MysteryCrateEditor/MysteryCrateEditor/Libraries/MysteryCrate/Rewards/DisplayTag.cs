using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCrateEditor.Libraries.MysteryCrate.Rewards
{
    public class DisplayTag:ItemTag
    {
        public DisplayTag()
        {
            TagName = "display";
        }
        public DisplayTag(string item, int amount)
        {
            TagName = "display";
            Item = item;
            Amount = amount;
        }
    }
}
