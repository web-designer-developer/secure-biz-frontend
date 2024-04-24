using System.Security.Cryptography;

namespace SecurityServices.Utlis
{
    public static class Hash
    {
        public static string GenerateHash(string value, string salt)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(value + salt);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);

                // Convert to text
                // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
                var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }

        public static string GenerateSalt(int length) 
        {
            var salt = RandomNumberGenerator.GetInt32(length).ToString();
            return salt;
        }

        public static bool Compare(string value, string salt, string hashValue) 
        { 
            if(GenerateHash(value,salt) == hashValue)
            {
                return true;
            }
            return false;
        }
    }
}
