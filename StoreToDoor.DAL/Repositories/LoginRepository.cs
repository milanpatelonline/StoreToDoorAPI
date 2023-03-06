using Dapper;
using StoreToDoor.DAL.Contract;
using StoreToDoor.Model;
using StoreToDoor.Model.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace StoreToDoor.DAL.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IDapper _dapper;
        public LoginRepository(IDapper dapper)
        {
            _dapper = dapper;
        }

        public async Task<ApiGenericResponseModel<UserMaster>> GetLoginUser(LoginModel loginModel)
        {
            ApiGenericResponseModel<UserMaster> response = new Model.ApiGenericResponseModel<UserMaster>();
            response.Result = new UserMaster();
            try
            {
                response.IsSuccess = true;
                var dbparams = new DynamicParameters();                
                dbparams.Add("@UserName", loginModel.UserName, DbType.String);
                dbparams.Add("@Password", loginModel.Password, DbType.String);
                response.Result = await Task.FromResult(_dapper.Execute<UserMaster>(SqlConstants.SP_GetLoginUser, dbparams, commandType: CommandType.StoredProcedure));
            }
            catch (Exception ex)
            {
                response.IsSuccess=false;
                response.ErrorMessage.Add(ex.Message);
            }
            return response;
        }
        public async Task<ApiGenericResponseModel<List<UserRole>>> GetUserRole(int Id)
        {
            ApiGenericResponseModel<List<UserRole>> response = new Model.ApiGenericResponseModel<List<UserRole>>();
            response.Result = new List<UserRole>();
            try
            {
                response.IsSuccess = true;
                response.Result = await Task.FromResult(_dapper.Get<List<UserRole>>
                    ($"Select u.RoleId, RoleName,UserId from {SqlConstants.RoleMaster} r inner join {SqlConstants.UserRole} u on r.RoleId=u.RoleId where UserId={Id}", null, commandType: CommandType.Text));

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage.Add(ex.Message);
            }
            return response;
        }
        public async Task<ApiGenericResponseModel<RoleMaster>> GetRoleMaster()
        {
            ApiGenericResponseModel<RoleMaster> response = new Model.ApiGenericResponseModel<RoleMaster>();
            response.Result = new RoleMaster();
            try
            {
                response.IsSuccess = true;
                response.Result = await Task.FromResult(_dapper.Get<RoleMaster>
                    ($"Select * from {SqlConstants.RoleMaster } ", null, commandType: CommandType.Text));

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage.Add(ex.Message);
            }
            return response;
        }
        public async Task<ApiGenericResponseModel<bool>> InsertRefreshToken(RefreshToken data)
        {
            ApiGenericResponseModel<bool> response = new Model.ApiGenericResponseModel<bool>();
            response.Result = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@Token", data.Token, DbType.String);
                dbparams.Add("@JwtId", data.JwtId, DbType.String);
                dbparams.Add("@UserId", data.UserId, DbType.Int16);
                dbparams.Add("@CreatedOn", data.CreatedOn, DbType.DateTime);
                dbparams.Add("@ExpiryDate", data.ExpiryDate, DbType.DateTime);
                dbparams.Add("@Used", data.Used, DbType.Boolean);
                var result = await Task.FromResult(_dapper.Insert<bool>(SqlConstants.SP_InserRefreshToken, dbparams, commandType: CommandType.StoredProcedure));
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage.Add(ex.Message);
            }
            return response;
        }
        public async Task<ApiGenericResponseModel<RefreshToken>> GetRefreshToken(string token)
        {
            ApiGenericResponseModel<RefreshToken> response = new ApiGenericResponseModel<RefreshToken>();
            response.Result = new RefreshToken();
            try
            {
                response.IsSuccess = true;
                response.Result = await Task.FromResult(_dapper.Get<RefreshToken>
                    ($"Select * from {SqlConstants.RefreshToken} where token='{token}'", null, commandType: CommandType.Text));

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage.Add(ex.Message);
            }
            return response;
        }
        public async Task<ApiGenericResponseModel<bool>> UpdateRefreshToken(RefreshToken data)
        {
            ApiGenericResponseModel<bool> response = new ApiGenericResponseModel<bool>();
            response.Result = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@RefreshTokenId", data.RefreshTokenId);
                dbparams.Add("@Token", data.Token, DbType.String);
                dbparams.Add("@JwtId", data.JwtId, DbType.String);
                dbparams.Add("@UserId", data.UserId, DbType.Int16);
                dbparams.Add("@CreatedOn", data.CreatedOn, DbType.DateTime);
                dbparams.Add("@ExpiryDate", data.ExpiryDate, DbType.DateTime);
                dbparams.Add("@Used", data.Used, DbType.Boolean);
                response.Result = await Task.FromResult(_dapper.Update<bool>(SqlConstants.SP_UpdateRefreshToken,
                   dbparams, commandType: CommandType.StoredProcedure));
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
