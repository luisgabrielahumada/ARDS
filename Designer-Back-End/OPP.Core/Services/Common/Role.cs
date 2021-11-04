using Common.Shared;
using Core.Component.Library.PagerRecord;
using Core.Component.Library.SQL;
using Core.Component.Library.WebTools;
using System.Collections.Generic;
using System.Linq;

namespace ARDZ.Core
{
    public class Role : IRole
    {
        public AppDataBase db;
        public readonly Settings _settings;
        public Role(Settings settings)
        {
            _settings = settings;
            db = new AppDataBase(_settings.ConnectionStrings.Cnn);
        }

        public void Delete(Dictionary<string, object> data)
        {
            new Execute(db, "DB_Role_Delete",
                                    new
                                    {
                                        Id = data.GetInteger("id"),
                                        Status = data.GetBoolean("status")
                                    })
                                    .Procedure<dynamic>()
                                    .FirstOrDefault();
        }
        public dynamic List(PaginationModel pag, int DatabaseId)
        {
            var data = new Execute(db, "DB_Role_List",
                                   new
                                   {
                                       pPageIndex = pag.PageIndex,
                                       pPageSize = pag.PageSize,
                                       DatabaseId = DatabaseId
                                   })
                                    .Procedure<dynamic>()
                                    .ToList();
            var totalRecords = data.Count == 0 ? 0 : (int)data.FirstOrDefault().totalRecords;
            return new WebPagerRecord<dynamic>(list: data, page: pag.PageIndex, pageSize: pag.PageSize, allItemsCount: totalRecords);
        }

        public int Save(Dictionary<string, object> data)
        {
            return new Execute(db, "DB_Role_Save",
                                     new
                                     {
                                         pId = data.GetInteger("id"),
                                         pName = data.GetString("name"),
                                         pStatus = data.GetBoolean("status")
                                     })
                                     .Procedure<int>()
                                     .FirstOrDefault();
        }

        public dynamic Get(Dictionary<string, object> data)
        {
            return new Execute(db, "DB_Role_Get",
                                    new { pId = data.GetInteger("id") })
                                      .Procedure<dynamic>()
                                    .FirstOrDefault();
        }
    }
}
