using Fakezapp.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Fakezapp.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    public class AuthMutation
    {
        private readonly IOptions<TokenSettings> _tokenSettings;
        private List<User> Users = new List<User>
        {
            new User{
                Id = 1,
                Name = "Yan Esteves",
                Email = "yan.m.esteves@gmail.com",
                Password="123456"
            },
            new User{
                Id = 2,
                Name = "Marco",
                Email = "marco@gmail.com",
                Password = "abcdef"
            }
        };

        public AuthMutation(IOptions<TokenSettings> tokenSettings)
        {
            _tokenSettings = tokenSettings;
        }


        public string UserLogin(Login login)
        {
            var currentUser = Users.Where(_ => _.Email.ToLower() == login.Email.ToLower() &&
                        _.Password == login.Password).FirstOrDefault();
            if (currentUser != null)
            {
                var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Value.Key));
                var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

                var jwtToken = new JwtSecurityToken(
                    issuer: _tokenSettings.Value.Issuer,
                    audience: _tokenSettings.Value.Audience,
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(jwtToken);

            }
            return "";
        }
    }
}