using Common.Shared;
using System.Collections.Generic;

namespace ARDZ.Core
{
    public interface IProduct
    {


        dynamic List(PaginationModel pag, Dictionary<string, object> data);
        dynamic InventoryGroupList();
        dynamic GetExistence(Dictionary<string, object> data);
        dynamic GetDetails(Dictionary<string, object> data);
        dynamic GetSettings();
        int Save( Dictionary<string,object> data);
    }
}
