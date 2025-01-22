namespace Restaurants.Domain.Exceptions;
public class NotFoundException(string resourceType, string resourceidentifier) :Exception($"{resourceType} with id = {resourceidentifier} doesn't exist")
{
}
