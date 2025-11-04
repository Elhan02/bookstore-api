using BookstoreApplication.DTOs;
using System.Security.Claims;

namespace BookstoreApplication.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegistrationDto data);
        Task<string> LoginAsync(LoginDto data);
        Task<string> GoogleLoginAsync(string? email, string? name, string? surname, string? pictureUrl);
        Task<ProfileDto> GetProfileAsync(ClaimsPrincipal userPrincipal);
    }
}
