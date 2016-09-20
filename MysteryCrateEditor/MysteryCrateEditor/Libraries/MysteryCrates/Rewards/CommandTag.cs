using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCrateEditor.Libraries.MysteryCrate.Rewards
{
    public class CommandTag : TagBase
    {
        string Command;
        public CommandTag(string command):base("cmd")
        {
            Command = command;
        }
        public override string GetTagContents()
        {
            Command = Command.Replace("\"", "\\\"");
            return Command;
        }
    }
}
