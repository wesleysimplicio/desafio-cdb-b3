using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Desafio.API.Domain
{
    public class DataForCalculation
    {
        private int _deadlineForRedemption;
        private double _applicationValue;

        [JsonPropertyName("prazo")]
        [Range(minimum: 2, maximum: int.MaxValue, ErrorMessage = "O prazo para resgate deve ser superior a 1 mês")]
        public int DeadlineForRedemption 
        {
            get => _deadlineForRedemption; 
            set
            { 
                if (value < 2)
                {
                    throw new DomainException("O prazo para resgate deve ser superior a 1 mês");
                }
                _deadlineForRedemption = value;
            } 
        }

        [JsonPropertyName("valor")]
        [Range(minimum: 0.01, maximum: double.MaxValue, ErrorMessage = "O valor investido deve ser maior do que zero")]
        public double Value 
        {
            get => _applicationValue;
            set
            {
                if (value <= 0)
                {
                    throw new DomainException("O valor investido deve ser maior do que zero");
                }
                _applicationValue = value;
            }
        }

        public DataForCalculation() {}
        public DataForCalculation(double value, int deadlineForRedemption)
        {
            this.Value = value;
            this.DeadlineForRedemption = deadlineForRedemption;
        }
    }
}
