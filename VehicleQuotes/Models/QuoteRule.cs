using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using VehicleQuotes.Validation;

namespace VehicleQuotes.Models
{
    [Index(nameof(FeatureType), nameof(FeatureValue), IsUnique = true)]
    public class QuoteRule
    {
        public static class FeatureTypes
        {
            public static string BodyType = "BodyType";
            public static string Size = "Size";
            public static string ItMoves = "ItMoves";
            public static string HasAllWheels = "HasAllWheels";
            public static string HasAlloyWheels = "HasAlloyWheels";
            public static string HasAllTires = "HasAllTires";
            public static string HasKey = "HasKey";
            public static string HasTitle = "HasTitle";
            public static string RequiresPickup = "RequiresPickup";
            public static string HasEngine = "HasEngine";
            public static string HasTransmission = "HasTransmission";
            public static string HasCompleteInterior = "HasCompleteInterior";

            public static string[] All => new string[] {
                BodyType, Size, ItMoves, HasAllWheels, HasAlloyWheels, HasAllTires,
                HasKey, HasTitle, RequiresPickup, HasEngine, HasTransmission, HasCompleteInterior
            };
        }

        public int ID { get; set; }

        [FeatureType]
        public string FeatureType { get; set; }
        public string FeatureValue { get; set; }
        public int PriceModifier { get; set; }
    }
}
