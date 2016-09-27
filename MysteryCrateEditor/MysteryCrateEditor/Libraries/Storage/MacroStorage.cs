using MysteryCrateEditor.Libraries.MysteryCrate.Rewards;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCrateEditor.Libraries.Storage
{
    public class MacroStorage
    {
        private static MacroStorage _storage;
        public static MacroStorage Storage
        {
            get
            {
                if(_storage == null)
                {
                    _storage = new MacroStorage();
                }
                return _storage;
            }
            set
            {
                _storage = value;
            }
        }

        private List<RewardMacro> _macros;
        public List<RewardMacro> Macros {
            get
            {
                if(_macros == null)
                {
                    _macros = getMacros();
                }
                return _macros;
            }
            set
            {
                _macros = value;
            }
        }

        public void Save()
        {
            saveMacros(_macros);
        }
        private string macroFilePath { get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/macros.json";
            }
        }
        private List<RewardMacro> getMacros()
        {
            if (File.Exists(macroFilePath))
            {
                using (StreamReader reader = new StreamReader(macroFilePath))
                {
                    // TODO probably could use better error handling here someday.
                    string jsonText = reader.ReadToEnd();
                    List<RewardMacro> macroList = JsonConvert.DeserializeObject<List<RewardMacro>>(jsonText);

                    return macroList;
                }
            }
            return null;
        }

        private void saveMacros(List<RewardMacro> macros)
        {
            using (StreamWriter writer = new StreamWriter(File.OpenWrite(macroFilePath)))
            {
                writer.Write(JsonConvert.SerializeObject(macros));
            }


        }
    }
}
