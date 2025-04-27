using System;
using System.ComponentModel.DataAnnotations;


namespace projectSync_back.Dtos.Task
{
    public class AssignTaskToUserDto
    {
        public int TaskId { get; set; }
        public int UserId { get; set; }
    }
}