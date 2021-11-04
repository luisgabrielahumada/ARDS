using Common.Shared;
using Core.Component.Library.PagerRecord;
using Core.Component.Library.SQL;
using Core.Component.Library.WebTools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ARDZ.Core
{
    public class Templates : ITemplates
    {
        public AppDataBase db;
        public readonly Settings _settings;
        public Templates(Settings settings)
        {
            _settings = settings;
            db = new AppDataBase(_settings.ConnectionStrings.Cnn);
        }

        public void Delete(Dictionary<string, object> data)
        {
            new Execute(db, "DB_Templates_Delete",
                                      new
                                      {
                                          id = data.GetString("id"),
                                          status = data.GetBoolean("status")
                                      })
                                     .Procedure<dynamic>()
                                     .FirstOrDefault();
        }

        public dynamic List(PaginationModel pag)
        {
            var data = new Execute(db, "DB_Templates_List",
                                    new { pag.PageIndex, pag.PageSize },
                                    new { pag.TotalRecords })
                                    .Procedure<dynamic>();

            return new WebPagerRecord<dynamic>(list: data, page: pag.PageIndex, pageSize: pag.PageSize, allItemsCount: data.FirstOrDefault().TotalRecords);
        }

        public void Save(Dictionary<string, object> data)
        {
            _ = new Execute(db, "DB_Templates_Save",
                                      new {
                                           json = data.GetString("json"),
                                      })
                                      .Procedure<dynamic>()
                                      .FirstOrDefault();
        }

        public Tuple<dynamic,dynamic> Get(string id)
        {
           return new Execute(db, "DB_Templates_Get",
                                  new {
                                      id = id
                                  })
                                  .Procedure<dynamic,dynamic>();
        }
    }
}
