
using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Newtonsoft.Json;

namespace SecurityServices.AwsServices
{
    public static class SecretsManager
    {
        public static async Task<Dictionary<string, string>?> GetSecret(string secretName)
        {
            string region = "us-east-2";

            IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));

            GetSecretValueRequest request = new GetSecretValueRequest
            {
                SecretId = secretName,
                VersionStage = "AWSCURRENT", // VersionStage defaults to AWSCURRENT if unspecified.
            };

            GetSecretValueResponse response;

            try
            {
                response = await client.GetSecretValueAsync(request);
            }
            catch (Exception e)
            {
                throw e;
            }

            var secret = JsonConvert.DeserializeObject<Dictionary<string,string>>(response.SecretString);
            return secret;

            // Your code goes here
        }
    }
}
