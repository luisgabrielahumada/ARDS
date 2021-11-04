using System;
using System.Collections.Generic;

namespace ARDZ.Core
{
    public interface IMenu
    {
        dynamic List(PaginationModel pag, int DataBaseId);
        Tuple<dynamic, dynamic> Get(Dictionary<string, object> data);
        void Delete(Dictionary<string, object> data);
        int Save(Dictionary<string,object> data);
        List<dynamic> GetAccess(Dictionary<string,object> data);
        dynamic GetRoot(Dictionary<string, object> data);
        void SaveAccess(Dictionary<string, object> data);
        dynamic GetItemMenu(Dictionary<string, object> data);
    }
}
