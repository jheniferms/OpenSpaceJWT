using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OpenSpace.Models;
using OpenSpace.Request;
using OpenSpace.Sevices.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OpenSpace.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService loginService;
        private readonly IConfiguration _configuration;


        public LoginController(ILoginService loginService, IConfiguration configuration)
        {
            this.loginService = loginService;
            _configuration = configuration;
        }

        // POST api/login
        /// <summary>
        /// Realiza o login
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/login
        ///     {
        ///        "userName": "jsantos",
        ///        "password": "lux123"
        ///     }
        /// </remarks>
        /// <param name="request">Dados do usuario</param>
        /// <returns>Token</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Authenticate")]
        public IActionResult Authenticate(LoginRequest request)
        {
            try
            {
                User user = loginService.Auth(request);
                if (user == null)
                {
                    return BadRequest("Usuário ou senha inválidos no sistema");
                }

                //Passo 5 
                string token = CreateToken(user);

                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //Passo 4 - Metodo que gera o token
        private string CreateToken(User user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            string securityKey = _configuration["Jwt:Key"];
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.UserName),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.MobilePhone, user.CellPhone),
                    new Claim("NickName", user.NickName)
                    //new Claim(ClaimTypes.Role, user.UserType.ToString()),
                }),

                Expires = DateTime.UtcNow.AddDays(40),
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
