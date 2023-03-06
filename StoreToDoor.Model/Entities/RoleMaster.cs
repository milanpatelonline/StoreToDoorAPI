using System;
using System.Collections.Generic;
using System.Text;

namespace StoreToDoor.Model
{
    public class RoleMaster
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
