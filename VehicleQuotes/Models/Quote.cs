using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleQuotes.Models
{
    public class Quote
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        // Directly tie this quote record to a specific vehicle that we have
        // registered in our db, if we have it.
        public int? ModelStyleYearID { get; set; }

        // If we don't have the specific vehicle in our db, then store the
        // vehicle model details independently.
        public string Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int BodyTypeID { get; set; }
        public int SizeID { get; set; }

        public bool ItMoves { get; set; }
        public bool HasAllWheels { get; set; }
        public bool HasAlloyWheels { get; set; }
        public bool HasAllTires { get; set; }
        public bool HasKey { get; set; }
        public bool HasTitle { get; set; }
        public bool RequiresPickup { get; set; }
        public bool HasEngine { get; set; }
        public bool HasTransmission { get; set; }
        public bool HasCompleteInterior { get; set; }

        public int OfferedQuote { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }

        public ModelStyleYear ModelStyleYear { get; set; }

        public BodyType BodyType { get; set; }
        public Size Size { get; set; }
    }
}
