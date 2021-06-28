using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace VehicleQuotes.Models
{
    [Index(nameof(ModelStyleYearID), IsUnique = true)]
    public class QuoteOverride
    {
        public int ID { get; set; }
        public int ModelStyleYearID { get; set; }
        public int Price { get; set; }

        public ModelStyleYear ModelStyleYear { get; set; }
    }
}
