using ARDZ.Core;
using ARDZ.Helper;
using Common.Shared;
using Core.Component.Library.WebTools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace ARDZ.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Truck/[action]")]
    public class TruckeController : ControllerBase
    {
        private ITruck _process;
        public TruckeController(ITruck process)
        {
            _process = process;
        }

        [HttpPost]
        public HttpMessage<dynamic> List(Dictionary<string, object> req)
        {

            //TODO: llamar al metodo que valida el usuario en la base de datos.
            var data = _process.List(new Context
            {
                Token = HttpContext.Request.Query["token"]
            }, new PaginationModel
            {
                PageIndex = req.GetInteger("pageIndex"),
                PageSize = req.GetInteger("pageSize")
            });

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
            var data = _process.Get(new Dictionary<string, object> { { "id", id } });

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
        public HttpMessage<dynamic> Save(Dictionary<string, object> data)
        {

            //TODO: llamar al metodo que valida el usuario en la base de datos.
            var id = _process.Save(data);

            return new HttpMessage<dynamic>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Data = id
            };
        }
    }
}
