using System.Text;

namespace SecurityServices.Utlis
{
    public static class OtpGenerator
    {
        public static string GenerateOtp(int length)
        {
            Random random = new Random();
            int min = int.Parse("1" + new String('0', length - 1));
            int max = int.Parse(new String('9', length));
            return random.Next(min, max).ToString();
        }
    }
}
