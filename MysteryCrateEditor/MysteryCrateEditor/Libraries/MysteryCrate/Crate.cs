using MysteryCrateEditor.Libraries.MysteryCrate.Rewards;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCrateEditor.Libraries.MysteryCrates
{
    /// <summary>
    /// Provides all the information needed for a crate
    /// </summary>
    public class Crate : INotifyPropertyChanged
    {
        public Crate(String name)
        {
            // Initialize our variables
            Id = Guid.NewGuid();
            Rewards = new List<Reward>();
            Name = name;
        }
        public CrateType Type { get; set; }
        public Guid Id { get; set; }
        [JsonProperty(ItemTypeNameHandling = TypeNameHandling.Arrays)]
        public List<Reward> Rewards { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool Preview { get; set; }
        public CrateShop Shop { get; set; }
        public CrateMessages Message { get; set; }
        public CrateEffects effect { get; set; }
        public void NotifyUpdate(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
    public enum CrateType
    {
        SUPPLYCRATE,
        MYSTRYCRATE,
        MENUCRATE,
        KEYCRATE,
        ROULETTEKEYCRATE,
        CSGOKEYCRATE
    }
    public class CrateShop
    {
        public bool Enabled { get; set; }
        public int Buy { get; set; }
    }
    public class CrateMessages
    {
        public string OnOpen { get; set; }
        public string Broadcast { get; set; }
    }
    public class CrateEffects
    {

    }
}
