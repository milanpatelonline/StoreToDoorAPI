using StoreToDoor.Model;
using StoreToDoor.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoreToDoor.DAL.Contract
{
    public interface ILoginRepository
    {
        Task<ApiGenericResponseModel<UserMaster>> GetLoginUser(LoginModel loginModel);
        Task<ApiGenericResponseModel<List<UserRole>>> GetUserRole(int Id);
        Task<ApiGenericResponseModel<RoleMaster>> GetRoleMaster();
        Task<ApiGenericResponseModel<bool>> InsertRefreshToken(RefreshToken data);
        Task<ApiGenericResponseModel<RefreshToken>> GetRefreshToken(string token);
        Task<ApiGenericResponseModel<bool>> UpdateRefreshToken(RefreshToken data);
    }
}
