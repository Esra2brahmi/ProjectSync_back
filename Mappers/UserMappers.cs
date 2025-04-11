using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projectSync_back.Dtos.User;
using projectSync_back.Models;

namespace projectSync_back.Mappers
{
    public static class UserMappers
    {
        public static UserDto ToUserDto(this User userModel){
            return new UserDto{
                Id = userModel.Id,
                UserFirstName =userModel.UserFirstName ,
                UserLastName = userModel.UserLastName,
                Department = userModel.Department,
                Email =userModel.Email,
                Classe =userModel.Classe,
                ProjectType =userModel.ProjectType,
           
            };
            
        }

        public static User ToUserFromCreateDto(this CreateUserRequestDto userDto){
            return new User{
                UserFirstName =userDto.UserFirstName ,
                UserLastName = userDto.UserLastName,
                Department = userDto.Department,
                Email =userDto.Email,
                Classe=userDto.Classe,
                ProjectType =userDto.ProjectType,
                SupervisorFullName=userDto.SupervisorFullName
            };
            
        }
        
        
        
        
        
        
        
    }
    }