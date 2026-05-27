namespace Practical17.Application.Dtos.Students;

// There is no need separate DTOs for create and update in this simple case,
// but it's a good practice to have them in case the requirements diverge in the future.
// Used records for DTOs following best practices for immutability and value equality.
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
