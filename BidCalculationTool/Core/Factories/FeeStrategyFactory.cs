using BidCalculationTool.Core.Domain.Enums;
using BidCalculationTool.Core.Domain.Interfaces;
using BidCalculationTool.Core.Services.FeeStrategies;

namespace BidCalculationTool.Core.Factories
{
    public class FeeStrategyFactory
    {
        public IFeeStrategy GetFeeStrategy(VehicleType vehicleType)
        {
            return vehicleType switch
            {
                VehicleType.Luxury => new LuxuryVehicleFeeStrategy(),
                _ => new CommonVehicleFeeStrategy(),
            };
        }
    }
}