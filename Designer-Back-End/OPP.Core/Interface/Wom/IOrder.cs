using Common.Shared;
using System.Collections.Generic;

namespace ARDZ.Core
{
    public interface IOrder
    {
        dynamic List(PaginationModel pag);
        dynamic Get( Dictionary<string, object> data);
        dynamic Delete(int id);
        dynamic SaveSerialPhoneMassive(string json);
        //Dictionary<string, object> Import(Dictionary<string, object> req);
        dynamic SaveSerialPhone(Dictionary<string, object> req);
        dynamic UploadSku(Dictionary<string, object> data);
        dynamic ApprovedSku(Dictionary<string, object> data);
        dynamic GetGeoreferencing();
        dynamic DownloadedGeoreferencing();


    }
}
