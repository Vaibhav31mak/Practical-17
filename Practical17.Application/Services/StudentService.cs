namespace Practical17.Application.Services;

public sealed class StudentService(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ILogger<StudentService> logger) : IStudentService
{
    public async Task<Result<StudentDto>> GetByIdAsync(Guid id)
    {
        var student = await unitOfWork.Students.GetByIdAsync(id);

        if (student == null)
            return Result<StudentDto>.Failure("Student not found.");

        return Result<StudentDto>.Success(mapper.Map<StudentDto>(student));
    }

    public async Task<Result<IReadOnlyList<StudentDto>>> GetAllAsync()
    {
        var students = await unitOfWork.Students.GetAllAsync();

        return Result<IReadOnlyList<StudentDto>>.Success(
            students.Select(mapper.Map<StudentDto>).ToList());
    }

    public async Task<Result<StudentDto>> CreateAsync(CreateStudentDto dto)
    {
        var student = mapper.Map<Student>(dto);

        await unitOfWork.Students.AddAsync(student);
        await unitOfWork.CommitAsync();

        logger.LogInformation("Created student {StudentId}", student.Id);
        return Result<StudentDto>.Success(mapper.Map<StudentDto>(student));
    }

    public async Task<Result<StudentDto>> UpdateAsync(Guid id, UpdateStudentDto dto)
    {
        var student = await unitOfWork.Students.GetByIdAsync(id);

        if (student == null)
            return Result<StudentDto>.Failure("Student not found.");

        mapper.Map(dto, student);

        unitOfWork.Students.Update(student);
        await unitOfWork.CommitAsync();

        logger.LogInformation("Updated student {StudentId}", student.Id);
        return Result<StudentDto>.Success(mapper.Map<StudentDto>(student));
    }

    public async Task<Result<bool>> DeleteAsync(Guid id)
    {
        var student = await unitOfWork.Students.GetByIdAsync(id);

        if (student == null)
            return Result<bool>.Failure("Student not found.");

        unitOfWork.Students.Delete(student);
        await unitOfWork.CommitAsync();

        logger.LogInformation("Deleted student {StudentId}", student.Id);
        return Result<bool>.Success(true);
    }
}