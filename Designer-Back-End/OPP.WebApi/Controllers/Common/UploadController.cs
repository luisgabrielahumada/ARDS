using ARDZ.Core;
using ARDZ.Helper;
using Common.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace ARDZ.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/Upload/[action]")]
    public class UploadController : ControllerBase
    {
        private IUpload _process;
        public UploadController(IUpload process)
        {
            _process = process;
        }

        [HttpPost]
        public HttpMessage<dynamic> UploadFile(IFormCollection files, int id, string tableName)
        {
            // full path to file in temp location
            foreach (var file in files.Files)
            {
                string s = string.Empty;
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        s = Convert.ToBase64String(fileBytes);
                    }
                }
                if (string.IsNullOrEmpty(s))
                    continue;

                _process.UploadFile(new Context
                {
                    Token = HttpContext.Request.Query["token"]
                }, new Dictionary<string, object>
                {
                    { "recordId", id },
                    { "tableName", tableName },
                    { "name", file.FileName },
                    { "contenType", file.ContentType },
                    { "file", s }
                });
            }

            return new HttpMessage<dynamic>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true
            };
        }

        [HttpGet]
        public HttpMessage<dynamic> Get(int id, string tableName)
        {
            //TODO: llamar al metodo que valida el usuario en la base de datos.
            var data = _process.GetFiles(new Context
            {
                Token = HttpContext.Request.Query["token"]
            }, new Dictionary<string, object>
                {
                    { "id", id },
                    { "tableName", tableName },
                });

            return new HttpMessage<dynamic>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Data = data
            };
        }


        [HttpPost]
        public HttpMessage<dynamic> DeleteFile(Dictionary<string, object> req)
        {
            // full path to file in temp location
            _process.DeleteFile(new Context
            {
                Token = HttpContext.Request.Query["token"]
            }, req);

            return new HttpMessage<dynamic>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true
            };
        }
    }
}
