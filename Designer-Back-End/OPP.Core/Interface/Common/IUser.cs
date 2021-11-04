using System;
using System.Collections.Generic;

namespace ARDZ.Core
{
    public interface IUser
    {
        dynamic List(PaginationModel pag, int DataBaseId, int All);
        Tuple<dynamic, dynamic> Get(Dictionary<string, object> data);
        void Delete(Dictionary<string, object> data);
        int Save(Dictionary<string,object> data);
        void SaveExistsUsers(Dictionary<string, object> data);
        Tuple<dynamic, dynamic> List(int DatabaseId, int All);
    }
}
