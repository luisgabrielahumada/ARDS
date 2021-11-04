using Common.Shared;
using System.Collections.Generic;

namespace ARDZ.Core
{
    public interface ITruck
    {
        dynamic List(Context context ,PaginationModel pag);
        dynamic Get(Dictionary<string, object> data);
        void Delete(Dictionary<string, object> data);
        int Save(Dictionary<string,object> data);
    }
}
