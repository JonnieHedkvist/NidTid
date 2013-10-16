using System;
using System.Collections.Specialized;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using NidTid.Domain.Entities;
using NidTid.Domain.Concrete;
 
namespace NidTid.WebUI.Security
{
    public class CustomRoleProvider : RoleProvider
    {
        private int _cacheTimeoutInMinutes = 30;

        
        public override void Initialize(string name, NameValueCollection config)
        {
            int val;
            if (!string.IsNullOrEmpty(config["cacheTimeoutInMinutes"]) && Int32.TryParse(config["cacheTimeoutInMinutes"], out val))
                _cacheTimeoutInMinutes = val;
 
            base.Initialize(name, config);
        }
 
        public override bool IsUserInRole(string username, string roleName)
        {
            var userRoles = GetRolesForUser(username);
            return userRoles.Contains(roleName);
        }

        public override string[] GetRolesForUser(string username)
        {

            if (!HttpContext.Current.User.Identity.IsAuthenticated) 
                return null;

            var cacheKey = string.Format("UserRoles_{0}", username);
            if (HttpRuntime.Cache[cacheKey] != null)
                return (string[])HttpRuntime.Cache[cacheKey];

            var userRoles = new string[] { };
            using (var context = new EFDbContext())
            {
                var user = (from u in context.Users
                            where String.Compare(u.Email, username, StringComparison.OrdinalIgnoreCase) == 0
                            select u).FirstOrDefault();
 
                if (user != null)
                    userRoles = new[]{user.Role};
            }
 
            HttpRuntime.Cache.Insert(cacheKey, userRoles, null, DateTime.Now.AddMinutes(_cacheTimeoutInMinutes), Cache.NoSlidingExpiration);
 
            return userRoles.ToArray();
        }      

 
        #region Overrides of RoleProvider that throw NotImplementedException
 
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }
 
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }
 
        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
 
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }
 
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }
 
        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }
 
        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }
 
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }
 
        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
 
        #endregion
    }
}