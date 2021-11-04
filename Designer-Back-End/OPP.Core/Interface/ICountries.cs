using System.Collections.Generic;

namespace ARDZ.Core
{
    public interface ICountries
    {
        dynamic List(PaginationModel pag);
        dynamic Get(Dictionary<string, object> data);
        void Delete(Dictionary<string, object> data);
        void Save(Dictionary<string,object> data);
    }
}
