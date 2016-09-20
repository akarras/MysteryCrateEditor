using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCrateEditor.Libraries.MysteryCrate.Rewards
{
    public abstract class TagBase:IRewardTag
    {
        public TagBase()
        {

        }
        public TagBase(string tagName)
        {
            TagName = tagName;
        }

        public string TagName { get; set; }

        public string GetTag()
        { 
            return $"{TagName}:({GetTagContents()})";
        }

        public abstract string GetTagContents();
    }
}
