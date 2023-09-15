using Desafio.API.Domain;

namespace Desafio.API.Service
{
    public class CdbService : ICdbService
    {
        private const double TB = 1.08; //108%
        private const double CDI = 0.009; //0.9%
        private const double FACTOR = 1 + TB * CDI;

        public ProfitabilityResult Calculate(DataForCalculation data)
        {
            var grossIncome = CalculateGrossIncome(data);
            var taxRate = GetTaxRate(data.DeadlineForRedemption);
            var netIncome = GetNetIncome(grossIncome, data.Value, taxRate);
            return new ProfitabilityResult(grossIncome, netIncome);
        }

        private double CalculateGrossIncome(DataForCalculation data)
        {
            double finalValue = 0;
            double initialValue = data.Value;
            for (int months = 0; months < data.DeadlineForRedemption; months++)
            {
                finalValue = initialValue * FACTOR;
                initialValue = finalValue;
            }

            return finalValue;
        }

        private double GetTaxRate(int months)
        {
            if (months < 7) return 0.225;
            if (months < 13) return 0.2;
            if (months < 25) return 0.175;
            return 0.15;
        }

        private double GetNetIncome(double grossIncome, double initialValue, double taxRate)
        {
            var profit = grossIncome - initialValue;
            var tax = profit * taxRate;
            return grossIncome - tax;
        }
    }
}
