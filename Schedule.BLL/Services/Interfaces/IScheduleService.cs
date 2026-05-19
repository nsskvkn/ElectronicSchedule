using Schedule.BLL.DTO;
namespace Schedule.BLL.Services.Interfaces;
public interface IScheduleService
{
    Task<IEnumerable<LessonDTO>> GetScheduleAsync(FilterDTO filter);
    Task<IEnumerable<LessonDTO>> GetByGroupAsync(int groupId);
    Task<IEnumerable<LessonDTO>> GetByTeacherAsync(int teacherId);
    Task<LessonDTO> GetByIdAsync(int id);
    Task<LessonDTO> AddLessonAsync(CreateLessonDTO dto);
    Task<LessonDTO> UpdateLessonAsync(int id, CreateLessonDTO dto);
    Task DeleteLessonAsync(int id);
    Task<IEnumerable<object>> GetAllGroupsAsync();
    Task<IEnumerable<object>> GetAllClassroomsAsync();
}