using Common.Shared;
using Core.Component.Library.PagerRecord;
using Core.Component.Library.SQL;
using Core.Component.Library.WebTools;
using System.Collections.Generic;
using System.Linq;

namespace ARDZ.Core
{
    public class Truck : ITruck
    {
        public AppDataBase db;
        public readonly Settings _settings;
        public Truck(Settings settings)
        {
            _settings = settings;
            db = new AppDataBase(_settings.ConnectionStrings.Cnn);
        }


        public dynamic List(Context context, PaginationModel pag)
        {
            var data = new Execute(context.appDataBase, "DB_Truck_List",
                                    new {
                                        pPageIndex =  pag.PageIndex,
                                        pPageSize = pag.PageSize 
                                      })
                                    .Procedure<dynamic>()
                                    .ToList();
            var totalRecords = data.Count == 0 ? 0 : (int)data.FirstOrDefault().totalRecords;
            return new WebPagerRecord<dynamic>(list: data, page: pag.PageIndex, pageSize: pag.PageSize, allItemsCount: totalRecords);
        }

        public dynamic Get(Dictionary<string, object> data)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Dictionary<string, object> data)
        {
            throw new System.NotImplementedException();
        }

        public int Save(Dictionary<string, object> data)
        {
            throw new System.NotImplementedException();
        }
    }
}
