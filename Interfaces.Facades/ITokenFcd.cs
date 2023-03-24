using Common.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Facades
{
    public interface ITokenFcd
    {
        string Get(UserDTO user);
    }
}
