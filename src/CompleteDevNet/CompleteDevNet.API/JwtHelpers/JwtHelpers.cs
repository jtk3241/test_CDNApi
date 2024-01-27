using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CompleteDevNet.API.JwtHelpers;

public static class JwtHelpers
{
    public static IEnumerable<Claim> GetClaims(this UserTokenModel userAccounts, Guid Id)
    {
        IEnumerable<Claim> claims = new Claim[] {
                    new Claim(ClaimTypes.PrimarySid, userAccounts.GuidId.ToString()),
                    new Claim(ClaimTypes.Name, userAccounts.UserName),
                    new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt"))
            };
        return claims;
    }

    public static IEnumerable<Claim> GetClaims(this UserTokenModel userAccounts, out Guid Id)
    {
        Id = Guid.NewGuid();
        return GetClaims(userAccounts, Id);
    }

    public static UserTokenModel GenTokenkey(UserTokenModel model, JwtSettings jwtSettings)
    {
        var userToken = new UserTokenModel();
        if (model == null) throw new ArgumentException(nameof(model));
        // Get secret key
        var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);
        Guid Id = Guid.Empty;
        userToken.Validaty = model.ExpiredTime.TimeOfDay;
        userToken.ExpiredTime = model.ExpiredTime;
        var JWToken = new JwtSecurityToken(
            issuer: jwtSettings.ValidIssuer,
            audience: jwtSettings.ValidAudience,
            claims: GetClaims(model, out Id),
            notBefore: new DateTimeOffset(DateTime.Now).DateTime,
            expires: new DateTimeOffset(model.ExpiredTime).DateTime,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        );
        userToken.Token = new JwtSecurityTokenHandler().WriteToken(JWToken);
        userToken.UserName = model.UserName;
        userToken.GuidId = Id;
        return userToken;
    }
}
