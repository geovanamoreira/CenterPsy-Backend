using Microsoft.AspNetCore.Identity;

namespace CenterPsy_Backend.Security
{
    public class PasswordService
    {
        private readonly PasswordHasher<object> _hasher = new();

        public string HashPassword(string password)
        {
            return _hasher.HashPassword(new object(), password);
        }

        public bool IsHashedPassword(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            try
            {
                var bytes = Convert.FromBase64String(value);

                return bytes.Length > 0 &&
                       (bytes[0] == 0x00 || bytes[0] == 0x01);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var result = _hasher.VerifyHashedPassword(
                new object(),
                hashedPassword,
                providedPassword
            );

            return result != PasswordVerificationResult.Failed;
        }
    }
}
