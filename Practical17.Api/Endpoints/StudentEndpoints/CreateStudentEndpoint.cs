namespace Practical17.Api.Endpoints.StudentEndpoints;

public class CreateStudentEndpoint(IStudentService studentService) : BaseEndpoint
{
    [HttpPost("api/students")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> HandleAsync([FromBody] CreateStudentDto dto)
    {
        var result = await studentService.CreateAsync(dto);

        if (!result.IsSuccess)
            return BadRequest(result.ErrorMessage);

        return Created($"/api/students/{result.Value!.Id}", result);
    }
}