using Xunit;

namespace Desafio.API.Domain.Tests
{
    public class DataForCalculationTest
    {
        const int MONTHS = 12;
        const double VALUE = 100.52;

        [Fact]
        public void ShoudAcceptValidValueUsingConstructor()
        {
            var data = new DataForCalculation(deadlineForRedemption: MONTHS, value: VALUE);

            Assert.Equal(data.Value, VALUE);
            Assert.Equal(data.DeadlineForRedemption, MONTHS);
        }

        [Fact]
        public void ShoudAcceptSettingValidValue()
        {
            var data = new DataForCalculation
            {
                DeadlineForRedemption = MONTHS,
                Value = VALUE
            };

            Assert.Equal(data.Value, VALUE);
            Assert.Equal(data.DeadlineForRedemption, MONTHS);
        }

        [Fact]
        public void ShoudNotAcceptNegativeValue()
        {
            Assert.Throws<DomainException>(() => new DataForCalculation(value: -1, deadlineForRedemption: MONTHS));

        }

        [Fact]
        public void ShoudNotAcceptDeadlineForRedemptionLowerThan2()
        {
            Assert.Throws<DomainException>(() => new DataForCalculation(value: VALUE, deadlineForRedemption: 1));

        }
    }
}
