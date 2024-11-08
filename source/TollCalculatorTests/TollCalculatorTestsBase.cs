using FluentAssertions;

namespace TollCalculatorTests
{
    public class TollCalculatorTestsBase
    {
        protected int? result;

        protected void ThenItReturnsAResult()
        {
            result.Should().NotBe(null);
        }
    }
}
