using BidCalculationTool.Core.Domain.Interfaces;

namespace BidCalculationTool.Core.Services.FeeStrategies
{
    public class CommonVehicleFeeStrategy : IFeeStrategy
    {
        public decimal CalculateBasicFee(decimal price)
        {
            return Math.Clamp(price * 0.10m, 10m, 50m);
        }

        public decimal CalculateSpecialFee(decimal price)
        {
            return price * 0.02m;
        }
    }
}
