namespace TollFeeCalculator;

public class ExpectedSingleDateException : Exception
{
    public ExpectedSingleDateException(string message)
        : base(message) { }
}
