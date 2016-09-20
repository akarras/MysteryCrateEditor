using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCrateEditor.Libraries.MysteryCrate.Rewards
{
    public interface IRewardTag
    {
        string GetTag();
        string GetTagContents();
    }
}
