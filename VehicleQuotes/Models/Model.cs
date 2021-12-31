using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VehicleQuotes.Models
{
    [Index(nameof(Name), nameof(MakeID), IsUnique = true)]
    public class Model
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int MakeID { get; set; }

        public Make Make { get; set; }

        public ICollection<ModelStyle> ModelStyles { get; set; }
    }
}
