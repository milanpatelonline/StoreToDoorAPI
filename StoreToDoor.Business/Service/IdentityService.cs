using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using StoreToDoor.Business.Contract;
using StoreToDoor.Model;
using StoreToDoor.Model.Common;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;
using StoreToDoor.DAL;
using Microsoft.Extensions.Configuration;
using StoreToDoor.DAL.Contract;
using StoreToDoor.DAL.Repositories;

namespace StoreToDoor.Business.Service
{
    public class IdentityService : IIdentityService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IUserMasterRepository _userMasterRepository;
        private readonly ServiceConfiguration _appSettings;
        private readonly IConfiguration _iconfiguration;
        private readonly TokenValidationParameters _tokenValidationParameters;
        public IdentityService(ILoginRepository loginRepository,
            IOptions<ServiceConfiguration> settings,
            TokenValidationParameters tokenValidationParameters, IConfiguration iconfiguration, IUserMasterRepository userMasterRepository)
        {
            _loginRepository = loginRepository;
            _appSettings = settings.Value;
            _tokenValidationParameters = tokenValidationParameters;
            _iconfiguration = iconfiguration;
            _userMasterRepository = userMasterRepository;
        }
        public async Task<ResponseModel<TokenModel>> LoginAsync(LoginModel login)
        {
            ResponseModel<TokenModel> response = new ResponseModel<TokenModel>();
            try
            {
                string md5Password = login.Password;//encript 
                UserMaster loginUser = new UserMaster();

                var loginResult = await _loginRepository.GetLoginUser(login);
                if (loginResult != null && loginResult.Result != null && loginResult.IsSuccess)
                {
                    loginUser = loginResult.Result;
                }

                if (loginUser == null || loginUser.UserId < 1)
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid Username And Password";
                    return response;
                }

                AuthenticationResult authenticationResult = await AuthenticateAsync(loginUser);
                if (authenticationResult != null && authenticationResult.Success)
                {
                    response.Data = new TokenModel() { Token = authenticationResult.Token,
                        RefreshToken = authenticationResult.RefreshToken,
                        UserProfile = loginResult.Result
                    };
                }
                else
                {
                    response.Message = "Something went wrong!";
                    response.IsSuccess = false;
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<AuthenticationResult> AuthenticateAsync(UserMaster user)
        {
            // authentication successful so generate jwt token  
            AuthenticationResult authenticationResult = new AuthenticationResult();
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var key = Encoding.ASCII.GetBytes(_appSettings.JwtSettings.Secret);

                ClaimsIdentity Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim("Name", user.Name),
                    new Claim("EmailAddress",user.EmailAddress==null?"":user.EmailAddress),
                    new Claim("UserName",user.UserName==null?"":user.UserName),
                    new Claim("UserRole",user.UserRole==null?"":user.UserRole),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    });

                foreach (var item in await GetUserRole(user.UserId))
                {
                    Subject.AddClaim(new Claim(ClaimTypes.Role, item.RoleName));
                }

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = Subject,
                    Expires = DateTime.UtcNow.Add(_appSettings.JwtSettings.TokenLifetime),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                authenticationResult.Token = tokenHandler.WriteToken(token);
                var refreshToken = new RefreshToken
                {
                    Token = Guid.NewGuid().ToString(),
                    JwtId = token.Id,
                    UserId = user.UserId,
                    CreatedOn = DateTime.UtcNow,
                    ExpiryDate = DateTime.UtcNow.AddMonths(6)
                };

                await _loginRepository.InsertRefreshToken(refreshToken);
                authenticationResult.RefreshToken = refreshToken.Token;
                authenticationResult.Success = true;
                return authenticationResult;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        private async Task<List<UserRole>> GetUserRole(int UserId)
        {
            try
            {
                List<UserRole> rolesMasters = new List<UserRole>();
                var userRole = await _loginRepository.GetUserRole(UserId);
                rolesMasters = userRole.Result;
                return rolesMasters;
            }
            catch (Exception)
            {
                return new List<UserRole>();
            }
        }

        public async Task<ResponseModel<TokenModel>> RefreshTokenAsync(TokenModel request)
        {
            ResponseModel<TokenModel> response = new ResponseModel<TokenModel>();
            try
            {
                var authResponse = await GetRefreshTokenAsync(request.Token, request.RefreshToken);
                if (!authResponse.Success)
                {

                    response.IsSuccess = false;
                    response.Message = string.Join(",", authResponse.Errors);
                    return response;
                }
                TokenModel refreshTokenModel = new TokenModel();
                refreshTokenModel.Token = authResponse.Token;
                refreshTokenModel.RefreshToken = authResponse.RefreshToken;
                response.Data = refreshTokenModel;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Something went wrong!";
                return response;
            }
        }

        private async Task<AuthenticationResult> GetRefreshTokenAsync(string token, string refreshToken)
        {
            var validatedToken = GetPrincipalFromToken(token);

            if (validatedToken == null)
            {
                return new AuthenticationResult { Errors = new[] { "Invalid Token" } };
            }

            var expiryDateUnix =
                long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);

            if (expiryDateTimeUtc > DateTime.UtcNow)
            {
                return new AuthenticationResult { Errors = new[] { "This token hasn't expired yet" } };
            }

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            var storedRefreshToken = await _loginRepository.GetRefreshToken(refreshToken);

            if (storedRefreshToken == null && storedRefreshToken.Result == null)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token does not exist" } };
            }

            if (DateTime.UtcNow > storedRefreshToken.Result.ExpiryDate)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token has expired" } };
            }

            if (storedRefreshToken.Result.Used == true)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token has been used" } };
            }

            if (storedRefreshToken.Result.JwtId != jti)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token does not match this JWT" } };
            }

            storedRefreshToken.Result.Used = true;
            await _loginRepository.UpdateRefreshToken(storedRefreshToken.Result);
            string strUserId = validatedToken.Claims.Single(x => x.Type == "UserId").Value;
            long userId = 0;
            long.TryParse(strUserId, out userId);
            var userResult = await _userMasterRepository.GetById((int)userId);
            var user = new UserMaster();
            if (userResult == null && userResult.Result != null && userResult.IsSuccess)
            {
                user = userResult.Result;
            }
            //_context.UsersMaster.FirstOrDefault(c => c.UserId == userId);
            if (user == null || user.UserId < 1)
            {
                return new AuthenticationResult { Errors = new[] { "User Not Found" } };
            }

            return await AuthenticateAsync(user);
        }
        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var tokenValidationParameters = _tokenValidationParameters.Clone();
                tokenValidationParameters.ValidateLifetime = false;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }

                return principal;
            }
            catch
            {
                return null;
            }
        }
        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                   jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                       StringComparison.InvariantCultureIgnoreCase);
        }

    }
}
