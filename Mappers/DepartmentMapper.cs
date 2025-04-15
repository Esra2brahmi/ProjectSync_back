using projectSync_back.Dtos.Department;
using projectSync_back.Models;

namespace projectSync_back.Mappers
{
    public static class DepartmentMapper
    {
        public static DepartmentDto ToDepartmentDto(this Department departmentModel)
        {
            return new DepartmentDto
            {
                Id = departmentModel.Id,
                Name = departmentModel.Name,
                ChairName = departmentModel.ChairName,
                PhoneNumber = departmentModel.PhoneNumber,
                Email = departmentModel.Email,
                 
            };
        }

          public static Department ToDepartmentFromCreateDto(this CreateDepartmentDto departmentDto)
            {
                return new Department
                {
                    Name = departmentDto.Name,
                    ChairName = departmentDto.ChairName,
                    PhoneNumber = departmentDto.PhoneNumber,
                    Email = departmentDto.Email,
                   
                };
            }
    }
}