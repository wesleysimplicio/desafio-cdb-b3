using System.Text.Json.Serialization;

namespace Desafio.API.Domain
{
    public class ProfitabilityResult
    {
        [JsonPropertyName("valor_bruto")]
        public double GrossIncome { get; private set; }
        [JsonPropertyName("valor_liquido")]
        public double NetIncome { get; private set; }

        public ProfitabilityResult(double grossIncome, double netIncome)
        {
            this.GrossIncome = grossIncome;
            this.NetIncome = netIncome;
        }
    }
}
