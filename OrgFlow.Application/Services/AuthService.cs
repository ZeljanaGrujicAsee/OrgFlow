using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OegFlow.Domain.Models;
using OrgFlow.Application.Interfaces;
using OrgFlow.Domain.Entities;
using OrgFlow.Domain.Enums;
using OrgFlow.Infrastructure.Interfaces;

namespace OrgFlow.Application.Services
{

    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;
        private readonly IRolePermissionsRepository _permissionRepository;
        private readonly IUserRolesRepository _rolesRepository;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
             IUserRepository userRepository,
            IConfiguration config,
            IRolePermissionsRepository permissionRepository,
            IUserRolesRepository rolesRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
            _userRepository = userRepository;
            _permissionRepository = permissionRepository;
            _rolesRepository = rolesRepository;
        }

        public async Task<(bool IsSuccess, string Message)> RegisterAsync(RegisterRequest request, string role)
        {
            var existing = await _userManager.FindByNameAsync(request.UserName);
            if (existing != null)
                return (false, "User already exists");
            var userInOrg = await _userRepository.GetUserBaseDataByEmailAsync(request.Email);
            if (userInOrg == null)
                return (false, "User it is not part of any organization");

            var user = new ApplicationUser
            {
                UserId = userInOrg.Id,
                OrganizationId = userInOrg.Department.OrganizationId,
                DepartmentId = (int)userInOrg.DepartmentId,
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            var result = await _userManager.CreateAsync(user, request.PasswordHash);

            if (!result.Succeeded)
                return (false, "User creation failed");

            if (!await _roleManager.RoleExistsAsync(Enum.GetName(typeof(Role), userInOrg.Position.Role)))
                await _roleManager.CreateAsync(new IdentityRole(Enum.GetName(typeof(Role), userInOrg.Position.Role)));

            await _userManager.AddToRoleAsync(user, Enum.GetName(typeof(Role), userInOrg.Position.Role));

            return (true, "User created successfully");
        }

        public async Task<(bool IsSuccess, string Token)> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
                return (false, "Invalid username");

            if (!await _userManager.CheckPasswordAsync(user, request.Password))
                return (false, "Invalid password");

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserId", user.UserId.ToString()),
                new Claim("OrganizationId", user.OrganizationId.ToString()),
                new Claim("DepartmentId", user.DepartmentId.ToString())
            };

            foreach (var r in roles)
                claims.Add(new Claim(ClaimTypes.Role, r));

            var userRole = await _rolesRepository.GetByNameAsync(roles.FirstOrDefault());
            var rolePermissions = await _permissionRepository.GetRolePermissions(userRole.Id);
            

            foreach (var perm in rolePermissions)
            {
                claims.Add(new Claim("Permission", perm.Name));
            }

            return (true, GenerateJwt(claims));
        }

        private string GenerateJwt(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

