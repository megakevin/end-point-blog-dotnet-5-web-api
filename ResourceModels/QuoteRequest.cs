using System.ComponentModel.DataAnnotations;
using VehicleQuotes.Validation;

namespace VehicleQuotes.ResourceModels
{
    public class QuoteRequest
    {
        [Required]
        public string Year { get; set; }
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        [VehicleBodyType]
        public string BodyType { get; set; }
        [Required]
        [VehicleSize]
        public string Size { get; set; }

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

        public string this[string propertyName]
        {
            get
            {
                return GetType().GetProperty(propertyName).GetValue(this).ToString();
            }
        }
    }
}
