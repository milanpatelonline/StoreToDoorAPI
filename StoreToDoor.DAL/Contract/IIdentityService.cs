using StoreToDoor.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoreToDoor.DAL.Contract
{
    public interface IIdentityService1
    {
        Task<ResponseModel<TokenModel>> LoginAsync(LoginModel login);
    }
}
