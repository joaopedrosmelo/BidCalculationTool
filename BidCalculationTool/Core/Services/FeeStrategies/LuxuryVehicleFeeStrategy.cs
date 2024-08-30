using BidCalculationTool.Core.Domain.Interfaces;

namespace BidCalculationTool.Core.Services.FeeStrategies
{
    public class LuxuryVehicleFeeStrategy : IFeeStrategy
    {
        public decimal CalculateBasicFee(decimal price)
        {
            return Math.Clamp(price * 0.10m, 25m, 200m);
        }

        public decimal CalculateSpecialFee(decimal price)
        {
            return price * 0.04m;
        }
    }
}
