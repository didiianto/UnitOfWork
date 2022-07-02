using Microsoft.AspNetCore.Mvc;
using UnitOfWork.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitOfWork.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace UnitOfWork.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUnitOfWork unitOfWork;
        private IConfiguration _config;

        public UserController(IUnitOfWork unitOfWork, IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            _config = config;   
        }

        private string GenerateJSONWebToken(UserViewModel userViewModel)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                      _config["Jwt:Issuer"],
                      null,
                      expires: DateTime.Now.AddMinutes(120),
                      signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private async Task<UserViewModel> AuthenticateUser(UserViewModel login)
        {
            UserViewModel user = null;

            var userListAsync = await unitOfWork.UserRepository.GetWhere(x => x.Email == login.Email && x.Password == login.Password);
            List<User> userList = userListAsync.ToList();
            if(userList.Count == 1)
            {
                user = new UserViewModel();
                user.Email = userList[0].Email;
                user.Password = userList[0].Password;
            }

            return user;
        }

        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] UserViewModel data)
        {
            IActionResult response = Unauthorized();
            var user = await AuthenticateUser(data);
            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { Token = tokenString, Message = "Success" });
            }
            return response;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await unitOfWork.UserRepository.GetAll();
        }
    }
}
