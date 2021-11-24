using Common.Infrastructure.Utils;
using Common.Shared;
using Core.Component.Library.PagerRecord;
using Core.Component.Library.SQL;
using Core.Component.Library.WebTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ARDZ.Core
{
    public class Menu : IMenu
    {
        public AppDataBase db;
        public readonly Settings _settings;
        public Menu(Settings settings)
        {
            _settings = settings;
            db = new AppDataBase(_settings.ConnectionStrings.Cnn);
        }

        public void Delete(Dictionary<string, object> data)
        {
            new Execute(db, "DB_Menu_Delete",
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
            var data = new Execute(db, "DB_Menu_List",
                                   new
                                   {
                                       pPageIndex = pag.PageIndex,
                                       pPageSize = pag.PageSize,
                                       DatabaseId = DatabaseId,
                                   })
                                    .Procedure<dynamic>()
                                    .ToList();
            var totalRecords = data.Count == 0 ? 0 : (int)data.FirstOrDefault().totalRecords;
            return new WebPagerRecord<dynamic>(list: data, page: pag.PageIndex, pageSize: pag.PageSize, allItemsCount: totalRecords);
        }
        public int Save(Dictionary<string, object> data)
        {
            return  new Execute(db, "DB_Menu_Save",
                                     new
                                     {
                                         pId = data.GetInteger("id"),
                                         pName = data.GetString("name"),
                                         pPath = data.GetString("path"),
                                         pIcon = data["icon"] ?? string.Empty,
                                         pRoot = data.GetString("root"),
                                         pDataBaseId = data.GetInteger("dataBaseId"),
                                         pStatus = data.GetBoolean("status"),
                                         pSourcePrime = data.GetBoolean("sourcePrime"),
                                     })
                                     .Procedure<int>()
                                     .FirstOrDefault();
        }
        public Tuple<dynamic,dynamic> Get(Dictionary<string, object> data)
        {
            var resp =  new Execute(db, "DB_Menu_Get",
                                    new {
                                        pId = data.GetInteger("id"),
                                        pdatabaseId = data.GetInteger("databaseId")
                                    })
                                    .Procedure<dynamic, dynamic>();

            return Tuple.Create<dynamic, dynamic>(resp.Item1, resp.Item2);
        }

        public List<dynamic> GetAccess(Dictionary<string, object> data)
        {
            var resp=  new Execute(db, "DB_MenusByDatabase_Get",
                                    new { 
                                            pUserId = data.GetInteger("id") 
                                    })
                                    .Procedure<string>()
                                    .FirstOrDefault();
            var items = resp.GetJson();
            return items;
        }

        public void SaveAccess(Dictionary<string, object> data)
        {


            _ = new Execute(db, "DB_MenuByPermissions_Save",
                                    new
                                    {
                                        pId = data.GetInteger("id"),
                                        pMenu = data.GetInteger("menu"),
                                        pRole = data.GetInteger("role"),
                                        pL = data.GetBoolean("l"),
                                        pW = data.GetBoolean("w"),
                                        pD = data.GetBoolean("d"),
                                        pV = data.GetBoolean("v"),
                                        pR = data.GetBoolean("r"),
                                        pStatus = data.GetBoolean("status"),
                                    })
                                    .Procedure<dynamic>();
        }

        public dynamic GetRoot(Dictionary<string, object> data)
        {
            return new Execute(db, "DB_RootMenu_Get",
                                    new
                                    {
                                        pdatabaseId = data.GetInteger("databaseId")
                                    })
                                    .Procedure<dynamic>()
                                    .ToList();
        }

        public dynamic GetItemMenu(Dictionary<string, object> data)
        {
            var resp = new Execute(db, "DB_Menu_Item_Get",
                                    new
                                    {
                                        pMenuId = data.GetInteger("id"),
                                        pUserId = data.GetInteger("userid"),
                                    })
                                    .Procedure<dynamic>()
                                    .FirstOrDefault();
            if (resp == null)
                return resp;

            var Cnn = (string)resp.ConnectionString;
            resp.Token = Cnn.EncryptData();
            return resp;
        }

    }
}
