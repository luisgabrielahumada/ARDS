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
    [Route("api/Templates/[action]")]
    public class TemplatesController : ControllerBase
    {
        private ITemplates _process;
        public TemplatesController(ITemplates process)
        {
            _process = process;
        }

        [HttpGet]
        [AllowAnonymous]
        public HttpMessage<dynamic> List(int pageIndex, int pageSize)
        {

            //TODO: llamar al metodo que valida el usuario en la base de datos.
            var data = _process.List(new PaginationModel
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            });

            return new HttpMessage<dynamic>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Data = data
            };
        }

        [HttpGet]
        [AllowAnonymous]
        public HttpMessage<dynamic> Get(string id)
        {

            //TODO: llamar al metodo que valida el usuario en la base de datos.
            var data = _process.Get(id);

            return new HttpMessage<dynamic>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Data = new
                {
                    info = data.Item1,
                    items = data.Item2
                }
            };
        }

        [HttpPost]
        [AllowAnonymous]
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
        [AllowAnonymous]
        public HttpMessage<dynamic> Save(Dictionary<string, object> data)
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
