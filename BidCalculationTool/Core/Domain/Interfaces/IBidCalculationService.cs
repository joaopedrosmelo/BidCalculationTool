using BidCalculationTool.Core.Domain.Enums;
using BidCalculationTool.Core.Domain.ValueObjects;

namespace BidCalculationTool.Core.Domain.Interfaces
{
    public interface IBidCalculationService
    {
        CalculationResult CalculateTotalBid(decimal vehiclePrice, VehicleType vehicleType);
    }
}