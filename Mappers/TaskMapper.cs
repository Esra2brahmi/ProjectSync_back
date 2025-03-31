using projectSync_back.Dtos.Task;
using projectSync_back.Models;

namespace projectSync_back.Mappers
{
    public static class TaskMapper
{

    public static TaskDto ToTaskDto(this ProjectTask taskModel){
            return new TaskDto{
                Id = taskModel.Id,
                ProjectId=taskModel.ProjectId,
                TaskName =taskModel.TaskName,
                TaskDescription = taskModel.TaskDescription,
                DueDate = taskModel.DueDate,
            };
            
        }
    public static ProjectTask ToTaskFromCreateDto(this CreateTaskRequestDto dto,int projectId)
    {
        return new ProjectTask
        {
            TaskName = dto.TaskName,
            ProjectId=projectId,
            TaskDescription = dto.TaskDescription,
            DueDate = dto.DueDate
        };
    }

    public static ProjectTask ToTaskFromUpdateDto(this UpdateTaskRequestDto dto)
    {
        return new ProjectTask
        {
            TaskName = dto.TaskName,
            TaskDescription = dto.TaskDescription,
            DueDate = dto.DueDate
        };
    }
}
}
