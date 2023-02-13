using NeedleOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NeedleOrganizer
{
    class Utils
    {       
        public static async Task CreateTestNeedles()
        {
            using Stream readStream = await FileSystem.OpenAppPackageFileAsync("test_needles.json");
            using StreamReader reader = new StreamReader(readStream);
            string content = await reader.ReadToEndAsync();
            List<Needle> needles = JsonSerializer.Deserialize<List<Needle>>(content);
            string newContent = JsonSerializer.Serialize(needles);
            string targetFile = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "needles.json");
            using FileStream outputStream = System.IO.File.OpenWrite(targetFile);
            using StreamWriter writer = new StreamWriter(outputStream);
            await writer.WriteAsync(newContent);
        }
    }
}
