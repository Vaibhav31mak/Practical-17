namespace Practical17.Application.Contracts;

public interface IStudentService
{
    Task<Result<StudentDto>> GetByIdAsync(Guid id);
    Task<Result<IReadOnlyList<StudentDto>>> GetAllAsync();
    Task<Result<StudentDto>> CreateAsync(CreateStudentDto dto);
    Task<Result<StudentDto>> UpdateAsync(Guid id, UpdateStudentDto dto);
    Task<Result<bool>> DeleteAsync(Guid id);
}
