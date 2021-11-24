using Common.Shared;
using Core.Component.Library.PagerRecord;
using Core.Component.Library.SQL;
using Core.Component.Library.WebTools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ARDZ.Core
{
    public class Order : IOrder
    {
        public AppDataBase db;
        public readonly Settings _settings;
        public Order(Settings settings)
        {
            _settings = settings;
            db = new AppDataBase(_settings.ConnectionStrings.Cnn);
        }


        public dynamic List(PaginationModel pag)
        {
            var data = new ExecuteMySQL(db, "DB_Truck_List",
                                    new
                                    {
                                        pPageIndex = pag.PageIndex,
                                        pPageSize = pag.PageSize
                                    })
                                    .Procedure<dynamic>()
                                    .ToList();
            var totalRecords = data.Count == 0 ? 0 : (int)data.FirstOrDefault().totalRecords;
            return new WebPagerRecord<dynamic>(list: data, page: pag.PageIndex, pageSize: pag.PageSize, allItemsCount: totalRecords);
        }

        public dynamic Get(Dictionary<string, object> data)
        {
            var sql = @$"SELECT cod_movimiento as id,cod_linea as line,fecha_creacion as dateCreation,total_unidades as quantity,
                                bodega.nombre_bodega as name,bodega.cod_bodega as warehouse, documento_principal as nroOrder
                         FROM movimiento
                                INNER JOIN bodega   on movimiento.cod_bodega_origen = bodega.cod_bodega
                         WHERE documento_principal=@nroOrden ORDER BY 1 DESC LIMIT 10;";

            return new ExecuteMySQL(db, sql,
                              new
                              {
                                  nroOrden = data.GetString("nroOrden")
                              })
                              .Query<dynamic>()
                              .FirstOrDefault();


        }

        public dynamic Delete(int id)
        {
            return new ExecuteMySQL(db, "DB_DeleteSerialAssignmentPreRecruitment",
                                                new
                                                {
                                                    v_id = id,
                                                })
                                                .Procedure<dynamic>()
                                                .FirstOrDefault();

        }

        public dynamic SaveSerialPhoneMassive(string json)
        {
            return new ExecuteMySQL(db, "DB_SerialAssignmentPreRecruitmentMassive",
                                              new
                                              {
                                                  vJsonSerial = json
                                              })
                                              .Procedure<dynamic>()
                                              .FirstOrDefault();
        }


        public dynamic SaveSerialPhone(Dictionary<string, object> req)
        {
            return new ExecuteMySQL(db, "DB_SerialAssignmentPreRecruitment",
                                   new
                                   {
                                       v_serial = req.GetString("serial"),
                                       v_min = req.GetString("phone"),
                                       v_NroOrder = req.GetString("nroOrder"),
                                       v_status = req.GetString("status"),
                                       v_TypeLoad = req.GetString("typeLoad"),
                                       v_Id = req.GetValueOrDefault("id")
                                   })
                                   .Procedure<dynamic>()
                                   .FirstOrDefault();

        }

        public dynamic UploadSku(Dictionary<string, object> data)
        {


            return new ExecuteMySQL(db, "DB_UploadSerialAssignment",
                              new
                              {
                                  vNroOrden = data.GetString("nroOrden")
                              })
                              .Procedure<dynamic>().ToList();
        }

        public dynamic ApprovedSku(Dictionary<string, object> data)
        {
            return new ExecuteMySQL(db, "DB_ApprovedSerialAssignment",
                                         new
                                         {
                                             vNroOrden = data.GetString("nroOrden"),
                                             vSerial = data.GetString("serial"),
                                             vMin = data.GetString("phone"),
                                         })
                                         .Procedure<dynamic>().ToList();
        }
        public dynamic GetGeoreferencing()
        {
            return new ExecuteMySQL(db, "DB_GetAllGeoreferencing")
                             .Procedure<dynamic>().ToList();
        }

        public dynamic DownloadedGeoreferencing()
        {
            return new ExecuteMySQL(db, "DB_DownloadedGeoreferencing")
                             .Procedure<dynamic>().ToList();
        }
    }
}
