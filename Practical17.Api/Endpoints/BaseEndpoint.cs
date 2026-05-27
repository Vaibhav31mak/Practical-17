namespace Practical17.Api.Endpoints;

// Base Endpoint class that all other endpoints will inherit from.
// It provides common configurations and behaviors for all API endpoints.
// I have used separate Endpoint classes for each operation (Get, Post, Put, Delete)
// to keep the code organized and maintainable and follow SRP.
[ApiController]
[Produces("application/json")]
public abstract class BaseEndpoint : ControllerBase
{
}