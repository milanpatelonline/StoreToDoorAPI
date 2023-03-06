using System;
using System.Net.NetworkInformation;

namespace StoreToDoor.DAL
{
    public static class SqlConstants
    {

        //Store Procedures
        public const string SP_InsertUserMaster = "SP_InsertUserMaster";
        public const string SP_UpdateUserMaster = "SP_UpdateUserMaster";
        public const string SP_InserRefreshToken = "SP_InserRefreshToken";
        public const string SP_UpdateRefreshToken = "SP_InserRefreshToken";
        public const string SP_GetLoginUser = "SP_GetLoginUser";
        public const string SP_InsertRoleMaster = "SP_InsertRoleMaster";
        public const string SP_UpdateRoleMaster = "SP_UpdateRoleMaster";
        public const string SP_GetUsers = "SP_GetUsers";

        //Tabels
        public const string UserMaster = "UserMaster";
        public const string RoleMaster = "RoleMaster";
        public const string RefreshToken = "RefreshToken";
        public const string UserRole = "UserRole";
    }
}
