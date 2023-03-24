using Common.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngineeringThesis.Api.Controllers
{
    public class ExternalAuthController:BaseApiController
    {
        public ExternalAuthController()
        {

        }
        [HttpGet("facebook")]
        public IActionResult Facebook([FromBody]FacebookAuthViewModel model)
        {
            return Ok();
        }
    }
}
