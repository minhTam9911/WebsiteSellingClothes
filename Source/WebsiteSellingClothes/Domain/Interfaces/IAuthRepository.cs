using Domain.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces;
public interface IAuthRepository
{

	Task<ApiResponseDto> LoginAsync(string username, string password);
	Task<ApiResponseDto> RefreshTokenAsync(string refreshToken, string accessToken);
	Task<int> RevokeTokenAsync(Guid? id);
	Task<int> ChangePasswordAsync(string oldPassword, string newPassword);
	Task<string?> ForgotPasswordAsync(string email);
	Task<int> VerifySecurityCodeAsync(string token, string code);
	Task<int> ChangeForgotPasswordAsync(string newPassword);
	Task<int> ActiveAccountAsync(string email, string code);

}
