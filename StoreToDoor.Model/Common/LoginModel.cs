using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreToDoor.Model.Common
{
    public class LoginModel
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
