using Common.Shared;
using Core.Component.Library.SQL;
using Core.Component.Library.WebTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ARDZ.Core
{
    public class Upload : IUpload
    {
        public AppDataBase db;
        public readonly Settings _settings;
        public Upload(Settings settings)
        {
            _settings = settings;
            db = new AppDataBase(_settings.ConnectionStrings.Cnn);
        }

        public void UploadFile(Context context, Dictionary<string, object> req)
        {
            string url = string.Empty;
            string pathString = string.Empty;
            if (_settings.UploadFile.IsLocal)
            {
                var body = req.GetString("file");
                var byteArray = Convert.FromBase64String(body);
                pathString = Path.Combine(_settings.UploadFile.Path, req.GetString("tableName"));
                if (!Directory.Exists(pathString))
                    Directory.CreateDirectory(pathString);

                pathString = Path.Combine(pathString, req.GetString("name"));
                if (!File.Exists(pathString))
                {
                    using (var fs = new FileStream(pathString, FileMode.Create, FileAccess.Write))
                    {
                        fs.Write(byteArray, 0, byteArray.Length);
                    }
                }
                url = $"{_settings.UploadFile.WebSite}/{req.GetString("tableName")}/{req.GetString("name")}";
            }
            //TODO: falta definie le tema de Azure.
            _ = new Execute(context.appDataBase ?? db, "DB_Files_Save",
                                   new
                                   {
                                       pRecordId = req.GetInteger("recordId"),
                                       pTableName = req.GetString("tableName"),
                                       pName = req.GetString("name"),
                                       pContenType = req.GetString("contenType"),
                                       pPath = pathString,
                                       pUrl = url
                                   })
                                   .Procedure<dynamic>()
                                   .FirstOrDefault();
        }

        public dynamic GetFiles(Context context, Dictionary<string, object> req)
        {
            var data = new Execute(context.appDataBase ?? db, "DB_Files_Get",
                                    new
                                    {
                                        pRecordId = req.GetInteger("id"),
                                        pTableName = req.GetString("tableName"),
                                    })
                                    .Procedure<dynamic>()
                                    .ToList();
            return data;
        }

        public void DeleteFile(Context context, Dictionary<string, object> req)
        {
            _ = new Execute(context.appDataBase ?? db, "DB_Files_Delete",
                                  new
                                  {
                                      pId = req.GetInteger("id")
                                  })
                                  .Procedure<dynamic>()
                                  .FirstOrDefault();
        }
    }
}
