using Common.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Infrastructure
{
    public interface IUserSrv
    {
        public Task<bool> Create(RegisterDTO model);
        public UserDTO FindByEmail(string email);
        Task<bool> Login(LoginDTO model);
    }
}
