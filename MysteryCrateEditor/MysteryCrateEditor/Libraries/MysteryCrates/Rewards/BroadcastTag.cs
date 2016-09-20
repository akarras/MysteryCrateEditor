using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCrateEditor.Libraries.MysteryCrate.Rewards
{
    public class BroadcastTag : TagBase
    {
        string broadcastMessage;

        public BroadcastTag(string message):base("broadcast")
        {
            broadcastMessage = message;
        }

        public override string GetTagContents()
        {
            return broadcastMessage;
        }
    }
}
