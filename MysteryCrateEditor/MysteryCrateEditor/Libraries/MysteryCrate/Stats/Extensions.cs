using MysteryCrateEditor.Libraries.MysteryCrates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCrateEditor.Libraries.MysteryCrate.Stats
{
    public static class StatExtensions
    {
        public static Stats getStats(this Crate crate)
        {
            // You know you're off to a great start when you work on the extension before the other class
            // Insomnia programming FTW
            var stats = new Stats(crate.Rewards);
            return stats;
        }
    }
}
