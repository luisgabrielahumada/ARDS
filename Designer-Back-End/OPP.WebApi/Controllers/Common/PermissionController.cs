using ARDZ.Core;
using ARDZ.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace ARDZ.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Permission/[action]")]
    public class PermissionController : ControllerBase
    {
        private IPermission _process;
        public PermissionController(IPermission process)
        {
            _process = process;
        }

        [HttpGet]
        public HttpMessage<dynamic> List(int isProcess)
        {

            //TODO: llamar al metodo que valida el usuario en la base de datos.
            var data = _process.List(isProcess);

            return new HttpMessage<dynamic>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Data = data
            };
        }

        [HttpGet]
        public HttpMessage<dynamic> Get(int id)
        {

            //TODO: llamar al metodo que valida el usuario en la base de datos.
            var data = _process.Get(id);

            return new HttpMessage<dynamic>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Data = data
            };
        }

        [HttpPost]
        public HttpMessage<dynamic> Delete(Dictionary<string, object> data)
        {

            //TODO: llamar al metodo que valida el usuario en la base de datos.
            _process.Delete(data);

            return new HttpMessage<dynamic>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true
            };
        }

        [HttpPost]
        public HttpMessage<dynamic> Save(Dictionary<string,object> data)
        {

            //TODO: llamar al metodo que valida el usuario en la base de datos.
            _process.Save(data);

            return new HttpMessage<dynamic>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true
            };
        }
    }
}
