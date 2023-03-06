using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreToDoor.Model.Common
{
    public class TokenModel
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }
        public UserMaster UserProfile { get; set; }
    }
    public class AuthenticationResult : TokenModel
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
