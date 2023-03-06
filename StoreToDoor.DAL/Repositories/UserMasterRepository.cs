using Dapper;
using StoreToDoor.DAL.Contract;
using StoreToDoor.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace StoreToDoor.DAL.Repositories
{
    public class UserMasterRepository : IUserMasterRepository
    {
        private readonly IDapper _dapper;
        public UserMasterRepository(IDapper dapper)
        {
            _dapper = dapper;
        }
        public async Task<ApiGenericResponseModel<bool>> Create(UserMaster data)
        {
            ApiGenericResponseModel<bool> response = new Model.ApiGenericResponseModel<bool>();
            response.Result = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@Name", data.Name, DbType.String);
                dbparams.Add("@Address", data.Address, DbType.String);
                dbparams.Add("@Phone", data.Phone, DbType.String);
                dbparams.Add("@Designation", data.Designation, DbType.String);
                dbparams.Add("@EmailAddress", data.EmailAddress, DbType.String);
                dbparams.Add("@UserName", data.UserName, DbType.String);
                dbparams.Add("@Password", data.Password, DbType.String);
                dbparams.Add("@IsActive", true, DbType.Boolean);
                dbparams.Add("@RoleId", data.RoleId, DbType.Int32);
                var result = await Task.FromResult(_dapper.Insert<bool>(SqlConstants.SP_InsertUserMaster, dbparams, commandType: CommandType.StoredProcedure));
                response.Result = true;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UC_UserName"))
                {
                    response.ErrorMessage.Add("User Name Already Exist!");
                }
                else { response.ErrorMessage.Add(ex.Message); }
                response.IsSuccess = false;

            }
            return response;
        }

        public async Task<ApiGenericResponseModel<UserMaster>> GetById(int Id)
        {
            ApiGenericResponseModel<UserMaster> response = new Model.ApiGenericResponseModel<UserMaster>();
            response.Result = new UserMaster();
            try
            {
                response.Result = await Task.FromResult(_dapper.Get<UserMaster>
                    ($"Select u.UserId,u.Name,u.Address,u.Phone,u.designation,u.EmailAddress,u.UserName,u.IsActive,u.CreatedOn,u.UpdateOn,r.RoleName as UserRole,ur.RoleId " +
                    $"from {SqlConstants.UserMaster} u left join {SqlConstants.UserRole} ur on u.UserId=ur.UserId left join {SqlConstants.RoleMaster} r " +
                    $"on ur.RoleId=r.RoleId where u.UserId = {Id}", null, commandType: CommandType.Text));
                response.IsSuccess = true;

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage.Add(ex.Message);
            }
            return response;
        }

        public async Task<ApiGenericResponseModel<List<UserMaster>>> GetUsers()
        {
            ApiGenericResponseModel<List<UserMaster>> response = new Model.ApiGenericResponseModel<List<UserMaster>>();
            response.Result = new List<UserMaster>();
            try
            {
                response.Result = await Task.FromResult(_dapper.GetAll<UserMaster>(SqlConstants.SP_GetUsers, null, commandType: CommandType.StoredProcedure));
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage.Add(ex.Message);
            }
            return response;
        }

        public async Task<ApiGenericResponseModel<bool>> Delete(int Id)
        {
            ApiGenericResponseModel<bool> response = new Model.ApiGenericResponseModel<bool>();
            response.Result = true;
            try
            {
                response.Result = await Task.FromResult(_dapper.Execute<bool>($"update {SqlConstants.UserMaster} set IsActive = 0 Where UserId = {Id}", null, commandType: CommandType.Text));
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage.Add(ex.Message);
            }
            return response;
        }

        public async Task<ApiGenericResponseModel<bool>> Active(int Id)
        {
            ApiGenericResponseModel<bool> response = new Model.ApiGenericResponseModel<bool>();
            response.Result = true;
            try
            {
                response.Result = await Task.FromResult(_dapper.Execute<bool>($"update {SqlConstants.UserMaster} set IsActive = 1 Where UserId = {Id}", null, commandType: CommandType.Text));
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage.Add(ex.Message);
            }
            return response;
        }

        public async Task<ApiGenericResponseModel<bool>> Update(UserMaster data)
        {
            ApiGenericResponseModel<bool> response = new Model.ApiGenericResponseModel<bool>();
            response.Result = false;
            try
            {
                var dbPara = new DynamicParameters();
                dbPara.Add("@UserId", data.UserId);
                dbPara.Add("@Name", data.Name, DbType.String);
                dbPara.Add("@Address", data.Address, DbType.String);
                dbPara.Add("@Phone", data.Phone, DbType.String);
                dbPara.Add("@Designation", data.Designation, DbType.String);
                dbPara.Add("@EmailAddress", data.EmailAddress, DbType.String);
                dbPara.Add("@UserName", data.UserName, DbType.String);
                dbPara.Add("@Password", data.Password, DbType.String);
                dbPara.Add("@IsActive", data.IsActive, DbType.Boolean);
                dbPara.Add("@RoleId", data.RoleId, DbType.Int32);
                response.Result = await Task.FromResult(_dapper.Update<bool>(SqlConstants.SP_UpdateUserMaster,
                   dbPara, commandType: CommandType.StoredProcedure));
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
