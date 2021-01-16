using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReadingIsGood.API.Application.Token
{
    public class TokenCommandHandler : IRequestHandler<TokenCommand, string>
    {
        public async Task<string> Handle(TokenCommand request, CancellationToken cancellationToken)
        {
            var key = Encoding.UTF8.GetBytes("ReadingIsGoodSecretKey");
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
