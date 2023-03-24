using Common.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Facades
{
    public interface IUserFcd
    {
        Task<bool> Create(RegisterDTO model);
        UserDTO FindByEmail(string email);
        Task<bool> Login(LoginDTO model);
    }
}
