namespace Practical17.Application.Services;

// Sealed class StudentService implements the IStudentService interface and provides methods to
// manage student data. Used C# 12 Primary Constructors to inject dependencies.
public sealed class StudentService(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ILogger<StudentService> logger) : IStudentService
{
    /// <summary>
    /// Gets a student by their Guid. 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Returns the student if found, otherwise returns a failure result.</returns>
    public async Task<Result<StudentDto>> GetByIdAsync(Guid id)
    {
        var student = await unitOfWork.Students.GetByIdAsync(id);
        if (student == null)
            return Result<StudentDto>.Failure("Student not found.");

        return Result<StudentDto>.Success(mapper.Map<StudentDto>(student));
    }

    /// <summary>
    /// Gets all students. 
    /// </summary>
    /// <returns>Returns a ReadOnlyList of all students.</returns>
    public async Task<Result<IReadOnlyList<StudentDto>>> GetAllAsync()
    {
        var students = await unitOfWork.Students.GetAllAsync();

        return Result<IReadOnlyList<StudentDto>>.Success(
            [.. students.Select(mapper.Map<StudentDto>)]);
    }

    /// <summary>
    /// Creates a new student based on the provided CreateStudentDto.
    /// </summary>
    /// <param name="dto"></param>
    /// <returns>Returns the created student if successful, otherwise returns a failure result.</returns>
    public async Task<Result<StudentDto>> CreateAsync(CreateStudentDto dto)
    {
        var student = mapper.Map<Student>(dto);
        await unitOfWork.Students.AddAsync(student);
        await unitOfWork.CommitAsync();
        logger.LogInformation("Created student {StudentId}", student.Id);

        return Result<StudentDto>.Success(mapper.Map<StudentDto>(student));
    }

    /// <summary>
    /// Updates an existing student based on the provided UpdateStudentDto.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns>Returns the updated student if successful, otherwise returns a failure result.</returns>
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

    /// <summary>
    /// Deletes a student by their Guid.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Returns a success result if the student is deleted, otherwise returns a failure result.</returns>
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