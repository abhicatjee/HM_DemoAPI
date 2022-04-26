using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineTestAPI.Entities;
using OnlineTestAPI.Models;
using OnlineTestAPI.Services;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace OnlineTestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService accountService;

        public AccountController(AccountService accountService)
        {
            this.accountService = accountService;
        }
        [HttpPost]
        [Route("SignUp")]
        public IActionResult SignUp(User user)
        {
            try
            {
                accountService.Register(user);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("SignIn")]
       // [AllowAnonymous]
        public IActionResult SignIn(Login login)
        {
            try
            {
                Auth auth = new Auth();
                User user = accountService.ValidateUser(login);
                if(user != null)
                {
                    auth.UserId = user.UserId;
                    auth.Name = user.Name;
                    auth.Role = user.Role;
                    auth.Token = GetToken(user);
                
                }
                return Ok(auth);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetAllUsers")]
        [Authorize(Roles = "admin")]
        public IActionResult GetAllUsers()
        {
            try
            {
                List<User> users = accountService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        private string GetToken(User user)
        {
            var _config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json").Build();
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var expiry = DateTime.Now.AddMinutes(120);
            var securityKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials
            (securityKey, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
            {
new Claim(ClaimTypes.Name, user.UserId.ToString()),
new Claim(ClaimTypes.Role, user.Role)
            }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };



            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
    }
}
