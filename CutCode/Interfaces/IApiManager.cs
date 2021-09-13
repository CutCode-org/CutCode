using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutCode
{
    public interface IApiManager
    {
        Task<ShareAddResponseModel> ShareAddRequest(ShareAddRequestModel request);
        Task<ShareGetResponseModel> ShareGetRequest(ShareGetRequestModel request);
        bool IsInternetAvailable();
    }
}
