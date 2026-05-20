namespace Practical17.Application.Mapping;

// Following SRP, this class is responsible for mapping between Student entity and
// its related DTOs using AutoMapper.
public sealed class StudentProfile : Profile
{
    public StudentProfile()
    {
        CreateMap<Student, StudentDto>();
        CreateMap<CreateStudentDto, Student>();
        CreateMap<UpdateStudentDto, Student>();
    }
}
