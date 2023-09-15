using Xunit;

namespace Desafio.API.Domain.Tests
{
    public class ProfitabilityResultTest
    {
        [Fact]
        public void ShouldAcceptValuesFromConstructor()
        {
            const double GROSS = 1000.98;
            const double NET = 950.15;

            var profitability = new ProfitabilityResult(grossIncome: GROSS, netIncome: NET);
            Assert.Equal(profitability.GrossIncome, GROSS);
            Assert.Equal(profitability.NetIncome, NET);
        }
    }
}
