using System;
using System.Collections.Generic;

namespace ARDZ.Core
{
    public interface ITemplates
    {
        dynamic List(PaginationModel pag);
        Tuple<dynamic, dynamic> Get(string id);
        void Delete(Dictionary<string, object> data);
        void Save(Dictionary<string,object> data);
    }
}
