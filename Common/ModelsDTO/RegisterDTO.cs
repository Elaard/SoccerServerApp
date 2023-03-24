using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ModelsDTO
{
    public class RegisterDTO: LoginDTO
    {
        public string UserName { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}
