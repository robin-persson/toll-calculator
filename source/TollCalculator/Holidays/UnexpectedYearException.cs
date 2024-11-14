namespace TollFeeCalculator.Holidays
{
    [Serializable]
    public class UnexpectedYearException : Exception
    {
        public UnexpectedYearException() { }

        public UnexpectedYearException(string? message)
            : base(message) { }

        public UnexpectedYearException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
