using SocialNetwork.Application.Services.Auths.DTOs;

namespace SocialNetwork.Application.Services.Auths
{
    public interface IAuthenticationService
    {
        Task<ApiResponse<RegisterResponse>> Register(RegisterRequest registerRequest);
        Task<ApiResponse<LoginResponse>> Login(LoginRequest loginRequest);
        Task<ApiResponse<RefreshTokenDto>> Refresh(RefreshTokenDto refreshToken);
    }
}
