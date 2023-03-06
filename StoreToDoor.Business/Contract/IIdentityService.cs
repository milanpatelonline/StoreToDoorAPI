using Newtonsoft.Json.Linq;
using StoreToDoor.Model;
using StoreToDoor.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoreToDoor.Business.Contract
{
    public interface IIdentityService
    {
        Task<ResponseModel<TokenModel>> LoginAsync(LoginModel login);
        Task<ResponseModel<TokenModel>> RefreshTokenAsync(TokenModel request);        
    }
}
