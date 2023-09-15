using Desafio.API.Controllers;
using Desafio.API.Domain;
using Desafio.API.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Desafio.API.Tests
{
    public class CdbControllerTest
    {
        [Fact]
        public void ShoudReturnCorrectCalculation()
        {
            double gross = 1000;
            double net = 950;
            var data = new DataForCalculation(100, 2);
            var profitability = new ProfitabilityResult(gross, net);
            var mockService = new Mock<ICdbService>();
            mockService
                .Setup(svc => svc.Calculate(data))
                .Returns(profitability);
            var controller = new CdbController(mockService.Object);

            var result = controller.Calculate(data);

            Assert.IsType<ActionResult<ProfitabilityResult>>(result);
        }

        [Fact]
        public void ShouldReturnBadRequestWhenValueIsInvalid()
        {
            var mockService = new Mock<ICdbService>();
            var controller = new CdbController(mockService.Object);

            Assert.Throws<DomainException>(() => controller.Calculate(new DataForCalculation(0, 2)));
        }
    }
}
