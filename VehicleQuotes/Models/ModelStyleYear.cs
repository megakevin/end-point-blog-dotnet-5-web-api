using Microsoft.EntityFrameworkCore;

namespace VehicleQuotes.Models
{
    [Index(nameof(Year), nameof(ModelStyleID), IsUnique = true)]
    public class ModelStyleYear
    {
        public int ID { get; set; }
        public string Year { get; set; }
        public int ModelStyleID { get; set; }

        public ModelStyle ModelStyle { get; set; }
    }
}