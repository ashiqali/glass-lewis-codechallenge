using CompanyPortal.DTO.DTOs.Jwt;
using CompanyPortal.DTO.DTOs.User;

namespace CompanyPortal.BLL.Services.IServices;

public interface IAuthService
{
    Task<UserToReturnDTO> LoginAsync(UserToLoginDTO userToLoginDTO);
    Task<UserToReturnDTO> RegisterAsync(UserToRegisterDTO userToRegisterDTO);
    RefreshTokenToReturnDTO RefreshToken(RefreshTokenDTO refreshTokenDTO);
}
