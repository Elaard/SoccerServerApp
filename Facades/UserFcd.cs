using Common.ModelsDTO;
using Interfaces.Facades;
using Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Facades
{
    public class UserFcd : IUserFcd
    {
        private readonly IUserSrv _userSrv;
        public UserFcd(IUserSrv userSrv)
        {
            _userSrv = userSrv;
        }
        public async Task<bool> Create(RegisterDTO model)
        {
            return await _userSrv.Create(model);
        }

        public UserDTO FindByEmail(string email)
        {
            return _userSrv.FindByEmail(email);
        }

        public Task<bool> Login(LoginDTO model)
        {
            return _userSrv.Login(model);
        }
    }
}
