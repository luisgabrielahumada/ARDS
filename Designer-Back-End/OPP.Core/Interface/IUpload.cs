using Common.Shared;
using System.Collections.Generic;

namespace ARDZ.Core
{
    public interface IUpload
    {
        void UploadFile(Context context, Dictionary<string, object> req);
        dynamic GetFiles(Context context, Dictionary<string, object> req);
        void DeleteFile(Context context, Dictionary<string, object> req);
    }
}
