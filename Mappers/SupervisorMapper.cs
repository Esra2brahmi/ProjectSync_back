using projectSync_back.Dtos.Supervisor;
using projectSync_back.Models;

namespace projectSync_back.Mappers
{
    public static class SupervisorMapper
    {
        public static SupervisorDto ToSupervisorDto(this Supervisor supervisorModel)
        {
            return new SupervisorDto
            {
                Id = supervisorModel.Id,
                FirstName = supervisorModel.FirstName,
                LastName = supervisorModel.LastName,
                Email = supervisorModel.Email,
                PhoneNumber = supervisorModel.PhoneNumber,
                Description = supervisorModel.Description,
                Address = supervisorModel.Address,
                AcademicTitle = supervisorModel.AcademicTitle,
                 DepartmentIds = supervisorModel.DepSupervisors?
                    .Select(ds => ds.DepartmentId)
                    .ToList() ?? new List<int>()
            };
        }

          public static Supervisor ToSupervisorFromCreateDto(this CreateSupervisorDto supervisorDto)
            {
                return new Supervisor
                {
                    FirstName = supervisorDto.FirstName,
                    LastName = supervisorDto.LastName,
                    Email = supervisorDto.Email,
                    PhoneNumber = supervisorDto.PhoneNumber,
                    Description = supervisorDto.Description,
                    Address = supervisorDto.Address,
                    AcademicTitle = supervisorDto.AcademicTitle,
                    // Initialize the collections
                    DepSupervisors = supervisorDto.DepartmentIds?
                        .Select(id => new DepSupervisor { DepartmentId = id })
                        .ToList() ?? new List<DepSupervisor>()
                };
            }
    }
}