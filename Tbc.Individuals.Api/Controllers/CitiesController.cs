using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tbc.Individuals.Application.Features.Cities.Queries;

namespace Tbc.Individuals.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CitiesController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetCityResponse>>> GetCities()
    {
        var response = await sender.Send(new GetCitiesQuery());

        return Ok(response);
    }
}
