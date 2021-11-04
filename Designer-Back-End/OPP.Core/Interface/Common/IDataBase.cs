using System.Collections.Generic;

namespace ARDZ.Core
{
    public interface IDataBase
    {
        dynamic List(PaginationModel pag);
        dynamic Get(Dictionary<string, object> data);
        void Delete(Dictionary<string, object> data);
        int Save(Dictionary<string,object> data);
    }
}
