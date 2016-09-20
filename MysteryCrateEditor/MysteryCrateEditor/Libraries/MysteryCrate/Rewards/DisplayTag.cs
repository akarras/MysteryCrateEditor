using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCrateEditor.Libraries.MysteryCrate.Rewards
{
    public class DisplayTag:ItemTag
    {
        public DisplayTag() : base("display")
        {
        }
        public DisplayTag(string item, int amount):base("display")
        {
            Item = item;
            Amount = amount;
        }
    }
}
