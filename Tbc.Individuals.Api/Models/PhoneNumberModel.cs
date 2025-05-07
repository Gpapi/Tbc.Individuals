using Tbc.Individuals.Domain.Entities;

namespace Tbc.Individuals.Api.Models;

public record PhoneNumberModel(PhoneTypes Type, string Number);
