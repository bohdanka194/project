using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace books
{
    public class AuthModel
    {
        public AuthModel(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; set; }
        public string Password { get; set; }
    }

    [Produces("application/json")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private List<Person> people = new List<Person>
        {
            new Person { Login="admin@gmail.com", Password="12345" },
            new Person { Login="qwerty", Password="55555", }
        };

        [HttpPost]
        [Route("api/auth/token")]
        public IActionResult Token([FromBody] AuthModel requestBody)
        {
            var identity = GetIdentity(requestBody.Username, requestBody.Password);
            if (identity == null)
            {
                return BadRequest("Invalid username or password.");
            }

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                AuthOptions.ISSUER,
                AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(
                    TimeSpan.FromMinutes(AuthOptions.LIFETIME)
                ),
                signingCredentials: new SigningCredentials(
                    AuthOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256)
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
             
            return Ok(new
            {
                access_token = encodedJwt,
                username = identity.Name,
                available_to = jwt.ValidTo
            });
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            Person person = people.FirstOrDefault(x => x.Login == username && x.Password == password);

            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }
    }
}