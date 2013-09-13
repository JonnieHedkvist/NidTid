using System;
using System.Security.Principal;
using System.Web.Security;

namespace NidTid.WebUI.Security
{

    [Serializable]
    public class CustomIdentity : IIdentity
    {

        public IIdentity Identity { get; set; }
        public string FirstName { get; set; }
        public int UserId { get; set; }
        public string UserRoleName { get; set; }

        public string Name
        {
            get { return Identity.Name; }
        }

        public string AuthenticationType
        {
            get { return Identity.AuthenticationType; }
        }

        public bool IsAuthenticated { get { return Identity.IsAuthenticated; } }

        public CustomIdentity(IIdentity identity)
        {
            Identity = identity;

            var customMembershipUser = (CustomMembershipUser)Membership.GetUser(identity.Name);
            if (customMembershipUser != null)
            {
                FirstName = customMembershipUser.FirstName;
                UserId = customMembershipUser.UserId;
                UserRoleName = customMembershipUser.UserRoleName;
            }
        }

    }
}