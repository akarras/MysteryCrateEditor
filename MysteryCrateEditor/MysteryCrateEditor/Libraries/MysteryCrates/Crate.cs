using MysteryCrateEditor.Libraries.MysteryCrate.Rewards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCrateEditor.Libraries.MysteryCrates
{
    /// <summary>
    /// Provides all the information needed for a crate
    /// </summary>
    public class Crate
    {
        public Crate()
        {
            // Initialize our variables
            id = Guid.NewGuid();
            rewards = new List<Reward>();
        }

        // Public variable, whaddaya gonna do about it?
        public List<Reward> rewards;
        public Guid id;
        public String name;
    }
}
