using BidCalculationTool.Core.Domain.Enums;

namespace BidCalculationTool.Core.Domain.Interfaces
{
    public interface IBidCalculationAppService
    {
        object CalculateBid(decimal vehiclePrice, VehicleType vehicleType);
    }
}