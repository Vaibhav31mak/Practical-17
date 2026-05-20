namespace Practical17.Api.Endpoints.StudentEndpoints;

// Get all students endpoint
public class GetAllStudentsEndpoint(IStudentService studentService) : BaseEndpoint
{
    [HttpGet("api/students")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> HandleAsync()
    {
        var result = await studentService.GetAllAsync();

        if (!result.IsSuccess)
            return BadRequest(result.ErrorMessage);

        return Ok(result.Value);
    }
}