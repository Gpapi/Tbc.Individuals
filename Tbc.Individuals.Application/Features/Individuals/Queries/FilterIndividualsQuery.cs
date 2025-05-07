using MediatR;
using Tbc.Individuals.Application.Helpers;
using Tbc.Individuals.Domain.Entities;
using Tbc.Individuals.Domain.Repositories;

namespace Tbc.Individuals.Application.Features.Individuals.Queries;

public record FilterIndividualsQuery(string? SearchTerm, DateOnly? BirthDateFrom, DateOnly? BirthDateTo,
    Genders? Gender, int? City, int PageSize = 20, int PageNumber = 1) : IRequest<PaginatedResponse<FilterIndividualsResponse>>;

public class FilterIndividualsQueryHandler(IIndividualsRepository individualsRepository)
    : IRequestHandler<FilterIndividualsQuery, PaginatedResponse<FilterIndividualsResponse>>
{
    public async Task<PaginatedResponse<FilterIndividualsResponse>> Handle(FilterIndividualsQuery request, CancellationToken cancellationToken)
    {
        (List<Individual> Data, int Count) = await individualsRepository.Filter(request.SearchTerm, request.BirthDateFrom, request.BirthDateTo,
            request.Gender, request.City, request.PageSize, request.PageNumber);

        var individuals = Data.Select(i => new FilterIndividualsResponse(i.Id, i.FirstName.ToString(), i.LastName.ToString(),
            i.PersonalId.ToString(), i.DateOfBirth.Value, i.Gender, i.ProfileImage)).ToList();

        return new PaginatedResponse<FilterIndividualsResponse>(Count, request.PageSize, request.PageNumber, individuals);
    }
}

public record FilterIndividualsResponse(int Id, string FirstName, string LastName, string PersonalId, DateOnly DateOfBirth,
    Genders Gender, string? ProfileImage);
