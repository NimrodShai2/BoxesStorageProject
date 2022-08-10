using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace BoxModel
{
    internal class Configuration
    {
        public ConfigData Data { get; set; }
        public Configuration()
        {
            var currentDir = Environment.CurrentDirectory;
            var fileName = "configuration.json";
            var path = Path.Combine(currentDir, fileName);
            var raw = File.ReadAllText(path);
            Data = JsonConvert.DeserializeObject<ConfigData>(raw);
        }
    }
    class ConfigData
    {
            public int MaxNumOfBoxes { get; set; }
            public int NumofDaysUntilExpired { get; set; }
            public double PrecentageAllowedToSearch { get; set; }
            public int LargestWidth { get; set; }
            public int LargestHeight { get; set; }
            public int AlmostNoBoxes { get; set; }
    }
}
