using System;
using System.Collections.Generic;
using System.Text;

namespace StoreToDoor.Model
{
    public class UserMaster
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Designation { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
    }
}
