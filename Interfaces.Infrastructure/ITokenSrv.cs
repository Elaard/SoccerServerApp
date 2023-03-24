using Common.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Infrastructure
{
    public interface ITokenSrv
    {
        string Get(UserDTO user);
    }
}
