using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tbc.Individuals.Api.Models;
using Tbc.Individuals.Application.Features.Individuals.Commands.Create;
using Tbc.Individuals.Application.Features.Individuals.Commands.Delete;
using Tbc.Individuals.Application.Features.Individuals.Commands.Update;
using Tbc.Individuals.Application.Features.Individuals.Queries;

namespace Tbc.Individuals.Api.Controllers;

[ApiController]
[Route("individuals")]
public class IndividualsController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateIndividualModel model)
    {
        var result = await sender.Send(new CreateIndividualCommand(
            model.FirstName,
            model.LastName,
            model.Gender,
            model.PersonalId,
            model.DateOfBirth,
            model.City,
            [.. model.PhoneNumbers.Select(_ => ValueTuple.Create(_.Number, _.Type))]));

        return CreatedAtAction(nameof(GetById), new { id = result }, null);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateIndividualModel model)
    {
        await sender.Send(new UpdateIndividualCommand(id, model.FirstName, model.LastName, model.Gender, model.PersonalId, model.DateOfBirth, model.City,
            [.. model.PhoneNumbers.Select(_ => (_.Number, _.Type))]));

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await sender.Send(new DeleteIndividualCommand(id));
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetIndividualByIdResponse>> GetById(int id)
    {
        var result = await sender.Send(new GetIndividualByIdQuery(id));
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<FilterIndividualsResponse>> GetAll([FromQuery] FilterIndividualsQuery query)
    {
        var result = await sender.Send(query);
        return Ok(result);
    }

    [HttpPost("{id}/related-individuals")]
    public async Task<ActionResult> AddRelatedIndividual(int id, [FromBody] AddRelatedIndividualModel model)
    {
        await sender.Send(new AddRelatedIndividualCommand(id, model.Id, model.RelationshipType));

        return NoContent();
    }

    [HttpDelete("{id}/related-individuals/{relatedId}")]
    public async Task<ActionResult> RemoveRelatedIndividual(int id, int relatedId)
    {
        await sender.Send(new RemoveRelatedIndividualCommand(id, relatedId));

        return NoContent();
    }

    [HttpPost("{id}/profile-image")]
    public async Task<ActionResult> UploadProfileImage(int id, [FromForm] UploadProfileImageModel model)
    {
        await sender.Send(new UploadProfileImageCommand(id, model.Image.FileName, model.Image.OpenReadStream()));
        return NoContent();
    }

    [HttpDelete("{id}/profile-image")]
    public async Task<ActionResult> DeleteProfileImage(int id)
    {
        await sender.Send(new DeleteProfileImageCommand(id));
        return NoContent();
    }
}