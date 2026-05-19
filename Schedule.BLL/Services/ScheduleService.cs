using AutoMapper;
using Schedule.BLL.DTO;
using Schedule.BLL.Exceptions;
using Schedule.BLL.Services.Interfaces;
using Schedule.DAL.Entities;
using Schedule.DAL.Repositories.Interfaces;
namespace Schedule.BLL.Services;

public class ScheduleService : IScheduleService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public ScheduleService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<IEnumerable<LessonDTO>> GetScheduleAsync(FilterDTO filter)
    {
        var lessons = await _uow.Lessons.GetAllAsync();

        if (filter.GroupId.HasValue)
            lessons = lessons.Where(l => l.GroupId == filter.GroupId);
        if (filter.TeacherId.HasValue)
            lessons = lessons.Where(l => l.TeacherId == filter.TeacherId);
        if (filter.ClassroomId.HasValue)
            lessons = lessons.Where(l => l.ClassroomId == filter.ClassroomId);
        if (filter.DisciplineId.HasValue)
            lessons = lessons.Where(l => l.DisciplineId == filter.DisciplineId);
        if (filter.DayOfWeek.HasValue)
            lessons = lessons.Where(l => l.DayOfWeek == filter.DayOfWeek);
        if (filter.IsOddWeek.HasValue)
            lessons = lessons.Where(l => l.IsOddWeek == filter.IsOddWeek);

        return _mapper.Map<IEnumerable<LessonDTO>>(lessons);
    }

    public async Task<IEnumerable<LessonDTO>> GetByGroupAsync(int groupId)
    {
        var lessons = await _uow.Lessons.GetByGroupAsync(groupId);
        return _mapper.Map<IEnumerable<LessonDTO>>(lessons);
    }

    public async Task<IEnumerable<LessonDTO>> GetByTeacherAsync(int teacherId)
    {
        var lessons = await _uow.Lessons.GetByTeacherAsync(teacherId);
        return _mapper.Map<IEnumerable<LessonDTO>>(lessons);
    }

    public async Task<LessonDTO> GetByIdAsync(int id)
    {
        var lesson = await _uow.Lessons.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(Lesson), id);
        return _mapper.Map<LessonDTO>(lesson);
    }

    public async Task<LessonDTO> AddLessonAsync(CreateLessonDTO dto)
    {
        await ValidateCreateDtoAsync(dto);
        await CheckConflictAsync(dto, excludeId: null);

        var lesson = _mapper.Map<Lesson>(dto);
        await _uow.Lessons.AddAsync(lesson);
        await _uow.SaveAsync();

        var created = await _uow.Lessons.GetByIdAsync(lesson.Id)
            ?? throw new NotFoundException(nameof(Lesson), lesson.Id);
        return _mapper.Map<LessonDTO>(created);
    }

    public async Task<LessonDTO> UpdateLessonAsync(int id, CreateLessonDTO dto)
    {
        var existing = await _uow.Lessons.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(Lesson), id);

        await ValidateCreateDtoAsync(dto);
        await CheckConflictAsync(dto, excludeId: id);

        _mapper.Map(dto, existing);
        _uow.Lessons.Update(existing);
        await _uow.SaveAsync();

        var updated = await _uow.Lessons.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(Lesson), id);
        return _mapper.Map<LessonDTO>(updated);
    }

    public async Task DeleteLessonAsync(int id)
    {
        var lesson = await _uow.Lessons.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(Lesson), id);

        _uow.Lessons.Delete(lesson);
        await _uow.SaveAsync();
    }

    public async Task<IEnumerable<object>> GetAllGroupsAsync()
    {
        var groups = await _uow.Groups.GetAllAsync();
        return groups.Select(g => new { g.Id, g.Name });
    }

    public async Task<IEnumerable<object>> GetAllClassroomsAsync()
    {
        var classrooms = await _uow.Classrooms.GetAllAsync();
        return classrooms.Select(c => new { c.Id, c.Number, c.Building });
    }

    // private helpers 

    private async Task ValidateCreateDtoAsync(CreateLessonDTO dto)
    {
        if (dto.LessonNumber is < 1 or > 8)
            throw new ValidationException("Номер пари має бути від 1 до 8.");

        var teacher = await _uow.Teachers.GetByIdAsync(dto.TeacherId);
        if (teacher is null)
            throw new NotFoundException(nameof(Teacher), dto.TeacherId);

        var group = await _uow.Groups.GetByIdAsync(dto.GroupId);
        if (group is null)
            throw new NotFoundException(nameof(Group), dto.GroupId);

        var classroom = await _uow.Classrooms.GetByIdAsync(dto.ClassroomId);
        if (classroom is null)
            throw new NotFoundException(nameof(Classroom), dto.ClassroomId);

        var discipline = await _uow.Disciplines.GetByIdAsync(dto.DisciplineId);
        if (discipline is null)
            throw new NotFoundException(nameof(Discipline), dto.DisciplineId);
    }

    private async Task CheckConflictAsync(CreateLessonDTO dto, int? excludeId)
    {
        var slot = await _uow.Lessons.GetByDayAsync(dto.DayOfWeek);

        var conflicts = slot.Where(l =>
            l.LessonNumber == dto.LessonNumber &&
            l.IsOddWeek == dto.IsOddWeek &&
            (excludeId == null || l.Id != excludeId) &&
            (l.TeacherId == dto.TeacherId ||
             l.GroupId == dto.GroupId ||
             l.ClassroomId == dto.ClassroomId));

        if (conflicts.Any())
            throw new ConflictException(
                "Конфлікт розкладу: викладач, група або аудиторія вже зайняті в цей слот");
    }
}
