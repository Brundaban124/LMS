using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LMS.API.Controllers.Base;
using LMS.API.DTO;
using LMS.Data;
using LMS.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController<User, AuthRepository>
    {
        private IConfiguration _config;
        public AuthController(AuthRepository authRepository,IConfiguration config) : base(authRepository)
        {
            this._config = config;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForLoginDTO userForLoginDTO)
        {
            try
            {
                var userName = userForLoginDTO.UserName.ToLower();
                
                if (await base.Repository.UserExists(userName))
                    return BadRequest("Username already exists");

                var userToCreate = new User
                {
                    Name = userName,
                    EmailId = userName.Trim()+"@LMSPOC.com"
                };

                var createdUser = await base.Repository.Register(userToCreate, userForLoginDTO.Password);
            }
            catch(Exception ex) { }

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDTO userForLoginDTO)           
        {           
            try
            {
                var userFromRepo = await base.Repository.Login(userForLoginDTO.UserName, userForLoginDTO.Password);
                if (userFromRepo == null) return Unauthorized();

                
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),
                    new Claim(ClaimTypes.Name,userFromRepo.Name)

                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return Ok(new { token = tokenHandler.WriteToken(token) });
            }
            catch (Exception ex) { }

            return StatusCode(201);
        }



    }
}