using NeedleOrganizer.Interfaces;
using NeedleOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NeedleOrganizer.Services
{
    public class NeedleService : INeedleService
    {
        string addNeedle = ",\r\n  {\r\n    \"Id\": 5,\r\n    \"Type\": \"Rundsticka\",\r\n    \"Size\": 10,\r\n    \"Length\": 100,\r\n    \"Manufacturer\": \"Drops\",\r\n    \"IsAvailable\": false,\r\n    \"OnProject\": \"Används till fiskartröja.\"\r\n  }";

        List<Needle> needles = new List<Needle>();

        public async Task<List<Needle>> GetNeedles()
        {
            if (needles.Count > 0)
            {
                return needles;
            }

            string dataFile = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "needledata.json");

            // open file from AppDataDirectory, throws exception if it doesn't exist
            try
            {
                using Stream readStream = File.OpenRead(dataFile);
                using StreamReader reader = new StreamReader(readStream);
                string content = await reader.ReadToEndAsync();
                needles = JsonSerializer.Deserialize<List<Needle>>(content);

                return needles;
            }
            // open file with default needles from AppPAckage and save it to file in AppDataDiractory
            catch
            {
                using Stream readStream = await FileSystem.OpenAppPackageFileAsync("package_needledata.json");
                using StreamReader reader = new StreamReader(readStream);
                string content = await reader.ReadToEndAsync();
                needles = JsonSerializer.Deserialize<List<Needle>>(content);
                string newContent = JsonSerializer.Serialize(needles);
                string targetFile = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "needledata.json");
                using FileStream outputStream = System.IO.File.OpenWrite(targetFile);
                using StreamWriter writer = new StreamWriter(outputStream);
                await writer.WriteAsync(newContent);

                return needles;
            }
        }

        public async Task AddNeedle(Needle needle)
        {
            string dataFile = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "needledata.json");
            using Stream readStream = File.OpenRead(dataFile);
            using StreamReader reader = new StreamReader(readStream);
            string content = await reader.ReadToEndAsync();
            List<Needle> needles = JsonSerializer.Deserialize<List<Needle>>(content);
            needles.Add(needle);
            string newContent = JsonSerializer.Serialize(needles);

            DeleteAppDataNeedlesFile();

            string targetFile = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "needledata.json");
            using FileStream outputStream = System.IO.File.OpenWrite(targetFile);
            using StreamWriter writer = new StreamWriter(outputStream);
            await writer.WriteAsync(newContent);
        }

        public void DeleteAppDataNeedlesFile()
        {
            var path = FileSystem.Current.AppDataDirectory;
            var targetFile = Path.Combine(path, "needledata.json");
            File.Delete(targetFile);
        }
    }
}
