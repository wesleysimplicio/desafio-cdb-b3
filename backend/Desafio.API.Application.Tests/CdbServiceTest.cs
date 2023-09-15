using Desafio.API.Domain;
using Desafio.API.Service;
using System;
using Xunit;

namespace Desafio.API.Tests
{
    public class CdbServiceTest
    {
        const int TOLERANCE = 6;
        private ProfitabilityResult CalculateExpectedValue(DataForCalculation data, double taxRate)
        {
            //TB = 108%, CDI = 0.9%
            const double FACTOR = 1.08 * 0.009;
            double gross = data.Value * Math.Pow(1 + FACTOR, data.DeadlineForRedemption);
            var tax = (gross - data.Value) * taxRate;
            double net = gross - tax;
            return new ProfitabilityResult(grossIncome: gross, netIncome: net);
        }

        [Fact]
        public void ShouldCalculateValidValueFor6Months()
        {
            const double TAX_RATE = 0.225;
            var data = new DataForCalculation(value: 100, deadlineForRedemption: 6);

            var result = new CdbService().Calculate(data);
            var expected = CalculateExpectedValue(data, TAX_RATE);

            Assert.Equal(result.GrossIncome, expected.GrossIncome, TOLERANCE);
            Assert.Equal(result.NetIncome, expected.NetIncome, TOLERANCE);
        }

        [Fact]
        public void ShouldCalculateValidValueFor11Months()
        {
            const double TAX_RATE = 0.2;
            var data = new DataForCalculation(value: 100, deadlineForRedemption: 11);

            var result = new CdbService().Calculate(data);
            var expected = CalculateExpectedValue(data, TAX_RATE);

            Assert.Equal(result.GrossIncome, expected.GrossIncome, TOLERANCE);
            Assert.Equal(result.NetIncome, expected.NetIncome, TOLERANCE);
        }

        [Fact]
        public void ShouldCalculateValidValueFor21Months()
        {
            const double TAX_RATE = 0.175;
            var data = new DataForCalculation(value: 100, deadlineForRedemption: 21);

            var result = new CdbService().Calculate(data);
            var expected = CalculateExpectedValue(data, TAX_RATE);

            Assert.Equal(result.GrossIncome, expected.GrossIncome, TOLERANCE);
            Assert.Equal(result.NetIncome, expected.NetIncome, TOLERANCE);
        }

        [Fact]
        public void ShouldCalculateValidValueFor27Months()
        {
            const double TAX_RATE = 0.15;
            var data = new DataForCalculation(value: 100, deadlineForRedemption: 27);

            var result = new CdbService().Calculate(data);
            var expected = CalculateExpectedValue(data, TAX_RATE);

            Assert.Equal(result.GrossIncome, expected.GrossIncome, TOLERANCE);
            Assert.Equal(result.NetIncome, expected.NetIncome, TOLERANCE);
        }
    }
}
