using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoData.Dtos;

namespace ToDoData.Responses;

public class LoginResponse : BaseReponse
{
    public UserDto? User { get; set; }
}
