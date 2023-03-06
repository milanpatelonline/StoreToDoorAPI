using StoreToDoor.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoreToDoor.DAL.Contract
{
    public interface IRoleMasterRepository
    {
        Task<ApiGenericResponseModel<List<RoleMaster>>> GetUserTypeList();
        Task<ApiGenericResponseModel<bool>> CreateUserType(RoleMaster data);
        Task<ApiGenericResponseModel<bool>> DeleteUserType(int Id);
        Task<ApiGenericResponseModel<bool>> UpdateUserType(RoleMaster data);
        Task<ApiGenericResponseModel<UserRole>> GetUserRoleById(int Id);
        Task<ApiGenericResponseModel<RoleMaster>> GetUserTypeById(int Id);
    }
}
