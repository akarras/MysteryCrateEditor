using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCrateEditor.Libraries.MysteryCrate.Rewards
{
    public abstract class TagBase:IRewardTag
    {
        public TagBase(string tagName)
        {
            _tagName = tagName;
        }

        private string _tagName;
        public string TagName
        {
            get
            {
                return _tagName;
            }

            set
            {
                _tagName = value;
            }
        }

        public string GetTag()
        { 
            return $"{TagName}:({GetTagContents()})";
        }

        public abstract string GetTagContents();
    }
}
