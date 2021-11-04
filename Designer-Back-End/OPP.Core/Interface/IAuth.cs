using System.Collections.Generic;

namespace ARDZ.Core
{
    public interface IAuth
    {
        Dictionary<string, object> Login(AuthModel auth);
        //void Register(Dictionary<string, object> data);
        //dynamic GetProfile(Dictionary<string, object> data);
        //void RecoverPassword(Dictionary<string, object> data);
    }
}
