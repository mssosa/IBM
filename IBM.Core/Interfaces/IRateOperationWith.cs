namespace IBM.Core.Interfaces
{
    public interface IRateOperationWith
    {
        decimal CalculateNewAmountInverse(decimal from, decimal rate);
        decimal CalculateNewAmount(decimal from, decimal rate);
    }
}