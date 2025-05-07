using MediatR;
using Tbc.Individuals.Application.Helpers;
using Tbc.Individuals.Domain.Repositories;

namespace Tbc.Individuals.Application.Features.Cities.Queries;

public record GetCitiesQuery() : IRequest<IEnumerable<GetCityResponse>>;

public class GetCitiesQueryHandler(ICitiesRepository cityRepository, LocalizationHelper localization) : IRequestHandler<GetCitiesQuery, IEnumerable<GetCityResponse>>
{
    public async Task<IEnumerable<GetCityResponse>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
    {
        var cities = await cityRepository.GetAllAsync(cancellationToken);
        return cities.Select(city => new GetCityResponse(city.Id, city.Name.GetTranslatedValue(localization.CurrentCulture, localization.DefaultCulture)));
    }
}
