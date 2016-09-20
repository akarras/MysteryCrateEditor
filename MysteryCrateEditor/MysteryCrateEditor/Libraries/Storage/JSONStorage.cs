using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MysteryCrateEditor.Libraries.MysteryCrate.Rewards;
using MysteryCrateEditor.Libraries.MysteryCrates;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;

namespace MysteryCrateEditor.Libraries.Storage
{
    /// <summary>
    /// Serves up rewards from a simple JSON storage system
    /// </summary>
    public class JSONStorage : IStorage
    {
        private List<Crate> _crates;

        #region interface_methods
        public void DeleteCrate(Crate crate)
        {
            // This really should be it's own method...
            string localPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MysteryCrateEditor/crates";
            // Delete the crate :'(
            string cratePath = $"{localPath}/{crate.Id}.json";
            if(File.Exists(cratePath))
            {
                File.Delete(cratePath);
            }
        }

        public List<string> GetCrateNames()
        {
            if(_crates == null)
            {
                GetCrates();
            }
            List<string> crateNames = new List<string>();
            foreach(Crate crate in _crates)
            {
                crateNames.Add(crate.Name);
            }
            return crateNames;
        }

        public List<Crate> GetCrates()
        {
            List<Crate> crates = new List<Crate>();
            // Iterate through all of the files found in the crate directory
            string[] files = getFiles();
            foreach(string filePath in files)
            {
                // See if the crate loads correctly before adding it
                Crate potentialCrate;
                tryCrateLoad(filePath, out potentialCrate);
                if (potentialCrate != null)
                {
                    crates.Add(potentialCrate);
                }
            }
            _crates = crates;
            return crates;
        }

        public void SaveCrate(Crate crate)
        {
            // Double check the directory exists before we save to it
            string localPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+"\\MysteryCrateEditor/crates";
            if (!Directory.Exists(localPath))
            {
                Directory.CreateDirectory(localPath);
            }
            // Creates or overwrites our crate file
            using (FileStream crateFile = File.Create($"{localPath}/{crate.Id}.json"))
            {
                // Opens a new writer for us
                using (StreamWriter writer = new StreamWriter(crateFile))
                {
                    // Load our crate into a JObject
                    string jsonData = JsonConvert.SerializeObject(crate, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });
                    // Write it out to our file
                    writer.Write(jsonData);
                }
            }


        }
        #endregion
        #region helpers
        /// <summary>
        /// Get a list of all the files in the crates directory in the local application data folder
        /// </summary>
        /// <returns>File paths to crate files</returns>
        private string[] getFiles()
        {
            string localPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+"\\MysteryCrateEditor\\crates";
            if (!Directory.Exists(localPath))
            {
                Directory.CreateDirectory(localPath);
            }
            return Directory.GetFiles(localPath);
        }

        private void tryCrateLoad(string filePath, out Crate crate)
        {
            // Open the file stream
            using (FileStream stream = File.OpenRead(filePath))
            {
                // Make a new reader using that stream
                using(StreamReader reader = new StreamReader(stream))
                {
                    // Deserialize the file into our crate
                    crate = JsonConvert.DeserializeObject<Crate>(reader.ReadToEnd(), new JsonSerializerSettings() {
                        TypeNameHandling = TypeNameHandling.Auto
                    });
                }
            }
        }
        #endregion
    }
}
