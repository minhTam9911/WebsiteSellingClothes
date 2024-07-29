using Application.DTOs.Requests;
using Application.Features.AuthFeatures.Commands.ActiveAccount;
using Application.Features.AuthFeatures.Commands.ChangeForgotPassword;
using Application.Features.AuthFeatures.Commands.ChangePassword;
using Application.Features.AuthFeatures.Commands.ForgotPassword;
using Application.Features.AuthFeatures.Commands.Login;
using Application.Features.AuthFeatures.Commands.RefreshToken;
using Application.Features.AuthFeatures.Commands.RevokeToken;
using Application.Features.AuthFeatures.Commands.VerifySecurityCode;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Common.Extensions;

namespace WebAPI.Controllers.V1;
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class AuthsController : ApiControllerBase
{
    
   

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        var data = await Sender.Send(new LoginCommand() { LoginRequestDto = loginRequestDto });
        return Ok(data);
    }

    [HttpGet("active-account")]
    public async Task<IActionResult> ActiveAccount([FromQuery] ActiveAccountCommand activeAccountCommand)
    {
        var data = await Sender.Send(new ActiveAccountCommand() { Code = activeAccountCommand.Code, Email = activeAccountCommand.Email });
        return Ok(data);
    }

    [HttpPost("change-forgot-password")]
    public async Task<IActionResult> ChangeForgotPassword([FromBody] ResetPasswordRequestDto resetPasswordRequestDto)
    {
        var data = await Sender.Send(new ChangeForgotPasswordCommand() { ResetPasswordRequestDto = resetPasswordRequestDto });
        return Ok(data);
    }

    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto changePasswordRequestDto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var data = await Sender.Send(new ChangePasswordCommand() { UserId = userId, ChangePasswordRequestDto = changePasswordRequestDto });
        return Ok(data);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto forgotPasswordRequestDto)
    {
        var data = await Sender.Send(new ForgotPasswordCommand() { ForgotPasswordRequestDto = forgotPasswordRequestDto });
        return Ok(data);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto refreshTokenRequestDto)
    {
        var data = await Sender.Send(new RefreshTokenCommand() { RefreshTokenRequestDto = refreshTokenRequestDto });
        return Ok(data);
    }

    [Authorize]
    [HttpPost("revoke-token")]
    public async Task<IActionResult> RevokeToken()
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var data = await Sender.Send(new RevokeTokenCommand() { UserId = userId });
        return Ok(data);
    }

    [HttpPost("verify-security-code")]
    public async Task<IActionResult> VerifySecurityCode([FromBody] VerifySecurityCodeRequestDto verifySecurityCodeRequestDto)
    {
        var data = await Sender.Send(new VerifySecurityCodeCommand() { VerifySecurityCodeRequestDto = verifySecurityCodeRequestDto });
        return Ok(data);
    }
}
