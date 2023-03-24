using Common.ModelsDTO;
using Interfaces.Facades;
using Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facades
{
    public class TokenFcd: ITokenFcd
    {
        private readonly ITokenSrv _tokenSrv;
        public TokenFcd(ITokenSrv tokenSrv)
        {
            _tokenSrv = tokenSrv;
        }

        public string Get(UserDTO user)
        {
            return _tokenSrv.Get(user);
        }
    }
}
