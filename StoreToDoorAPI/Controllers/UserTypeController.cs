using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreToDoor.Business.Contract;
using StoreToDoor.Model;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

namespace StoreToDoorAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypeController : ControllerBase
    {
        private readonly IRoleMasterService _roleMasterService;
        private readonly IIdentityService _identityService;
        public UserTypeController(IRoleMasterService roleMasterService, IIdentityService identityService)
        {
            _roleMasterService = roleMasterService;
            _identityService = identityService;
        }

        [HttpGet]
        [Route("GetUserTypeList")]
        public async Task<ApiGenericResponseModel<List<RoleMaster>>> GetUserTypeList()
        {
            ApiGenericResponseModel<List<RoleMaster>> response = new ApiGenericResponseModel<List<RoleMaster>>();
            try
            {
                response = await _roleMasterService.GetUserTypeList();
            }
            catch (Exception ex)
            {
                response.ErrorMessage.Add(ex.Message);
            }
            return response;

        }

        [HttpPost]
        [Route("CreateUserType")]
        public async Task<ApiGenericResponseModel<bool>> CreateUserType(RoleMaster data)
        {
            ApiGenericResponseModel<bool> response = new ApiGenericResponseModel<bool>();
            try
            {
                response = await _roleMasterService.CreateUserType(data);
                //if ("admin" == GetUserRoleFromToken().ToLower())
                //{
                //    response = await _roleMasterService.CreateUserType(data);
                //}
                //else
                //    response.ErrorMessage.Add("Only Admin can access");

            }
            catch (Exception ex)
            {
                response.ErrorMessage.Add(ex.Message);
            }
            return response;
        }

        [HttpDelete]
        [Route("DeleteUserType")]
        public async Task<ApiGenericResponseModel<bool>> DeleteUserType(int Id)
        {
            ApiGenericResponseModel<bool> response = new ApiGenericResponseModel<bool>();
            try
            {
                if ("admin" == GetUserRoleFromToken().ToLower())
                {
                    response = await _roleMasterService.DeleteUserType(Id);
                }
                else
                {
                    response.Result = false;
                    response.IsSuccess = true;
                    response.ErrorMessage.Add("Only Admin can access");
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage.Add(ex.Message);
            }
            return response;

        }

        [HttpPost]
        [Route("UpdateUserType")]
        public async Task<ApiGenericResponseModel<bool>> UpdateUserType(RoleMaster data)
        {
            ApiGenericResponseModel<bool> response = new ApiGenericResponseModel<bool>();
            try
            {
                response = await _roleMasterService.UpdateUserType(data);
                //if ("admin" == GetUserRoleFromToken().ToLower())
                //{
                //    response = await _roleMasterService.UpdateUserType(data);
                //}
                //else
                //    response.ErrorMessage.Add("Only Admin can access");
            }
            catch (Exception ex)
            {
                response.ErrorMessage.Add(ex.Message);
            }
            return response;
        }

        [HttpGet]
        [Route("GetUserTypeById")]
        public async Task<ApiGenericResponseModel<RoleMaster>> GetUserTypeById(int Id)
        {
            ApiGenericResponseModel<RoleMaster> response = new ApiGenericResponseModel<RoleMaster>();
            try
            {
                response = await _roleMasterService.GetUserTypeById(Id);
            }
            catch (Exception ex)
            {
                response.ErrorMessage.Add(ex.Message);
            }
            return response;

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
