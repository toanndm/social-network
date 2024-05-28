using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using SocialNetwork.Application.Repositories;
using SocialNetwork.Application.Services.Auths.DTOs;
using SocialNetwork.Application.Services.Auths.Jwt;
using SocialNetwork.Application.Utils;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Application.Services.Auths
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtFactory _jwtFactory;
        private readonly IMapper _mapper;
        public AuthenticationService(IUserRepository userRepository, IJwtFactory jwtFactory, IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtFactory = jwtFactory;
            _mapper = mapper;
        }

        public async Task<ApiResponse<LoginResponse>> Login(LoginRequest loginRequest)
        {
            try
            {
                User currentUser = await _userRepository.GetUserByEmail(loginRequest.Email);
                var password = PasswordHasher.HashPassword(loginRequest.Password);
                if (password != currentUser.Password)
                {
                    return new ApiResponse<LoginResponse>
                    {
                        Code = 500,
                        Message = "Passwords is incorrect",
                        Errors = new List<string> { "Passwords is incorrect" }
                    };
                }
                LoginResponse loginResponse = _mapper.Map<LoginResponse>(currentUser);
                loginResponse.AccessToken = _jwtFactory.GenerateToken(currentUser, 1);
                loginResponse.RefreshToken = _jwtFactory.GenerateToken(currentUser, 10);

                return new ApiResponse<LoginResponse>
                {
                    Code = 200,
                    Message = "OK",
                    Data = loginResponse
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<LoginResponse>
                {
                    Code = 500,
                    Message = "An error occurred while processing the request",
                    Errors = new List<string> { ex.Message }
                };
            }
        }
        public async Task<ApiResponse<RefreshTokenDto>> Refresh(RefreshTokenDto refreshToken)
        {
            try
            {
                var refreshTokenPayload = _jwtFactory.VerifyToken(refreshToken.Token);
                if (refreshTokenPayload == null || !refreshTokenPayload.ContainsKey("Id"))
                {
                    return new ApiResponse<RefreshTokenDto>
                    {
                        Code = 401,
                        Message = "Invalid refresh token",
                        Errors = new List<string> { "Invalid refresh token" }
                    };
                }
                var userId = refreshTokenPayload["Id"].ToString();

                var currentUser = await _userRepository.GetAsync(Guid.Parse(userId));
                if (currentUser == null)
                {
                    return new ApiResponse<RefreshTokenDto>
                    {
                        Code = 404,
                        Message = "User not found",
                        Errors = new List<string> { "User not found" }
                    };
                }

                var token = _jwtFactory.GenerateToken(currentUser, 10);

                var responeToken = new RefreshTokenDto(token);

                return new ApiResponse<RefreshTokenDto>
                {
                    Code = 200,
                    Message = "OK",
                    Data = responeToken
                };
            }
            catch (SecurityTokenException ex)
            {
                return new ApiResponse<RefreshTokenDto>
                {
                    Code = 401,
                    Message = "Invalid token",
                    Errors = new List<string> { ex.Message }
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<RefreshTokenDto>
                {
                    Code = 500,
                    Message = "An error occurred while processing the request",
                    Errors = new List<string> { ex.Message }
                };
            }
        }
        public async Task<ApiResponse<RegisterResponse>> Register(RegisterRequest registerRequest)
        {
            try
            {
                var existingUser = await _userRepository.GetUserByEmail(registerRequest.Email);
                if (existingUser != null)
                {
                    return new ApiResponse<RegisterResponse>
                    {
                        Code = 400,
                        Message = "User already exists",
                        Errors = new List<string> { "User already exists" }
                    };
                }
                if (registerRequest.Password != registerRequest.PasswordConfirm)
                {
                    return new ApiResponse<RegisterResponse>
                    {
                        Code = 500,
                        Message = "Passwords do not match",
                        Errors = new List<string> { "Passwords do not match" }
                    };
                }
                var user = _mapper.Map<User>(registerRequest);

                user.Id = Guid.NewGuid();
                user.Password = PasswordHasher.HashPassword(registerRequest.Password);
                user.CreatedAt = DateTime.UtcNow;
                user.UpdatedAt = DateTime.UtcNow;
                user.IsReported = false;
                user.IsBlocked = false;

                var createdUser = await _userRepository.InsertAsync(user);
                return new ApiResponse<RegisterResponse>
                {
                    Code = 200,
                    Message = "OK",
                    Data = _mapper.Map<RegisterResponse>(createdUser)
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<RegisterResponse>
                {
                    Code = 500,
                    Message = "An error occurred while processing the request",
                    Errors = new List<string> { ex.Message }
                };
            }
        }
    }
}
