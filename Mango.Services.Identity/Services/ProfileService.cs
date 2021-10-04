using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using Mango.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mango.Services.Identity.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ProfileService(IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            string username = context.Subject.Identity.IsAuthenticated? 
                context.Subject.Identity.Name:
                 context.Subject.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (!string.IsNullOrEmpty(username))
            {
                ApplicationUser user = await _userManager.FindByNameAsync(username);
                ClaimsPrincipal userClaims = await _userClaimsPrincipalFactory.CreateAsync(user);
                List<Claim> claims = userClaims.Claims.ToList();
                claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();
                claims.Add(new Claim(JwtClaimTypes.FamilyName, user.LastName));
                claims.Add(new Claim(JwtClaimTypes.GivenName, user.FirstName));
                claims.Add(new Claim(JwtClaimTypes.Email, user.Email));
                claims.Add(new Claim(JwtClaimTypes.Name, user.UserName));

                if (_userManager.SupportsUserRole)
                {
                    IList<string> roles = await _userManager.GetRolesAsync(user);
                    foreach (string roleName in roles)
                    {
                        claims.Add(new Claim(JwtClaimTypes.Role, roleName));

                        if (_roleManager.SupportsRoleClaims)
                        {
                            IdentityRole role = await _roleManager.FindByNameAsync(roleName);

                            if (role != null)
                            {
                                claims.AddRange(await _roleManager.GetClaimsAsync(role));
                            }
                        }
                    }
                }

                context.IssuedClaims = claims;
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            string username = context.Subject.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Email)?.Value;

            if(!string.IsNullOrEmpty(username))
            {
                ApplicationUser user = await _userManager.FindByNameAsync(username);
                context.IsActive = user != null;
            }            
        }
    }
}
