using SecurityServices.AwsServices;

namespace SecurityServices.Common
{
    public static class EnvironmentHelper
    {
        public static string AWS_ACCESS_KEY_ID { get; set; } = "";
        public static string AWS_SECRET_ACCESS_KEY { get; set; } = "";
        public static string AWS_REGION { get; set; } = "us-east-1";
        
        public static string SECRET_COMMON_EMAIL { get; set; } = "";
        public static string COMMON_EMAIL_ID { get; set; } = "";
        public static string COMMON_EMAIL_APIKEY { get; set; } = "";
        public static string SECRET_RDS { get; set; } = "";
        public static string RDS_USERNAME { get; set; } = "";
        public static string RDS_PASSWORD { get; set; } = "";
        public static string RDS_DATABASE { get; set; } = "";
        public static string RDS_HOST { get; set; } = "";


        public static void LoadEnvVars()
        {
            AWS_ACCESS_KEY_ID = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID") ?? "";
            AWS_SECRET_ACCESS_KEY = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY") ?? "";
            SECRET_COMMON_EMAIL = Environment.GetEnvironmentVariable("SECRET_COMMON_EMAIL") ?? "";
            SECRET_RDS = Environment.GetEnvironmentVariable("SECRET_RDS") ?? "";
        }

        public static void LoadCommonSecrets()
        {
            var emailSecret = SecretsManager.GetSecret(SECRET_COMMON_EMAIL).Result;
            COMMON_EMAIL_ID = emailSecret["email"];
            COMMON_EMAIL_APIKEY = emailSecret["apikey"];

            var rdsSecret = SecretsManager.GetSecret(SECRET_RDS).Result;
            RDS_HOST = rdsSecret["host"];
            RDS_DATABASE = rdsSecret["database"];
            RDS_USERNAME = rdsSecret["username"];
            RDS_PASSWORD = rdsSecret["password"];

        }



    }
}
