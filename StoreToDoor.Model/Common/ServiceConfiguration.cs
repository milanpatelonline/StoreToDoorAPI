using System;
using System.Collections.Generic;
using System.Text;

namespace StoreToDoor.Model.Common
{
    public class ServiceConfiguration
    {
        public JwtSettings JwtSettings { get; set; }
    }
    public class JwtSettings
    {
        public string Secret { get; set; }
        public TimeSpan TokenLifetime { get; set; }
    }
}
