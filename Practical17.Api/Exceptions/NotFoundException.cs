namespace Practical17.Api.Exceptions;

public sealed class NotFoundException(string message) : Exception(message);
