using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCrateEditor.Libraries.Storage
{
    /// <summary>
    /// Provides simple serialization for user preferences
    /// </summary>
    public class Preferences
    {
        private Preferences()
        {
            crateDirectoryLocations = new List<string>();
        }

        public static Preferences loadPreferences()
        {
            var prefFile = doFileCheck();
            try
            {
                // Read the file
                using (var streamReader = new StreamReader(prefFile))
                {
                    // Read the entire file from stream
                    var prefText = streamReader.ReadToEnd();
                    // Parse it and serialize it to a Preferences object
                    return JObject.Parse(prefText).ToObject<Preferences>();
                }
            }
            catch
            {

            }
            // Just going to return this then...
            return new Preferences();
        }

        public void savePreferences()
        {
            // Check the file and get it's path
            var filePath = doFileCheck();
            // Serialize the JSON into a string
            var jsonOutput = JObject.FromObject(this).ToString();
            // Open a write to the file
            using (var writer = new StreamWriter(filePath, false))
            {
                writer.Write(jsonOutput);
            }
            

        }

        /// <summary>
        /// Does a check on the preferences file
        /// </summary>
        /// <returns>Returns full file path to the preferences file</returns>
        private static string doFileCheck()
        {
            var dirPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MysteryCrateEditor";
            // Create directory if it doesn't exist
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            // Create the preferences file if it doesn't exist
            string preferences = dirPath + "/preferences.json";
            if (!File.Exists(preferences))
            {
                File.Create(preferences);
            }
            return preferences;
        }
        [JsonProperty]
        public string DefaultLocation { get; set; }

        [JsonProperty]
        public int NumberOfBackups { get; set; }

        [JsonProperty]
        public List<string> crateDirectoryLocations;
    }
}
