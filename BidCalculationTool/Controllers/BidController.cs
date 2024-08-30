using Microsoft.AspNetCore.Mvc;
using BidCalculationTool.Core.Domain.Enums;
using BidCalculationTool.Core.Domain.Interfaces;
using BidCalculationTool.Core.Domain.Query;

namespace BidCalculatorBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BidCalculationController : ControllerBase
    {
        private readonly IBidCalculationAppService _bidCalculationAppService;

        public BidCalculationController(IBidCalculationAppService bidCalculationAppService)
        {
            _bidCalculationAppService = bidCalculationAppService;
        }

        [HttpGet]
        public IActionResult Calculate([FromQuery] BidCalculationQuery bidCalculationQuery)
        {
            var result = _bidCalculationAppService.CalculateBid(bidCalculationQuery.VehiclePrice, bidCalculationQuery.VehicleType);
            return Ok(result);
        }
    }
}