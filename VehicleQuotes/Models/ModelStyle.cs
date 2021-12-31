using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VehicleQuotes.Models
{
    [Index(nameof(ModelID), nameof(BodyTypeID), nameof(SizeID), IsUnique = true)]
    public class ModelStyle
    {
        public int ID { get; set; }
        public int ModelID { get; set; }
        public int BodyTypeID { get; set; }
        public int SizeID { get; set; }

        public Model Model { get; set; }
        public BodyType BodyType { get; set; }
        public Size Size { get; set; }

        public ICollection<ModelStyleYear> ModelStyleYears { get; set; }
    }
}
