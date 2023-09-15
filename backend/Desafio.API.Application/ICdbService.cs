using Desafio.API.Domain;

namespace Desafio.API.Service
{
    public interface ICdbService
    {
        ProfitabilityResult Calculate(DataForCalculation data);
    }
}
