using Common.DTOs;
using Common.Helplers;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace Infrastructure.Repositories;
public class AuthReposiotry : IAuthRepository
{
    private readonly IHttpContextAccessor contextAccessor;
    private readonly AppDbContext appDbContext;
    private readonly IConfiguration configuration;


    public AuthReposiotry(AppDbContext appDbContext, IConfiguration configuration, IHttpContextAccessor contextAccessor)
    {
        this.appDbContext = appDbContext;
        this.configuration = configuration;
        this.contextAccessor = contextAccessor;
    }



    public async Task<int> ActiveAccountAsync(string email, string code)
    {
        var user = await appDbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        if (user == null) throw new BadHttpRequestException("Email does not exist");
        if(user.SecurityCode != code) throw new BadHttpRequestException("Security code does not match");
        user.SecurityCode = string.Empty;
        user.IsActive = true;
        appDbContext.Entry(user).State = EntityState.Modified;
        return await appDbContext.SaveChangesAsync();
    }

    public async Task<int> ChangeForgotPasswordAsync(string token, string newPassword)
    {
        var user = await appDbContext.Users.FirstOrDefaultAsync(x=>x.TokenResetPassword == token);
        if (user == null) throw new BadHttpRequestException("User does not exist token");
        if (user.TokenResetPasswordExpires < DateTime.Now) throw new UnauthorizedAccessException("Token expires");
        if (string.IsNullOrWhiteSpace(user.SecurityCode)) throw new UnauthorizedAccessException("You haven't taken the step to confirm the code. Request this step");
        user.TokenResetPassword = string.Empty;
        user.TokenResetPasswordExpires = DateTime.MinValue;
        user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
        appDbContext.Entry(user).State = EntityState.Modified;
        return await appDbContext.SaveChangesAsync();
    }

    public async Task<int> ChangePasswordAsync(Guid id, string oldPassword, string newPassword)
    {
        var user = await appDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user == null) throw new BadHttpRequestException("User does not exist");
        if (!BCrypt.Net.BCrypt.Verify(oldPassword, user.Password)) throw new BadHttpRequestException("Old password does not match");
        user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
        appDbContext.Entry(user).State = EntityState.Modified;
        return await appDbContext.SaveChangesAsync();
    }

    public async Task<string?> ForgotPasswordAsync(string email)
    {
        var user = await appDbContext.Users.FirstOrDefaultAsync(x=>x.Email == email);
        if (user == null) throw new BadHttpRequestException("User does not exist");
        user.TokenResetPassword = GenerateTokenResetPassword();
        user.TokenResetPasswordExpires = DateTime.Now.AddMinutes(10);
        user.SecurityCode = RandomHelper.RandomInt(6);
        appDbContext.Entry(user).State = EntityState.Modified;
        var result =  await appDbContext.SaveChangesAsync();
        if (result > 0)
        {
            var mailHelper = new MailHelper(configuration);
            mailHelper.Send(configuration["Gmail:Username"]!, email, "Verification Code", MailHelper.HtmlVerify(user.SecurityCode));
        }
        return user.TokenResetPassword;
    }

    public async Task<ApiDto?> LoginAsync(string username, string password)
    {
        try
        {
            var user = await appDbContext.Users.FirstOrDefaultAsync(x =>
                       x.Username == username.Trim().ToLower() || x.Email == username.Trim().ToLower());
            
            if (user == null) throw new BadHttpRequestException("User does not exist");
            
            if (!BCrypt.Net.BCrypt.Verify(password, user.Password)) throw new BadHttpRequestException("Password does not match");
            if (!user.IsActive) throw new BadHttpRequestException("The user is not yet activated");
            var accessToken = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken.Token;
            user.RefeshTokenExpires = refreshToken.Expires;
            appDbContext.Entry(user).State = EntityState.Modified;
            await appDbContext.SaveChangesAsync();
            return new ApiDto((int)HttpStatusCode.OK,true,"Bearer", accessToken, int.Parse(configuration["AppSettings:ExpiresJwt"]!) * 60, user.RefreshToken);
        }catch(Exception ex)
        {
           Debug.WriteLine(ex.Message);
            return null;
        }
       
    }

    public async Task<ApiDto?> RefreshTokenAsync(string refreshToken)
    {
        var user = await appDbContext.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
        if (user == null) throw new BadHttpRequestException("Refresh Token does not exist");
        if (user.RefeshTokenExpires < DateTime.Now) throw new BadHttpRequestException("Refresh token expires");
        var accessToken = GenerateJwtToken(user);
        return new ApiDto((int)HttpStatusCode.OK, true,"Bearer", accessToken, int.Parse(configuration["AppSettings:ExpiresJwt"]!) * 60, user.RefreshToken);
    }

    public async Task<int> RevokeTokenAsync(Guid? id)
    {
        var user = await appDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user == null) throw new UnauthorizedAccessException("User does not exist");
        user.RefreshToken = string.Empty;
        user.RefeshTokenExpires = DateTime.Now;
        appDbContext.Entry(user).State = EntityState.Modified;
        return await appDbContext.SaveChangesAsync();
    }

    public async Task<int> VerifySecurityCodeAsync(string token, string code)
    {
        var user = await appDbContext.Users.FirstOrDefaultAsync(x => x.TokenResetPassword == token);
        if (user == null) throw new BadHttpRequestException("Token does not exist");
        if (user.TokenResetPasswordExpires < DateTime.Now) throw new UnauthorizedAccessException("Token expires");
        if (user.SecurityCode != code) throw new BadHttpRequestException("Security code does not match");
        user.SecurityCode = string.Empty;
        appDbContext.Entry(user).State = EntityState.Modified;
        return await appDbContext.SaveChangesAsync();

    }
    private string GenerateTokenResetPassword()
    {
        return Convert.ToHexString(RandomNumberGenerator.GetBytes(128));
    }
    private string GenerateJwtToken(User user)
    {
        var expiresJwt = double.Parse(configuration["AppSettings:ExpiresJwt"]!);
        List<Claim> claims = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role!.Name.ToString()),
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToString()!),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
            new Claim(ClaimTypes.StreetAddress, user.Address),
            new Claim(ClaimTypes.Expiration, DateTime.Now.AddMinutes(15).ToString()),
           
        };

        //var claimJwts = new Claim[] {
        //    new (JwtRegisteredClaimNames.Iss, configuration["AppSettings:ValidIssuer"]!),
        //    new (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        //    new (JwtRegisteredClaimNames.Aud,configuration["AppSettings:ValidAudience"]!),
        //    new (JwtRegisteredClaimNames.Email, user.Email),
        //    new (JwtRegisteredClaimNames.Exp, DateTime.Now.AddMinutes(expiresJwt).ToString()),
        //    new (JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
        //    new (JwtRegisteredClaimNames.Name, user.FullName),
        //    new (JwtRegisteredClaimNames.Birthdate, user.DateOfBirth.ToString()),
        //    new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //    new (ClaimTypes.Role, user.Role.Name)
        //};
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            configuration.GetSection("AppSettings:Token").Value!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            issuer: configuration.GetSection("AppSettings:ValidIssuer").Value,
            audience: configuration.GetSection("AppSettings:ValidAudience").Value,
            claims: claims,
            expires: DateTime.Now.AddMinutes(expiresJwt),
            signingCredentials: creds
            );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

    private RefreshTokenDto GenerateRefreshToken()
    {
        var expires = DateTime.Now.AddDays(double.Parse(configuration["AppSettings:ExpiresRefreshJwt"]!));
        var more = Guid.NewGuid().ToString().Replace("-", "");
        var token = more + Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)) + expires.ToString("yyyyMMddHHmmss");
      /*  var cookieOption = new CookieOptions
        {
            HttpOnly = true,
            Expires = expires,
        };*/

        var apiToken = new RefreshTokenDto
        {
             Token = token,
             Expires = expires
        };
        return apiToken;
    }

    public string IpAddress()
    {
        var ipAddress = contextAccessor.HttpContext!.Connection.LocalIpAddress!.ToString();
        return ipAddress;
    }
}
