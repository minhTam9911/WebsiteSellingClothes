
using Common.DTOs;

namespace Domain.Interfaces;
public interface IAuthRepository
{

	Task<ApiDto?> LoginAsync(string username, string password);
	Task<ApiDto?> RefreshTokenAsync(string refreshToken);
	Task<int> RevokeTokenAsync(Guid? id);
	Task<int> ChangePasswordAsync(Guid id, string oldPassword, string newPassword);
	Task<string?> ForgotPasswordAsync(string email);
	Task<int> VerifySecurityCodeAsync(string token, string code);
	Task<int> ChangeForgotPasswordAsync(string token, string newPassword);
	Task<int> ActiveAccountAsync(string email, string code);
	string IpAddress();
}
