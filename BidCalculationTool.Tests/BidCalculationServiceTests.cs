using BidCalculationTool.Core.Calculation;
using BidCalculationTool.Core.Domain.Enums;
using BidCalculationTool.Core.Domain.Interfaces;
using BidCalculationTool.Core.Factories;
using BidCalculationTool.Core.Services;
using BidCalculationTool.Core.Services.FeeStrategies;
using Microsoft.Extensions.DependencyInjection;

namespace BidCalculationTool.Tests.Calculation
{
    public class BidCalculationServiceTests
    {
        private readonly FeeStrategyFactory _feeStrategyFactory = new FeeStrategyFactory();

        private BidCalculationService CreateService()
        {
            return new BidCalculationService(_feeStrategyFactory);
        }

        [Theory]
        [InlineData(398, VehicleType.Common, 39.80, 7.96, 5.00, 100.00, 550.76)]
        [InlineData(501, VehicleType.Common, 50.00, 10.02, 10.00, 100.00, 671.02)]
        [InlineData(57, VehicleType.Common, 10.00, 1.14, 5.00, 100.00, 173.14)]
        [InlineData(1800, VehicleType.Luxury, 180.00, 72.00, 15.00, 100.00, 2167.00)]
        [InlineData(1100, VehicleType.Common, 50.00, 22.00, 15.00, 100.00, 1287.00)]
        [InlineData(1000000, VehicleType.Luxury, 200.00, 40000.00, 20.00, 100.00, 1040320.00)]
        public void CalculateTotalBid_ValidInputs_ShouldCalculateCorrectly(
            decimal vehiclePrice, VehicleType vehicleType,
            decimal expectedBasicFee, decimal expectedSpecialFee, decimal expectedAssociationFee,
            decimal expectedStorageFee, decimal expectedTotalCost)
        {
            // Arrange
            var service = CreateService();

            // Act
            var result = service.CalculateTotalBid(vehiclePrice, vehicleType);

            // Assert
            Assert.Equal(expectedBasicFee, result.BasicFee);
            Assert.Equal(expectedSpecialFee, result.SpecialFee);
            Assert.Equal(expectedAssociationFee, result.AssociationFee);
            Assert.Equal(expectedStorageFee, result.StorageFee);
            Assert.Equal(expectedTotalCost, result.TotalCost);
        }

        [Fact]
        public void GetFeeStrategy_Luxury_ShouldReturnLuxuryVehicleFeeStrategy()
        {
            // Arrange
            var factory = new FeeStrategyFactory();

            // Act
            var result = factory.GetFeeStrategy(VehicleType.Luxury);

            // Assert
            Assert.IsType<LuxuryVehicleFeeStrategy>(result);
        }

        [Fact]
        public void GetFeeStrategy_Common_ShouldReturnCommonVehicleFeeStrategy()
        {
            // Arrange
            var factory = new FeeStrategyFactory();

            // Act
            var result = factory.GetFeeStrategy(VehicleType.Common);

            // Assert
            Assert.IsType<CommonVehicleFeeStrategy>(result);
        }

        [Fact]
        public void CalculateTotalBid_ZeroVehiclePrice_CommonVehicle_ShouldReturnMinimumBasicFee()
        {
            // Arrange
            var service = CreateService();

            // Act
            var result = service.CalculateTotalBid(0, VehicleType.Common);

            // Assert
            Assert.Equal(10, result.BasicFee);
            Assert.Equal(0, result.SpecialFee);
            Assert.Equal(5, result.AssociationFee);
            Assert.Equal(100, result.StorageFee);
            Assert.Equal(115, result.TotalCost);
        }

        [Fact]
        public void CalculateTotalBid_ZeroVehiclePrice_LuxuryVehicle_ShouldReturnMinimumBasicFee()
        {
            // Arrange
            var service = CreateService();

            // Act
            var result = service.CalculateTotalBid(0, VehicleType.Luxury);

            // Assert
            Assert.Equal(25, result.BasicFee);
            Assert.Equal(0, result.SpecialFee);
            Assert.Equal(5, result.AssociationFee);
            Assert.Equal(100, result.StorageFee);
            Assert.Equal(130, result.TotalCost);
        }

        [Fact]
        public void CalculateTotalBid_NullFeeStrategy_ShouldThrowException()
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => new BidCalculationService(null));
            Assert.Equal("Value cannot be null. (Parameter 'feeStrategyFactory')", ex.Message);
        }

        [Fact]
        public void CalculateSpecialFee_CommonVehicle_ShouldCalculateCorrectly()
        {
            // Arrange
            var strategy = new CommonVehicleFeeStrategy();
            decimal vehiclePrice = 2000m;

            // Act
            var specialFee = strategy.CalculateSpecialFee(vehiclePrice);

            // Assert
            Assert.Equal(40m, specialFee);
        }

        [Fact]
        public void CalculateSpecialFee_LuxuryVehicle_ShouldCalculateCorrectly()
        {
            // Arrange
            var strategy = new LuxuryVehicleFeeStrategy();
            decimal vehiclePrice = 50000m;

            // Act
            var specialFee = strategy.CalculateSpecialFee(vehiclePrice);

            // Assert
            Assert.Equal(2000m, specialFee);
        }

        [Fact]
        public void DependencyInjection_ShouldResolveBidCalculationAppService()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddScoped<FeeStrategyFactory>();
            services.AddScoped<IBidCalculationService, BidCalculationService>();
            services.AddScoped<IBidCalculationAppService, BidCalculationAppService>();
            var provider = services.BuildServiceProvider();

            // Act
            var service = provider.GetService<IBidCalculationAppService>();

            // Assert
            Assert.NotNull(service);
        }
    }
}