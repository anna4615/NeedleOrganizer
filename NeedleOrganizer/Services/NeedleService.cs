//using Android.App.AppSearch;
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
        //string addNeedle = ",\r\n  {\r\n    \"Id\": 5,\r\n    \"Type\": \"Rundsticka\",\r\n    \"Size\": 10,\r\n    \"Length\": 100,\r\n    \"Manufacturer\": \"Drops\",\r\n    \"IsAvailable\": false,\r\n    \"OnProject\": \"Används till fiskartröja.\"\r\n  }";

        //List<Needle> needles = new List<Needle>();

        public async Task<List<Needle>> GetNeedles()
        {
            string dataFile = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "needles.json");
            string content = "";

            if (File.Exists(dataFile))
            {
                using Stream readStream = File.OpenRead(dataFile);
                using StreamReader reader = new StreamReader(readStream);
                content = await reader.ReadToEndAsync();

            }
            else
            {
                using FileStream outputStream = System.IO.File.Create(dataFile);
            }

            if (string.IsNullOrEmpty(content))
            {
                return new List<Needle>();
            }

            List<Needle> needles = JsonSerializer.Deserialize<List<Needle>>(content);
            return needles;
        }


        public async Task AddNeedle(Needle needle)
        {      

            string dataFile = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "needles.json");
            using Stream readStream = File.OpenRead(dataFile);
            using StreamReader reader = new StreamReader(readStream);
            string content = await reader.ReadToEndAsync();
            DeleteAppDataNeedlesFile();

            List<Needle> needles;

            if (string.IsNullOrEmpty(content))
            {
                needles = new List<Needle>();
                needle.Id = 1;
                needles.Add(needle);
            }
            else
            {
                needles = JsonSerializer.Deserialize<List<Needle>>(content);
                needle.Id = GetNewId(needles);
                needles.Add(needle);
            }

            string newContent = JsonSerializer.Serialize(needles);

            string targetFile = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "needles.json");
            using FileStream outputStream = System.IO.File.OpenWrite(targetFile);
            using StreamWriter writer = new StreamWriter(outputStream);
            await writer.WriteAsync(newContent);
        }

        public void DeleteAppDataNeedlesFile()
        {
            var path = FileSystem.Current.AppDataDirectory;
            var targetFile = Path.Combine(path, "needles.json");
            File.Delete(targetFile);
        }
       

        private int GetNewId(List<Needle> needles)
        {
            var lastId = needles
                .OrderByDescending(n => n.Id)
                .FirstOrDefault()
                .Id;

            int newId = lastId + 1;
            return newId;
        }

    }
}
