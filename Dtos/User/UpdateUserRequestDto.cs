using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectSync_back.Dtos.User
{
    public class UpdateUserRequestDto
    {
          public string UserFirstName { get; set; } =string.Empty;

        public string UserLastName { get; set; } =string.Empty;

        public string Department { get; set; } =string.Empty;

        public string Email { get; set; } =string.Empty;

        public string Classe { get; set; } =string.Empty;

        public string ProjectType { get; set; } =string.Empty;

        public string SupervisorFullName { get; set; } =string.Empty;


    }}