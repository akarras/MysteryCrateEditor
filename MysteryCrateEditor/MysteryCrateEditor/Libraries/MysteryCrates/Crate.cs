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
        public Crate(String name)
        {
            // Initialize our variables
            Id = Guid.NewGuid();
            Rewards = new List<Reward>();
            Name = name;
        }

        // Public variables, whaddaya gonna do about it?
        public List<Reward> Rewards;
        public Guid Id;
        public String Name;
    }
}
