using Common.Shared;
using Core.Component.Library.PagerRecord;
using Core.Component.Library.SQL;
using Core.Component.Library.WebTools;
using System.Collections.Generic;
using System.Linq;

namespace ARDZ.Core
{
    public class Product : IProduct
    {
        public AppDataBase db;
        public readonly Settings _settings;
        public Product(Settings settings)
        {
            _settings = settings;
            db = new AppDataBase(_settings.ConnectionStrings.Cnn);
        }

        public dynamic InventoryGroupList()
        {
            var data = new ExecuteMySQL(db, "DB_InventoryGroupList")
                                    .Procedure<dynamic>()
                                    .ToList();
            return data;
        }
        public dynamic List(PaginationModel pag, Dictionary<string, object> req)
        {
            var data = new ExecuteMySQL(db, "DB_Product_List",
                                    new
                                    {
                                        pInventoryGroup = req.GetInteger("group"),
                                        pSku = req.GetString("sku"),
                                        pPart = req.GetString("part"),
                                        pPageIndex = pag.PageIndex,
                                        pPageSize = pag.PageSize,
                                    })
                                    .Procedure<dynamic>()
                                    .ToList();
            var totalRecords = data.Count == 0 ? 0 : (int)data.FirstOrDefault().totalRecords;
            return new WebPagerRecord<dynamic>(list: data, page: pag.PageIndex, pageSize: pag.PageSize, allItemsCount: totalRecords);
        }

        public dynamic GetDetails(Dictionary<string, object> data)
        {

            return new ExecuteMySQL(db, "DB_Details_Product",
                                    new
                                    {
                                        pId = data.GetInteger("Id")
                                    })
                                  .Procedure<dynamic>()
                                  .ToList();
        }
        public dynamic GetExistence( Dictionary<string, object> data)
        {

            return new ExecuteMySQL(db, "DB_Existence_Product",
                                    new
                                    {
                                        pId = data.GetInteger("Id")
                                    })
                                  .Procedure<dynamic>()
                                  .ToList();
        }

        public int Save(Dictionary<string, object> data)
        {
            var res = new ExecuteMySQL(db, "DB_UpdateProduct",
                                           new
                                           {
                                               pId = data.GetInteger("id"),
                                               pName = data.GetString("name"),
                                               pDescription = data.GetString("description"),
                                           })
                                         .Procedure<int>().FirstOrDefault();
            return res;

        }
        public dynamic GetSettings()
        {
            return  _settings.DirectTV;
        }
    }
}
