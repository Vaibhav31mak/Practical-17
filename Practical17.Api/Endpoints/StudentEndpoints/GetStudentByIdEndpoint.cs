namespace Practical17.Api.Endpoints.StudentEndpoints;

// Get endpoint for Student entity by ID
public class GetStudentByIdEndpoint(IStudentService studentService) : BaseEndpoint
{
    [HttpGet("api/students/{id:guid}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> HandleAsync(Guid id)
    {
        var result = await studentService.GetByIdAsync(id);

        if (!result.IsSuccess)
            return NotFound(result.ErrorMessage);

        return Ok(result.Value);
    }
}