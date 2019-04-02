using System;
using Microsoft.AspNetCore.Mvc;
using Distance.Business.Abstractions;
using Distance.Contracts;
using EnsureThat;

namespace Distance.Api.Controllers
{
    [Route("api/[controller]")]
    public class MeasureController : Controller
    {
        private readonly IDistanceService _distanceService;
        public MeasureController(IDistanceService distanceService)
        {
            Ensure.That(distanceService).IsNotNull();

            _distanceService = distanceService;
        }

        [HttpPost("[action]")]
        public ActionResult<DistanceModel> Distance([FromBody]Coordinates c)
        {
            Ensure.That(c).IsNotNull();

            if (!ModelState.IsValid)
            {
                return BadRequest($"Invalid arguments {nameof(c.From)} and {nameof(c.To)}");    
            }

            return Ok(_distanceService.Calculate(c.From, c.To));
        }   
    }
}