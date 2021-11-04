using System.Collections.Generic;

namespace ARDZ.Core
{
    public interface IRole
    {
        dynamic List(PaginationModel pag, int DataBaseId);
        dynamic Get(Dictionary<string, object> data);
        void Delete(Dictionary<string, object> data);
        int Save(Dictionary<string,object> data);
    }
}
