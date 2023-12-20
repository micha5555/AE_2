using System.Text.Json;

namespace AE_2_Ziober
{
    internal class Config
    {
        public int iterations { get; set; }
        public int populationSize { get; set;}
        public double mutationProbability { get; set; }
        public List<City> cities { get; set; }


        public static Config ReadConfig()
        {
            string jsonFilePath = System.IO.Path.GetFullPath(@"..\..\..\") + "/config.json";

            string jsonContent = File.ReadAllText(jsonFilePath);
            Console.WriteLine(jsonContent);


            Config? config =
                 JsonSerializer.Deserialize<Config>(jsonContent);
            return config;
        }
    }
}
