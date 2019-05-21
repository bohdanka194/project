using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace books
{
    public class Person
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; 
        public const string AUDIENCE = "http://localhost:51884/"; 
        const string KEY = "mysupersecret_secretkey!123";   // 
        public const int LIFETIME = 2880; // in minutes
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}