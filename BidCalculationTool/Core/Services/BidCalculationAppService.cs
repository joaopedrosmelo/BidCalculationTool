using BidCalculationTool.Core.Domain.Enums;
using BidCalculationTool.Core.Domain.Interfaces;

namespace BidCalculationTool.Core.Services
{
    public class BidCalculationAppService : IBidCalculationAppService
    {
        private readonly IBidCalculationService _bidCalculationService;

        public BidCalculationAppService(IBidCalculationService bidCalculationService)
        {
            _bidCalculationService = bidCalculationService;
        }

        public object CalculateBid(decimal vehiclePrice, VehicleType vehicleType)
        {
            var calculationResult = _bidCalculationService.CalculateTotalBid(vehiclePrice, vehicleType);

            return new
            {
                VehiclePrice = vehiclePrice,
                VehicleType = vehicleType.ToString(),
                Fees = new
                {
                    Basic = calculationResult.BasicFee,
                    Special = calculationResult.SpecialFee,
                    Association = calculationResult.AssociationFee,
                    Storage = calculationResult.StorageFee
                },
                calculationResult.TotalCost
            };
        }
    }
}
