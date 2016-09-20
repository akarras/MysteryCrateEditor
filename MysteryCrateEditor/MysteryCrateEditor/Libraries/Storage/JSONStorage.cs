using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MysteryCrateEditor.Libraries.MysteryCrate.Rewards;
using MysteryCrateEditor.Libraries.MysteryCrates;

namespace MysteryCrateEditor.Libraries.Storage
{
    /// <summary>
    /// Serves up rewards from a simple JSON storage system
    /// </summary>
    public class JSONStorage : IStorage
    {
        public List<string> GetCrateNames()
        {
            throw new NotImplementedException();
        }

        public List<Crate> GetCrates()
        {
            throw new NotImplementedException();
        }

        public void SaveCrate(Crate crate)
        {
            throw new NotImplementedException();
        }
    }
}
