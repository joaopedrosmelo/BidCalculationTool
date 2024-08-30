namespace BidCalculationTool.Core.Domain.ValueObjects
{
    public class CalculationResult
    {
        public decimal BasicFee { get; set; }
        public decimal SpecialFee { get; set; }
        public decimal AssociationFee { get; set; }
        public decimal StorageFee { get; set; }
        public decimal TotalCost { get; set; }
    }
}
