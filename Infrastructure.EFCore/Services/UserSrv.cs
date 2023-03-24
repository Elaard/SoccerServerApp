using AutoMapper;
using Common.CustomNaming;
using Common.ModelsDTO;
using Infrastructure.EFCore.Models;
using Interfaces.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EFCore.Services
{
    public class UserSrv: IUserSrv
    {

        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly UserManager<User> _usrMng;
        private readonly SignInManager<User> _signMng;

        public UserSrv(Context context, IMapper mapper, ILogger<UserSrv> logger, UserManager<User> userManager, SignInManager<User> signMng)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _usrMng = userManager;
            _signMng = signMng;
        }
        public async Task<bool> Login(LoginDTO model)
        {
            var result= await _signMng.CheckPasswordSignInAsync(GetByEmail(model.Email), model.Password, false);
            return result.Succeeded;
        }
        public async Task<bool> Create(RegisterDTO model)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));
            try
            {
                User user = new User
                {
                    Email = model.Email,
                    UserName=model.UserName
                };

                var result = await _usrMng.CreateAsync(user, model.Password);
                return result.Succeeded;
            }
            catch
            {
                return false;
            }
        }

        public UserDTO FindByEmail(string email)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            var model= GetByEmail(email);

            return _mapper.Map<UserDTO>(model);
        }
        private User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(x => x.Email == email);
        }
        
    }
}
