namespace IBM.Core.Interfaces
{
    public interface IRateOperationWith
    {
        decimal ConvertInverseTo(decimal from, decimal rate);
        decimal CalculateNewAmount(decimal from, decimal rate);
    }
}