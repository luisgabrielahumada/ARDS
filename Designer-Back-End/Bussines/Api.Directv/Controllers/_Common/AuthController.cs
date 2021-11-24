using Core.Component.Library.Security;
using Core.Component.Library.WebTools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ARDZ.Core;
using ARDZ.Helper;
using System;
using System.Collections.Generic;
using System.Net;

namespace ARDZ.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Auth/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration appSettings;
        private IAuth _process;
        public AuthController(IConfiguration _appSettings, IAuth process)
        {
            appSettings = _appSettings;
            _process = process;
        }

        [AllowAnonymous]
        [HttpPost]
        public HttpMessage<dynamic> SilentLogin(Dictionary<string, object> data)
        {

            //TODO: llamar al metodo que valida el usuario en la base de datos.
            var result = _process.Login(new AuthModel
            {
                Login = data.GetString("login"),
                Password = data.GetString("password"),
            });
            result = new JwtWeb().GenerateJSONWebToken(result, appSettings);

            return new HttpMessage<dynamic>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Data = result
            };
        }


        [AllowAnonymous]
        [HttpPost]
        public HttpMessage<dynamic> Login(Dictionary<string, object> data)
        {

            //TODO: llamar al metodo que valida el usuario en la base de datos.
            var result = _process.Login(new AuthModel
            {
                Login = data.GetString("login"),
                Password = data.GetString("password")
            });
            result = new JwtWeb().GenerateJSONWebToken(result, appSettings);

            return new HttpMessage<dynamic>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Data = result
            };
        }
    }
}
