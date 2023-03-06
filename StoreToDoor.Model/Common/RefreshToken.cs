using System;
using System.Collections.Generic;
using System.Text;

namespace StoreToDoor.Model.Common
{
    public class RefreshToken
    {
        public int RefreshTokenId { get; set; }
        public string Token { get; set; }
        public string JwtId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool Used { get; set; }
    }
}
