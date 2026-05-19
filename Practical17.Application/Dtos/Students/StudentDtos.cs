namespace Practical17.Application.Dtos.Students;

// We don't need separate DTOs for create and update in this simple case,
// but it's a good practice to have them in case the requirements diverge in the future.
public sealed record UpdateStudentDto(
    string StudentName, 
    string RollNumber, 
    string Course, 
    DateTimeOffset DateOfBirth
);
public sealed record CreateStudentDto(
    string StudentName,
    string RollNumber,
    string Course,
    DateTimeOffset DateOfBirth
);
public sealed record StudentDto(
    Guid Id,
    string StudentName,
    string RollNumber,
    string Course,
    DateTimeOffset DateOfBirth
);
