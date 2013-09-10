using NidTid.Domain.Entities;
using System;
using System.Web.Security;


namespace NidTid.WebUI.Security
{
    public class CustomMembershipUser : MembershipUser
    {
        #region Properties

        public string FirstName { get; set; }

        public int UserId { get; set; }

        public string UserRoleName { get; set; }

        #endregion

        public CustomMembershipUser(User user)
            : base("CustomMembershipProvider", user.Email, user.Id, user.Email, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
        {
            FirstName = user.Name;
            UserId = user.Id;
            UserRoleName = user.Role;
        }
    }
}