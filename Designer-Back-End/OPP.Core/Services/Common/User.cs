using Common.Shared;
using Core.Component.Library.PagerRecord;
using Core.Component.Library.SQL;
using Core.Component.Library.WebTools;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ARDZ.Core
{
    public class User : IUser
    {
        public AppDataBase db;
        public readonly Settings _settings;
        public User(Settings settings)
        {
            _settings = settings;
            db = new AppDataBase(_settings.ConnectionStrings.Cnn);
        }

        public void Delete(Dictionary<string, object> data)
        {
            new Execute(db, "DB_User_Delete",
                                      new
                                      {
                                          Id = data.GetInteger("id"),
                                          Status = data.GetBoolean("status")
                                      })
                                     .Procedure<dynamic>()
                                     .FirstOrDefault();
        }

        public dynamic List(PaginationModel pag, int DatabaseId, int All)
        {
            var data = new Execute(db, "DB_User_List",
                                    new
                                    {
                                        pPageIndex = pag.PageIndex,
                                        pPageSize = pag.PageSize,
                                        pDatabaseId = DatabaseId,
                                        pAll= All
                                    })
                                    .Procedure<dynamic>()
                                    .ToList();
            var totalRecords = data.Count == 0 ? 0 : (int)data.FirstOrDefault().totalRecords;
            return new WebPagerRecord<dynamic>(list: data, page: pag.PageIndex, pageSize: pag.PageSize, allItemsCount: totalRecords);
        }


        public Tuple<dynamic,dynamic> List(int DatabaseId, int All)
        {
            var data = new Execute(db, "DB_User_List",
                                    new
                                    {
                                        pPageIndex = 0,
                                        pPageSize = 0,
                                        pDatabaseId = DatabaseId,
                                        pAll = All
                                    })
                                    .Procedure<dynamic,dynamic>();

            return Tuple.Create<dynamic, dynamic>(data.Item1, data.Item2);
        }


        public Tuple<dynamic,dynamic> Get(Dictionary<string, object> data)
        {
            var resp =  new Execute(db, "DB_User_Get",
                                    new { pId = data.GetInteger("id") })
                                    .Procedure<dynamic,dynamic>();

            return Tuple.Create<dynamic,dynamic>(resp.Item1, resp.Item2);
        }

        public int Save(Dictionary<string, object> data)
        {
            var password = System.Text.Encoding.UTF8.GetBytes(data.GetString("newPassword"));
            var role = string.IsNullOrEmpty(data.GetString("role")) ? null : (JArray)data["role"];
            List<int> id = new List<int>();
            foreach(var i in role)
            {
                id.Add((int)i.First.First);
            }
            string pRole =  string.Join(",", id.Select(k=> k).ToArray());
            return new Execute(db, "DB_User_Save",
                                      new
                                      {
                                          pId = data.GetInteger("id"),
                                          pName = data.GetString("name"),
                                          pLogin = data.GetString("login"),
                                          pPassword = data.GetString("newPassword"),
                                          pEmail = data.GetString("email"),
                                          pStatus = data.GetBoolean("status"),
                                          pDocument = data.GetString("document"),
                                          pDataBaseId = data.GetInteger("dataBaseId"),
                                          pRole = pRole
                                      })
                                      .Procedure<int>()
                                      .FirstOrDefault();
        }

        public void SaveExistsUsers(Dictionary<string, object> data)
        {
            var users = string.IsNullOrEmpty(data.GetString("users")) ? null : (JArray)data["users"];
            List<int> id = new List<int>();
            foreach (var i in users)
            {
                id.Add((int)i.First.First);
            }
            string pUsers = string.Join(",", id.Select(k => k).ToArray());
            _= new Execute(db, "DB_User_Exists_Save",
                                      new
                                      {
                                          pUsers = pUsers,
                                          pDataBaseId = data.GetInteger("dataBaseId"),
                                      })
                                      .Procedure<int>();
        }

    }
}
