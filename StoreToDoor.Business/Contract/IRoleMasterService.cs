using StoreToDoor.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoreToDoor.Business.Contract
{
    public interface IRoleMasterService
    {
        Task<ApiGenericResponseModel<List<RoleMaster>>> GetUserTypeList();
        Task<ApiGenericResponseModel<bool>> CreateUserType(RoleMaster data);
        Task<ApiGenericResponseModel<bool>> DeleteUserType(int Id);
        Task<ApiGenericResponseModel<bool>> UpdateUserType(RoleMaster data);
        Task<ApiGenericResponseModel<RoleMaster>> GetUserTypeById(int Id);
    }
}
