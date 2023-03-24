using AutoMapper;
using Common.CustomNaming;
using Common.ModelsDTO;
using Infrastructure.EFCore.Models;
using Interfaces.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.EFCore.Services
{
    public class TokenSrv: ITokenSrv
    {

        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;


        public TokenSrv(Context context, IMapper mapper, ILogger<TokenSrv> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        public string Get(UserDTO user)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            var sercretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1BC774139B30F75AFE09A503A7501ADA5C69F9993BDD376076BD94E43C6EDE53"));
            var signingCredentials = new SigningCredentials(sercretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(
                    JwtRegisteredClaimNames.Sub, user.UserName,
                    JwtRegisteredClaimNames.Email, user.Email
                    )
            };

            var tokenOptions = new JwtSecurityToken(
                issuer: "null",
                audience: "null",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}
