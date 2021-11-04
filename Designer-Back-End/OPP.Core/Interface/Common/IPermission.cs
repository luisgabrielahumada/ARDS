using System.Collections.Generic;

namespace ARDZ.Core
{
    public interface IPermission
    {
        dynamic List(int isProcess);
        dynamic Get(int id);
        void Delete(Dictionary<string, object> data);
        void Save(Dictionary<string,object> data);
    }
}
