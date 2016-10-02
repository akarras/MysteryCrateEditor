using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCrateEditor.Libraries.MysteryCrate.Rewards
{
    public class TagMacro
    {
        public List<IRewardTag> Rewards { get; set; }
        public Dictionary<string,string> MacroValues { get; set; }
    }
    public class RewardMacro
    {
        public List<Reward> Rewards { get; set; }
        /// <summary>
        /// Macro Values, key is text to be replaced, value is text to replace the key with.
        /// </summary>
        public Dictionary<string,string> MacroValues { get; set; }

        public void doReplacement()
        {
            foreach(var reward in Rewards)
            {
                
            }
            foreach(var macroValue in MacroValues)
            {
                
            }
        }
    }
    public interface IMacro
    {
        
    }

}
