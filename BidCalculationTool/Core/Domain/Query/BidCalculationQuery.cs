using BidCalculationTool.Core.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BidCalculationTool.Core.Domain.Query
{
    public class BidCalculationQuery
    {
        [Required, Range(0.01, double.MaxValue, ErrorMessage = "Vehicle price must be greater than 0.")]
        public decimal VehiclePrice { get; set; }

        [Required]
        public VehicleType VehicleType { get; set; }
    }
}