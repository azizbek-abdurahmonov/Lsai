namespace Lsai.Domain.Common.Exceptions;

public class EntityNotFoundException(Type type) : Exception($"An entity of type {nameof(type)} - was not found!")
{
}
