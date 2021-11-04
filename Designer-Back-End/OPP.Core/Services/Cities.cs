using Common.Shared;
using Core.Component.Library.PagerRecord;
using Core.Component.Library.SQL;
using Core.Component.Library.WebTools;
using System.Collections.Generic;
using System.Linq;

namespace ARDZ.Core
{
    public class Cities : ICities
    {
        public AppDataBase db;
        public readonly Settings _settings;
        public Cities(Settings settings)
        {
            _settings = settings;
            db = new AppDataBase(_settings.ConnectionStrings.Cnn);
        }

        public void Delete(Dictionary<string, object> data)
        {
            new Execute(db, "DB_Cities_Delete",
                                      new
                                      {
                                          id = data.GetInteger("id"),
                                          value = data.GetBoolean("value")
                                      })
                                     .Procedure<dynamic>()
                                     .FirstOrDefault();
        }

        public dynamic List(PaginationModel pag)
        {
            var data = new Execute(db, "DB_Cities_List",
                                    new { pag.PageIndex, pag.PageSize },
                                    new { pag.TotalRecords })
                                    .Procedure<dynamic>();
            return new WebPagerRecord<dynamic>(list: data, page: pag.PageIndex, pageSize: pag.PageSize, allItemsCount: pag.TotalRecords);
        }

        public void Save(Dictionary<string, object> data)
        {
            _ = new Execute(db, "DB_Cities_Save",
                                      new
                                      {
                                          cityId = data.GetInteger("cityId"),
                                          IsActive = data.GetBoolean("isActive"),
                                          City = data.GetString("city"),
                                          CountryId = data.GetInteger("countryId")
                                      })
                                      .Procedure<dynamic>()
                                      .FirstOrDefault();
        }

        public dynamic Get(Dictionary<string, object> data)
        {
            return new Execute(db, "DB_Cities_Get",
                                    new { id = data.GetInteger("id") })
                                    .Procedure<dynamic>();
        }
    }
}
