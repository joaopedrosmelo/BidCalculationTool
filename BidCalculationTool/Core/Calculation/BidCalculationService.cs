using BidCalculationTool.Core.Domain.Enums;
using BidCalculationTool.Core.Domain.Interfaces;
using BidCalculationTool.Core.Domain.ValueObjects;
using BidCalculationTool.Core.Factories;

namespace BidCalculationTool.Core.Calculation
{
    public class BidCalculationService : IBidCalculationService
    {
        private readonly FeeStrategyFactory _feeStrategyFactory;

        public BidCalculationService(FeeStrategyFactory feeStrategyFactory)
        {
            _feeStrategyFactory = feeStrategyFactory ?? throw new ArgumentNullException(nameof(feeStrategyFactory));
        }

        public CalculationResult CalculateTotalBid(decimal vehiclePrice, VehicleType vehicleType)
        {
            var feeStrategy = _feeStrategyFactory.GetFeeStrategy(vehicleType);

            var basicFee = feeStrategy.CalculateBasicFee(vehiclePrice);
            var specialFee = feeStrategy.CalculateSpecialFee(vehiclePrice);
            var associationFee = CalculateAssociationFee(vehiclePrice);
            var storageFee = 100m;

            var totalCost = vehiclePrice + basicFee + specialFee + associationFee + storageFee;

            return new CalculationResult
            {
                BasicFee = basicFee,
                SpecialFee = specialFee,
                AssociationFee = associationFee,
                StorageFee = storageFee,
                TotalCost = totalCost
            };
        }

        private decimal CalculateAssociationFee(decimal price)
        {
            if (price <= 500)
                return 5m;
            if (price <= 1000)
                return 10m;
            if (price <= 3000)
                return 15m;
            return 20m;
        }
    }
}
