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
        private string storageLocation;
        /// <summary>
        /// Creates a JSON storage in the given directory
        /// </summary>
        /// <param name="directory">Full path to the crate directory</param>
        public JSONStorage(string directory)
        {
            if(!Directory.Exists(directory))
            {
                throw new DirectoryNotFoundException();
            }
            // As long as we've found a directory set it to the current storage location
            storageLocation = directory;
        }

        public JSONStorage()
        {
            // Default storage location
            storageLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MysteryCrateEditor/crates";
        }


        private List<Crate> _crates;

        #region interface_methods
        public void DeleteCrate(Crate crate)
        {
            // Delete the crate :'(
            string cratePath = $"{storageLocation}/{crate.Id}.json";
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
            
            if (!Directory.Exists(storageLocation))
            { 
                Directory.CreateDirectory(storageLocation);
            }
            // Get all .json files from the crate directory
            string[] files = Directory.GetFiles(storageLocation, "*.json");
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
            if (!Directory.Exists(storageLocation))
            {
                throw new DirectoryNotFoundException();
            }

            var numberOfBackups = Preferences.loadPreferences().NumberOfBackups;
            // Remove the topmost backup
            if(File.Exists($"{storageLocation}/{crate.Id}.json{numberOfBackups}"))
            {
                File.Delete($"{storageLocation}/{crate.Id}.json{numberOfBackups}");
            }

            // Iterate backwards through the list of backups
            for(int backup = numberOfBackups; backup > 0; backup--)
            {
                if(File.Exists($"{storageLocation}/{crate.Id}.json{backup}"))
                {
                    // Move the file one up.
                    File.Move($"{storageLocation}/{crate.Id}.json{backup}", $"{storageLocation}/{crate.Id}.json{backup + 1}");
                }
            }
            if(numberOfBackups > 0)
            {
                if(File.Exists($"{storageLocation}/{crate.Id}.json"))
                {
                    File.Move($"{storageLocation}/{crate.Id}.json", $"{storageLocation}/{crate.Id}.json1");
                }
            }

            // Creates or overwrites our crate file
            using (FileStream crateFile = File.Create($"{storageLocation}/{crate.Id}.json"))
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
        /// Get a list of all the files from the given directory
        /// </summary>
        /// <returns>File paths to crate files</returns>
        private string[] getFiles(string directory)
        {
            return Directory.GetFiles(directory,"*.json");
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
