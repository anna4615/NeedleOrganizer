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

        List<Needle> needles = new List<Needle>();

        public async Task<List<Needle>> GetNeedles()
        {
            if (needles.Count > 0)
            {
                return needles;
            }

            using var stream = await FileSystem.OpenAppPackageFileAsync("needledata.json");
            using var reader = new StreamReader(stream);
            string content = await reader.ReadToEndAsync();
            needles = JsonSerializer.Deserialize<List<Needle>>(content);

            return needles;
        }
    }
}
