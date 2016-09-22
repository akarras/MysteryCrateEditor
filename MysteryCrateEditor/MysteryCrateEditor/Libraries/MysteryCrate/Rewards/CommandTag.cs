using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCrateEditor.Libraries.MysteryCrate.Rewards
{
    public class CommandTag : TagBase
    {
        [JsonProperty("Command")]
        public string Command { get; set; }
        public CommandTag():base("cmd")
        {
            Command = "";
        }
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
