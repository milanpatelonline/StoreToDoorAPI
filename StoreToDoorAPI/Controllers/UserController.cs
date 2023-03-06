using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreToDoor.Business.Contract;
using StoreToDoor.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StoreToDoorAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserMasterService _UserMasterService;
        private readonly IIdentityService _identityService;
        public UserController(IUserMasterService UserMasterService, IIdentityService identityService)
        {
            _UserMasterService = UserMasterService;
            _identityService= identityService;
        }

        [HttpGet]
        [Route("GetUsersList")]
        public async Task<ApiGenericResponseModel<List<UserMaster>>> GetUsers()
        {
            ApiGenericResponseModel<List<UserMaster>> response = new ApiGenericResponseModel<List<UserMaster>>();
            try
            {
                response = await _UserMasterService.GetUsers();
            }
            catch (Exception ex)
            {
                response.ErrorMessage.Add(ex.Message);
            }
            return response;

        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<ApiGenericResponseModel<bool>> CreateUser(UserMaster data)
        {
            ApiGenericResponseModel<bool> response = new ApiGenericResponseModel<bool>();
            try
            {
                if ("admin" == GetUserRoleFromToken().ToLower())
                {
                    response = await _UserMasterService.Create(data);
                }
                else
                    response.ErrorMessage.Add("Only Admin can access");
                
            }
            catch (Exception ex)
            {
                response.ErrorMessage.Add(ex.Message);
            }
            return response;
        }

        [HttpGet]
        [Route("GetUserById")]
        public async Task<ApiGenericResponseModel<UserMaster>> GetUserById(int Id)
        {
            ApiGenericResponseModel<UserMaster> response = new ApiGenericResponseModel<UserMaster>();
            try
            {
                response = await _UserMasterService.GetById(Id);
            }
            catch (Exception ex)
            {
                response.ErrorMessage.Add(ex.Message);
            }
            return response;

        }
        
        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<ApiGenericResponseModel<bool>> DeleteUser(int Id)
        {
            ApiGenericResponseModel<bool> response = new ApiGenericResponseModel<bool>();
            try
            {
                if ("admin" == GetUserRoleFromToken().ToLower())
                {
                    response = await _UserMasterService.Delete(Id);
                }
                else
                    response.ErrorMessage.Add("Only Admin can access");
            }
            catch (Exception ex)
            {
                response.ErrorMessage.Add(ex.Message);
            }
            return response;

        }

        [HttpDelete]
        [Route("ActiveUser")]
        public async Task<ApiGenericResponseModel<bool>> ActiveUser(int Id)
        {
            ApiGenericResponseModel<bool> response = new ApiGenericResponseModel<bool>();
            try
            {
                if ("admin" == GetUserRoleFromToken().ToLower())
                {
                    response = await _UserMasterService.Active(Id);
                }
                else
                    response.ErrorMessage.Add("Only Admin can access");
            }
            catch (Exception ex)
            {
                response.ErrorMessage.Add(ex.Message);
            }
            return response;

        }

        [HttpPost]
        [Route("UpdateUser")]
        public async Task<ApiGenericResponseModel<bool>> UpdateUser(UserMaster data)
        {
            ApiGenericResponseModel<bool> response = new ApiGenericResponseModel<bool>();
            try
            {
                if ("admin" == GetUserRoleFromToken().ToLower())
                {
                    response = await _UserMasterService.Update(data);
                }
                else
                    response.ErrorMessage.Add("Only Admin can access");
            }
            catch (Exception ex)
            {
                response.ErrorMessage.Add(ex.Message);
            }
            return response;
        }

        [Route("getCurrentUser")]
        [HttpGet]
        public async Task<ApiGenericResponseModel<UserMaster>> GetCurrentUser()
        {
            long UserId = GetUserIdFromToken();
            return await _UserMasterService.GetById((int)UserId);
        }
        protected long GetUserIdFromToken()
        {
            long UserId = 0;
            try
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var identity = HttpContext.User.Identity as ClaimsIdentity;
                    if (identity != null)
                    {
                        IEnumerable<Claim> claims = identity.Claims;
                        string strUserId = identity.FindFirst("UserId").Value;
                        long.TryParse(strUserId, out UserId);

                    }
                }
                return UserId;
            }
            catch
            {
                return UserId;
            }
        }
        protected string GetUserRoleFromToken()
        {
            string UserRole = string.Empty;
            try
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var identity = HttpContext.User.Identity as ClaimsIdentity;
                    if (identity != null)
                    {
                        IEnumerable<Claim> claims = identity.Claims;
                        UserRole = identity.FindFirst("UserRole").Value;
                    }
                }
                return UserRole;
            }
            catch
            {
                return UserRole;
            }
        }
    }
}
