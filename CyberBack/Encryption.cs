namespace CyberBack
{
    public class Encryption
    {
        private static readonly Encryption Instance = new();

        private Encryption()
        {
        }

        public static Encryption GetInstance()
        {
            return Instance;
        }

        public string GenerateSalt()
        {
            return BCrypt.GenerateSalt();
        }

        public string HashPassword(string password, string salt)
        {
            return BCrypt.HashPassword(password, salt);
        }
    }
}