using MysteryCrateEditor.Libraries.MysteryCrate.Rewards;
using MysteryCrateEditor.Libraries.MysteryCrates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCrateEditor.Libraries.Storage
{
    interface IStorage
    {
        List<Crate> GetCrates();
        List<String> GetCrateNames();
        void SaveCrate(Crate crate);
    }
}
