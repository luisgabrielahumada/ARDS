using Common.Shared;
using Core.Component.Library.SQL;
using Core.Component.Library.WebTools;
using System.Collections.Generic;
using System.Linq;

namespace ARDZ.Core
{
    public class Permission : IPermission
    {
        public AppDataBase db;
        public readonly Settings _settings;
        public Permission(Settings settings)
        {
            _settings = settings;
            db = new AppDataBase(_settings.ConnectionStrings.Cnn);
        }
        

        public void Delete(Dictionary<string, object> data)
        {
            new Execute(db, "DB_Permission_Delete",
                                      new
                                      {
                                          id = data.GetInteger("id"),
                                          value = data.GetBoolean("value")
                                      })
                                     .Procedure<dynamic>()
                                     .FirstOrDefault();
        }

        public dynamic List(int isProcess)
        {
            var data = new Execute(db, "DB_Permission_Get",
                                    new { isProcess })
                                    .Procedure<dynamic>();

            return data;
        }

        public dynamic Get(int id)
        {
            return new Execute(db, "DB_Permission_Get",
                                    new { id })
                                    .Procedure<dynamic>();
        }

        public void Save(Dictionary<string, object> data)
        {
            var permission = data.GetString("permissions");
            _ = new Execute(db, "DB_Permission_Save",
                                      new {
                                              Id = data.GetInteger("id"),
                                              Permissions = permission
                                      }) .Procedure<dynamic>();
        }
    }
}
