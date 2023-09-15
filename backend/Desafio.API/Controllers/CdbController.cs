using Desafio.API.Domain;
using Desafio.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CdbController : ControllerBase
    {
        private readonly ICdbService cdbService;

        public CdbController(ICdbService cdbService)
        {
            this.cdbService = cdbService;
        }

        [HttpPost]
        public ActionResult<ProfitabilityResult> Calculate(DataForCalculation data)
        {
            return Ok(cdbService.Calculate(data));
        }
    }
}
