using CmsApi.ModelsDto;

namespace CmsApi.Iservices
{
    public interface IAuthUserService
    {
        Task<ResponseDto> LoginUser(LoginDto userInput);
        string GenerateToken(string username, string secretKey, int expiryMinutes = 30);
        (string hash, string salt) EncryptionPassword(string password);
        bool VerifyPassword(string password, string storedHash, string storedSalt);
    }
}
