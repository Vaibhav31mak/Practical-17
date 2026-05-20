namespace Practical17.Api.Endpoints.StudentEndpoints;

// Create endpoint for Student entity
public class CreateStudentEndpoint(IStudentService studentService) : BaseEndpoint
{
    [HttpPost("api/students")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> HandleAsync(CreateStudentDto dto)
    {
        var result = await studentService.CreateAsync(dto);

        if (!result.IsSuccess)
            return BadRequest(result.ErrorMessage);

        return Created($"/api/students/{result.Value!.Id}", result);
    }
}