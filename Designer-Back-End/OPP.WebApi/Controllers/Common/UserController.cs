using ARDZ.Core;
using ARDZ.Helper;
using Core.Component.Library.WebTools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace ARDZ.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/User/[action]")]
    public class UserController : ControllerBase
    {
        private IUser _process;
        public UserController(IUser process)
        {
            _process = process;
        }

        [HttpPost]
        public HttpMessage<dynamic> List(Dictionary<string, object> req)
        {

            //TODO: llamar al metodo que valida el usuario en la base de datos.
            var data = _process.List(new PaginationModel
            {
                PageIndex = req.GetInteger("pageIndex"),
                PageSize = req.GetInteger("pageSize")
            }, req.GetInteger("dataBaseId"), req.GetInteger("all"));

            return new HttpMessage<dynamic>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Data = data
            };
        }

        [HttpPost]
        public HttpMessage<dynamic> ListAll(Dictionary<string, object> req)
        {

            //TODO: llamar al metodo que valida el usuario en la base de datos.
            var data = _process.List(req.GetInteger("dataBaseId"), req.GetInteger("all"));

            return new HttpMessage<dynamic>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Data = new
                {
                    list = data.Item1,
                    userSelected = data.Item2
                }
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

        [HttpPost]
        public HttpMessage<dynamic> SaveExistsUsers(Dictionary<string, object> data)
        {

            //TODO: llamar al metodo que valida el usuario en la base de datos.
            _process.SaveExistsUsers(data);

            return new HttpMessage<dynamic>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true
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
                Data = new
                {
                    row = data.Item1,
                    role = data.Item2
                }
            };
        }
    }
}
