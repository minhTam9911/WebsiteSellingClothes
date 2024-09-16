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
using Application.DTOs.Responses;
using Common.DTOs;

namespace WebAPI.Controllers.V1;
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class AuthsController : ApiControllerBase
{
    /// <summary>
    /// This API is for app login 
    /// </summary>
    /// <param name="loginRequestDto"></param>
    /// <returns></returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(ErrorValidationResponse),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse),StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        var data = await Sender.Send(new LoginCommand() { LoginRequestDto = loginRequestDto });
        return Ok(data);
    }

    /// <summary>
    /// This API is for activating accounts into the app
    /// </summary>
    /// <param name="activeAccountCommand"></param>
    /// <returns></returns>
    [HttpGet("active-account")]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ServiceContainerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ActiveAccount([FromQuery] ActiveAccountCommand activeAccountCommand)
    {
        var data = await Sender.Send(new ActiveAccountCommand() { Code = activeAccountCommand.Code, Email = activeAccountCommand.Email });
        return Ok(data);
    }

    /// <summary>
    /// This is an API for changing forgotten passwords
    /// </summary>
    /// <param name="resetPasswordRequestDto"></param>
    /// <returns></returns>
    [HttpPost("change-forgot-password")]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ServiceContainerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ChangeForgotPassword([FromBody] ResetPasswordRequestDto resetPasswordRequestDto)
    {
        var data = await Sender.Send(new ChangeForgotPasswordCommand() { ResetPasswordRequestDto = resetPasswordRequestDto });
        return Ok(data);
    }


    /// <summary>
    /// This is an API for changing new passwords
    /// </summary>
    /// <param name="changePasswordRequestDto"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost("change-password")]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ServiceContainerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto changePasswordRequestDto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var data = await Sender.Send(new ChangePasswordCommand() { UserId = userId, ChangePasswordRequestDto = changePasswordRequestDto });
        return Ok(data);
    }

    /// <summary>
    /// This is an API for forgetting passwords
    /// </summary>
    /// <param name="forgotPasswordRequestDto"></param>
    /// <returns></returns>
    [HttpPost("forgot-password")]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ForgotPasswordResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto forgotPasswordRequestDto)
    {
        var data = await Sender.Send(new ForgotPasswordCommand() { ForgotPasswordRequestDto = forgotPasswordRequestDto });
        return Ok(data);
    }


    /// <summary>
    /// This is the API for token refresh
    /// </summary>
    /// <param name="refreshTokenRequestDto"></param>
    /// <returns></returns>
    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto refreshTokenRequestDto)
    {
        var data = await Sender.Send(new RefreshTokenCommand() { RefreshTokenRequestDto = refreshTokenRequestDto });
        return Ok(data);
    }

    /// <summary>
    ///  This is the API used to revoke tokens
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpPost("revoke-token")]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ServiceContainerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RevokeToken()
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var data = await Sender.Send(new RevokeTokenCommand() { UserId = userId });
        return Ok(data);
    }

    /// <summary>
    /// This is an API used to confirm the code when the password is forgotten
    /// </summary>
    /// <param name="verifySecurityCodeRequestDto"></param>
    /// <returns></returns>
    [HttpPost("verify-security-code")]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ServiceContainerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> VerifySecurityCode([FromBody] VerifySecurityCodeRequestDto verifySecurityCodeRequestDto)
    {
        var data = await Sender.Send(new VerifySecurityCodeCommand() { VerifySecurityCodeRequestDto = verifySecurityCodeRequestDto });
        return Ok(data);
    }
}
