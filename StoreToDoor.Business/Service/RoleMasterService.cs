using StoreToDoor.Business.Contract;
using StoreToDoor.DAL.Contract;
using StoreToDoor.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoreToDoor.Business.Service
{
    public class RoleMasterService : IRoleMasterService
    {
        private readonly IRoleMasterRepository _repo;
        public RoleMasterService(IRoleMasterRepository repo)
        {
            _repo = repo;
        }
        public async Task<ApiGenericResponseModel<bool>> CreateUserType(RoleMaster data)
        {
            ApiGenericResponseModel<bool> response = new ApiGenericResponseModel<bool>();
            response.Result = true;
            try
            {
                response = await _repo.CreateUserType(data);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage.Add(ex.Message);
            }
            return response;
        }

        public async Task<ApiGenericResponseModel<bool>> DeleteUserType(int Id)
        {
            ApiGenericResponseModel<bool> response = new ApiGenericResponseModel<bool>();
            response.Result = true;
            try
            {
                var userRole = await _repo.GetUserRoleById(Id);
                if (userRole != null && userRole.IsSuccess && userRole.Result != null && userRole.Result.RoleId > 0)
                {
                    response.IsSuccess = true;
                    response.Result = false;
                    response.ErrorMessage.Add("UserRole Already Assigned!");
                }
                else
                {
                    response = await _repo.DeleteUserType(Id);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage.Add(ex.Message);
            }
            return response;
        }

        public async Task<ApiGenericResponseModel<RoleMaster>> GetUserTypeById(int Id)
        {
            return await _repo.GetUserTypeById(Id);
        }

        public async Task<ApiGenericResponseModel<List<RoleMaster>>> GetUserTypeList()
        {
            ApiGenericResponseModel<List<RoleMaster>> response = new ApiGenericResponseModel<List<RoleMaster>>();
            response.Result = new List<RoleMaster>();
            try
            {
                response = await _repo.GetUserTypeList();
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage.Add(ex.Message);
            }
            return response;
        }

        public async Task<ApiGenericResponseModel<bool>> UpdateUserType(RoleMaster data)
        {
            ApiGenericResponseModel<bool> response = new ApiGenericResponseModel<bool>();
            response.Result = true;
            try
            {
                response = await _repo.UpdateUserType(data);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage.Add(ex.Message);
            }
            return response;
        }
    }
}
