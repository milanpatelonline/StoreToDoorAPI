using Dapper;
using StoreToDoor.DAL.Contract;
using StoreToDoor.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace StoreToDoor.DAL.Repositories
{
    public class RoleMasterRepository : IRoleMasterRepository
    {
        private readonly IDapper _dapper;
        public RoleMasterRepository(IDapper dapper)
        {
            _dapper = dapper;
        }
        public async Task<ApiGenericResponseModel<bool>> CreateUserType(RoleMaster data)
        {
            ApiGenericResponseModel<bool> response = new Model.ApiGenericResponseModel<bool>();
            response.Result = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@RoleName", data.RoleName, DbType.String);
                dbparams.Add("@CreatedOn", DateTime.Now, DbType.DateTime);
                dbparams.Add("@UpdatedOn", DateTime.Now, DbType.DateTime);
                var result = await Task.FromResult(_dapper.Insert<bool>(SqlConstants.SP_InsertRoleMaster, dbparams, commandType: CommandType.StoredProcedure));
                response.Result = true;
                response.IsSuccess = true;
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
            ApiGenericResponseModel<bool> response = new Model.ApiGenericResponseModel<bool>();
            response.Result = true;
            try
            {
                response.Result = await Task.FromResult(_dapper.Execute<bool>($"delete from {SqlConstants.RoleMaster}  Where RoleId = {Id}", null, commandType: CommandType.Text));
                response.IsSuccess = true;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.IsSuccess = false;
                response.ErrorMessage.Add(ex.Message);
            }
            return response;
        }

        public async Task<ApiGenericResponseModel<UserRole>> GetUserRoleById(int Id)
        {
            ApiGenericResponseModel<UserRole> response = new Model.ApiGenericResponseModel<UserRole>();
            response.Result = new UserRole();
            try
            {
                response.Result = await Task.FromResult(_dapper.Get<UserRole> ($"Select * from  {SqlConstants.UserRole}  where RoleId = {Id}", null, commandType: CommandType.Text));
                response.IsSuccess = true;
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
            ApiGenericResponseModel<RoleMaster> response = new ApiGenericResponseModel<RoleMaster>();
            response.Result = new RoleMaster();
            try
            {
                response.Result = await Task.FromResult(_dapper.Get<RoleMaster>($"Select * from  {SqlConstants.RoleMaster}  where RoleId = {Id}", null, commandType: CommandType.Text));
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage.Add(ex.Message);
            }
            return response;
        }

        public async Task<ApiGenericResponseModel<List<RoleMaster>>> GetUserTypeList()
        {
            ApiGenericResponseModel<List<RoleMaster>> response = new Model.ApiGenericResponseModel<List<RoleMaster>>();
            response.Result = new List<RoleMaster>();
            try
            {
                response.Result = await Task.FromResult(_dapper.GetAll<RoleMaster>($"Select * from {SqlConstants.RoleMaster} ", null, commandType: CommandType.Text));
                response.IsSuccess = true;
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
            ApiGenericResponseModel<bool> response = new Model.ApiGenericResponseModel<bool>();
            response.Result = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@RoleId", data.RoleId, DbType.String);
                dbparams.Add("@RoleName", data.RoleName, DbType.String);
                response.Result = await Task.FromResult(_dapper.Update<bool>(SqlConstants.SP_UpdateRoleMaster,
                   dbparams, commandType: CommandType.StoredProcedure));
                response.IsSuccess = true;
                response.Result = true;
                response.IsSuccess = response.Result;
            }
            catch (Exception ex)
            {
                response.ErrorMessage.Add(ex.Message);
            }
            return response;
        }
    }
}
