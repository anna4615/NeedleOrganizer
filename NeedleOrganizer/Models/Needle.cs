using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeedleOrganizer.Models
{
    public class Needle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double Size { get; set; }
        public int? Length { get; set; }
        public string Manufacturer { get; set; }
        public string Image { get; set; } = "needles.png";
        public bool IsAvailable { get; set; } = true;
        public string OnProject { get; set; } = "De här stickorna är lediga.";
        public string Comment { get; set; } = "Finns ingen kommentar.";

    }
}
