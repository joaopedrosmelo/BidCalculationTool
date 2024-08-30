namespace BidCalculationTool.Core.Domain.Interfaces
{
    public interface IFeeStrategy
    {
        decimal CalculateBasicFee(decimal price);
        decimal CalculateSpecialFee(decimal price);
    }

}
