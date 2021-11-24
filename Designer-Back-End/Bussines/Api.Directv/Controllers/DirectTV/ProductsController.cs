using ARDZ.Core;
using ARDZ.Helper;
using Core.Component.Library.WebTools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace ARDZ.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/Products/[action]")]
    public class ProductsController : ControllerBase
    {
        private IProduct _process;
        public ProductsController(IProduct process)
        {
            _process = process;
        }

        [HttpGet]
        public HttpMessage<dynamic> GetInventoryGroupList()
        {

            //TODO: llamar al metodo que valida el usuario en la base de datos.
            var data = _process.InventoryGroupList();

            return new HttpMessage<dynamic>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Data = data
            };
        }

        [HttpPost]
        public HttpMessage<dynamic> List(Dictionary<string, object> req)
        {

            //TODO: llamar al metodo que valida el usuario en la base de datos.
            var data = _process.List(new PaginationModel
            {
                PageIndex = req.GetInteger("pageIndex"),
                PageSize = req.GetInteger("pageSize")
            }, req);
     
            return new HttpMessage<dynamic>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Data = data
            };
        }

        [HttpGet]
        public HttpMessage<dynamic> GetDetails(int Id)
        {

            //TODO: llamar al metodo que valida el usuario en la base de datos.
            var data = _process.GetDetails( new Dictionary<string, object> { { "Id", Id } });
            
            return new HttpMessage<dynamic>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Data = data
            };
        }

        [HttpGet]
        public HttpMessage<dynamic> GetExistence(int Id)
        {

            //TODO: llamar al metodo que valida el usuario en la base de datos.
            var data = _process.GetExistence(new Dictionary<string, object> { { "Id", Id } });

            return new HttpMessage<dynamic>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Data = data
            };
        }

        [HttpGet]
        public HttpMessage<dynamic> GetSettings()
        {

            //TODO: llamar al metodo que valida el usuario en la base de datos.
            var data = _process.GetSettings();

            return new HttpMessage<dynamic>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Data = data
            };
        }

        [HttpPost]
        public HttpMessage<dynamic> Save(Dictionary<string, object> data)
        {
            //TODO: llamar al metodo que valida el usuario en la base de datos.
            var id = _process.Save( data);

            return new HttpMessage<dynamic>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Data = id
            };
        }
    }
}
