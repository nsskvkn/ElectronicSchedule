using AutoMapper;
using Schedule.BLL.DTO;
using Schedule.DAL.Entities;

namespace Schedule.BLL.Mapping;

public class ScheduleMappingProfile : Profile
{
    public ScheduleMappingProfile()
    {
        // Lesson → LessonDTO
        CreateMap<Lesson, LessonDTO>()
            .ForMember(dto => dto.DisciplineName,
                opt => opt.MapFrom(l => l.Discipline.Name))
            .ForMember(dto => dto.TeacherFullName,
                opt => opt.MapFrom(l =>
                    $"{l.Teacher.LastName} {l.Teacher.FirstName} {l.Teacher.Patronymic}".Trim()))
            .ForMember(dto => dto.GroupName,
                opt => opt.MapFrom(l => l.Group.Name))
            .ForMember(dto => dto.ClassroomNumber,
                opt => opt.MapFrom(l => l.Classroom.Number))
            .ForMember(dto => dto.ClassroomBuilding,
                opt => opt.MapFrom(l => l.Classroom.Building));

        // CreateLessonDTO → Lesson
        CreateMap<CreateLessonDTO, Lesson>();

        // Teacher → TeacherDTO
        CreateMap<Teacher, TeacherDTO>()
            .ForMember(dto => dto.DepartmentName,
                opt => opt.MapFrom(t => t.Department.Name));

        // User → UserDTO
        CreateMap<User, UserDTO>();
    }
}