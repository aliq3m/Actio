using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Actio.Common.Auth
{
  public  class JwtHandler:IJwtHandler
  {
      private JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
      private JwtOptions _options;
      private SecurityKey _issuerSecurityKey;
      private SigningCredentials _signingCredentials;
      private JwtHeader _jwtHeader;
      private TokenValidationParameters _tokenValidationParameters;

      public JwtHandler(IOptions<JwtOptions> options)
      {
          _options = options.Value;
          _issuerSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecurityKey));
          _signingCredentials = new SigningCredentials(_issuerSecurityKey, SecurityAlgorithms.HmacSha256);
          _jwtHeader = new JwtHeader(_signingCredentials);
          _tokenValidationParameters = new TokenValidationParameters()
          {
              ValidateAudience = false,
              IssuerSigningKey = _issuerSecurityKey,
              ValidIssuer = _options.Issuer
          };
      }

      public JsonWebToken Create(Guid UserId)
      {
          var nowUtc = DateTime.UtcNow;
          var expires = nowUtc.AddMinutes(_options.ExpiryMinutes);
          var centuryBegin = new DateTime(1970,1,1).ToUniversalTime();
          var exp = (long) (new TimeSpan(expires.Ticks - centuryBegin.Ticks).TotalSeconds);
          var now = (long) (new TimeSpan(nowUtc.Ticks - centuryBegin.Ticks).TotalSeconds);

          var payload = new JwtPayload()
          {
              {"sub",UserId},
              {"iss",_options.Issuer},
              {"iat",now},
              {"exp",exp},
              {"unique_name",UserId},
          };

          var jwt = new JwtSecurityToken(_jwtHeader, payload);
          var token = new JwtSecurityTokenHandler().WriteToken(jwt);

          return new JsonWebToken()
          {
              Expires = exp,
              Token = token
          };

      }
  }
}
