using ARDZ.Core;
using ARDZ.Helper;
using Common.Shared;
using Core.Component.Library.WebTools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Threading;
using OfficeOpenXml;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Text;

namespace ARDZ.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/Order/[action]")]
    public class OrderController : ControllerBase
    {
        private IOrder _process;
        public OrderController(IOrder process)
        {
            _process = process;
        }

        [HttpPost]
        public async Task<HttpMessage<dynamic>> Import(IFormCollection files, CancellationToken cancellationToken)
        {
            var file = files.Files[0];
            // full path to file in temp location
            var filePath = Path.GetTempFileName();
            List<SerialPhoneModel> mov = new List<SerialPhoneModel>();
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream, cancellationToken);

                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.Commercial;
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        var Serial = worksheet.Cells[row, 1].Value;
                        if (!string.IsNullOrEmpty(Serial.ToString()))
                        {
                            mov.Add(new SerialPhoneModel
                            {
                                Serial = Serial.ToString(),
                                Phone = worksheet.Cells[row, 2].Value.ToString(),
                                Status = "A",
                                NroOrder = HttpContext.Request.Query["nroOrde"],
                                TypeLoad ="Massive",

                            });
                        }
                    }
                }
            }
            string json = JsonSerializer.Serialize(mov);
            var data = _process.SaveSerialPhoneMassive(json);

         
            return new HttpMessage<dynamic>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Data = mov
            };
        }

        [HttpPost]
        public HttpMessage<dynamic> List(Dictionary<string, object> req)
        {

            //TODO: llamar al metodo que valida el usuario en la base de datos.
            var data = _process.List( new PaginationModel
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
        public HttpMessage<dynamic> Get(string nroOrden)
        {

            //TODO: llamar al metodo que valida el usuario en la base de datos.
            var row = _process.Get(new Dictionary<string, object> { { "nroOrden", nroOrden } });

            return new HttpMessage<dynamic>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Data = row
            };
        }


        [HttpGet]
        public HttpMessage<dynamic> UploadSku(string nroOrden)
        {

            //TODO: llamar al metodo que valida el usuario en la base de datos.
            var row = _process.UploadSku(new Dictionary<string, object> { { "nroOrden", nroOrden } });

            return new HttpMessage<dynamic>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Data = row
            };
        }

        [HttpPost]
        public HttpMessage<dynamic> Delete(int id)
        {

            //TODO: llamar al metodo que valida el usuario en la base de datos.
            _process.Delete( id);

            return new HttpMessage<dynamic>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true
            };
        }
        
        [HttpPost]
        public HttpMessage<dynamic> ApprovedSku(Dictionary<string, object> data)
        {

            //TODO: llamar al metodo que valida el usuario en la base de datos.
            var id = _process.ApprovedSku(data);

            return new HttpMessage<dynamic>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Data = id
            };
        }
        [HttpPost]
        public HttpMessage<dynamic> Save(Dictionary<string, object> data)
        {

            //TODO: llamar al metodo que valida el usuario en la base de datos.
            var id = _process.SaveSerialPhone(data);

            return new HttpMessage<dynamic>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Data = id
            };
        }

        [HttpGet]
        public HttpMessage<dynamic> getGeoreferencing()
        {

            //TODO: llamar al metodo que valida el usuario en la base de datos.
            var row = _process.GetGeoreferencing();

            return new HttpMessage<dynamic>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Data = row
            };
        }
        [HttpPost]
        public HttpMessage<dynamic> DownloadedGeoreferencing()
        {

            //TODO: llamar al metodo que valida el usuario en la base de datos.
            var row = _process.DownloadedGeoreferencing();

            return new HttpMessage<dynamic>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Data = row
            };
        }
    }
}
