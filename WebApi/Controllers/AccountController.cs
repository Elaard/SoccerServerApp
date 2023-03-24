using Common.CustomNaming;
using Common.ModelsDTO;
using Interfaces.Facades;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngineeringThesis.Api.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly ITokenFcd _tokenFcd;
        private readonly IUserFcd _userFcd;

        public AccountController(ITokenFcd tokenWriter, IUserFcd userFcd)
        {
            _tokenFcd = tokenWriter;
            _userFcd = userFcd;
        }
        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] RegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = _userFcd.FindByEmail(model.Email);
            if (user != null)
                return BadRequest(ApiException.EmailIsTaken);

            var userIdentity = new UserDTO { Email = model.Email, UserName = model.UserName };

            try
            {
                var result = await _userFcd.Create(model);

                if (!result) return BadRequest();

                string token = _tokenFcd.Get(userIdentity);

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiException.IncorrectData);

            bool result = await _userFcd.Login(model);
            if (!result)
                return Unauthorized();

            var user = _userFcd.FindByEmail(model.Email);
            string token = _tokenFcd.Get(user);

            return Ok(new { Token = token });
        }
    }
}
