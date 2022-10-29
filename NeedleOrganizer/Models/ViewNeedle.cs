using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeedleOrganizer.Models
{
    public class ViewNeedle
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Size { get; set; }
        public string Length { get; set; }
        public bool HasLength { get; set; }
        public string Manufacturer { get; set; }
        public string Image { get; set; } = "needles.png";
    }
}
