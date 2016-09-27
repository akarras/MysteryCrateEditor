using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCrateEditor.Libraries.MysteryCrate.Rewards
{
    public class RewardMacro
    {
        public List<Reward> Rewards { get; set; }
        /// <summary>
        /// Macro Values, key is text to be replaced, value is text to replace the key with.
        /// </summary>
        public List<KeyValuePair<String,String>> MacroValues { get; set; }
    }
}
