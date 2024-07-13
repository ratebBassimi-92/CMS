using CmsApi.DatabaseContext;
using CmsApi.Iservices;
using CmsApi.Models;
using CmsApi.ModelsDto;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CmsApi.Services
{
    public class AuthUserService : IAuthUserService
    {
        private readonly CmsDbContext _dbCmsContext;
        //private readonly IConfigurationRoot _configuration;
        public AuthUserService(CmsDbContext dbCmsContext)
        {
            _dbCmsContext = dbCmsContext;
            
        }
        public string GenerateToken(string username, string secretKey, int expiryMinutes = 30)
        {
            // Create security key
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Create claims
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Create token
            var token = new JwtSecurityToken(
                issuer: "https://localhost:7150",
                audience: "https://localhost:7150/",
                claims: claims,
                expires: DateTime.Now.AddMinutes(expiryMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public (string hash, string salt) EncryptionPassword(string password)
        {
            // Generate a random salt
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }

            // Create the Rfc2898DeriveBytes and get the hash value
            var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000);
            byte[] hashBytes = pbkdf2.GetBytes(20);

            // Combine the salt and hash bytes
            byte[] hashWithSaltBytes = new byte[36];
            Array.Copy(saltBytes, 0, hashWithSaltBytes, 0, 16);
            Array.Copy(hashBytes, 0, hashWithSaltBytes, 16, 20);

            // Convert to base64 for storage
            string hashWithSaltBase64 = Convert.ToBase64String(hashWithSaltBytes);
            string saltBase64 = Convert.ToBase64String(saltBytes);

            return (hashWithSaltBase64, saltBase64);
        }

        public  bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            // Convert stored salt to bytes
            byte[] saltBytes = Convert.FromBase64String(storedSalt);

            // Create the Rfc2898DeriveBytes and get the hash value
            var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000);
            byte[] hashBytes = pbkdf2.GetBytes(20);

            // Convert stored hash to bytes
            byte[] storedHashBytes = Convert.FromBase64String(storedHash);

            // Compare the hashes
            for (int i = 0; i < 20; i++)
            {
                if (storedHashBytes[i + 16] != hashBytes[i])
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<ResponseDto> LoginUser(LoginDto userInput)
        {
    
            ResponseDto responce = new ResponseDto();

            if(userInput is not null)
            {
                var usersData = _dbCmsContext.Users.AsNoTracking().Where(x => x.UserName == userInput.UserName && x.Password ==userInput.Password).FirstOrDefault();
                if(usersData !=null)
                {
                    var (hash, salt) = EncryptionPassword(userInput.Password);
                    bool isPasswordCorrect = VerifyPassword(userInput.Password, hash, salt);
                    if(isPasswordCorrect)
                    {
                        responce.Message = "The UserName and Password Correct";
                        responce.Success = true;
                        responce.Status = StatusCodes.Status200OK.ToString();
                        responce.Data = new { Token = GenerateToken(userInput.UserName, "cms@P@ssw0rd$123@DanatCustomerManagment") };
                    }
                    else
                    {
                        responce.Message = "The Password Not Valid";
                        responce.Success = false;
                        responce.Status = StatusCodes.Status401Unauthorized.ToString();
                    }
                }
                else
                {
                    responce.Message = "The UserName or Password un Correct";
                    responce.Success = false;
                    responce.Status = StatusCodes.Status404NotFound.ToString();
                }
            }
            else
            {
                responce.Message = "check from inputs Please";
                responce.Success = false;
                responce.Status = StatusCodes.Status404NotFound.ToString();
            }
            return responce;
        }
    }


        /*
        string password = "your_password_here";
        var(hash, salt) = PasswordEncryption.EncryptPassword(password);
        Console.WriteLine($"Hash: {hash}");
        Console.WriteLine($"Salt: {salt}");

        bool isPasswordCorrect = PasswordEncryption.VerifyPassword("your_password_here", hash, salt);
        Console.WriteLine($"Password is correct: {isPasswordCorrect}");
        */
  
}
