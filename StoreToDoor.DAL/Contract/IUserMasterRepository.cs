using StoreToDoor.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace StoreToDoor.DAL.Contract
{
    public interface IUserMasterRepository
    {
        Task<ApiGenericResponseModel<bool>> Create(UserMaster data);
        Task<ApiGenericResponseModel<UserMaster>> GetById(int Id);
        Task<ApiGenericResponseModel<List<UserMaster>>> GetUsers();
        Task<ApiGenericResponseModel<bool>> Delete(int Id);
        Task<ApiGenericResponseModel<bool>> Active(int Id);
        Task<ApiGenericResponseModel<bool>> Update(UserMaster data);
    }
}
