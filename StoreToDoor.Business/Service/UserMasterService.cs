using StoreToDoor.Business.Contract;
using StoreToDoor.DAL.Contract;
using StoreToDoor.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoreToDoor.Business.Service
{
    public class UserMasterService : IUserMasterService
    {
        private readonly IUserMasterRepository _repo;
        public UserMasterService(IUserMasterRepository repo)
        {
            _repo = repo;
        }
        public async Task<ApiGenericResponseModel<bool>> Create(UserMaster data)
        {
            return await _repo.Create(data);
        }

        public async Task<ApiGenericResponseModel<bool>> Delete(int Id)
        {
            return await _repo.Delete(Id);
        }
        public async Task<ApiGenericResponseModel<bool>> Active(int Id)
        {
            return await _repo.Active(Id);
        }

        public async Task<ApiGenericResponseModel<UserMaster>> GetById(int Id)
        {
            return await _repo.GetById(Id);
        }

        public async Task<ApiGenericResponseModel<List<UserMaster>>> GetUsers()
        {
            return await _repo.GetUsers();
        }

        public async Task<ApiGenericResponseModel<bool>> Update(UserMaster data)
        {
            return await _repo.Update(data);
        }
    }
}
