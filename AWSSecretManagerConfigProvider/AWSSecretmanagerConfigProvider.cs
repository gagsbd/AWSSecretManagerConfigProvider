using Amazon.SecretsManager;
using Microsoft.Extensions.Configuration;
using System.Net.Sockets;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace Gags.AWSSecretManager
{
    public class AWSSecretManagerConfigProvider : ConfigurationProvider
    {
       
        private readonly string? _awsRegion;
        private readonly string? _awsSecretName;

        public AWSSecretManagerConfigProvider(string? awsRegion, string? awsSecretName)
        {
            this._awsRegion = awsRegion;
            this._awsSecretName = awsSecretName;
        }

        public override async void Load()
        {
            var secret = await GetSecret();

            Data = JsonSerializer.Deserialize<Dictionary<string, string?>>(secret);
        }

        public override bool TryGet(string key, out string? value)
        {
            return base.TryGet(key, out value);
        }

        private async Task<string> GetSecret()
        {
            var secret = string.Empty;
            try
            {
               
                AmazonSecretsManagerClient clnt = new AmazonSecretsManagerClient(Amazon.RegionEndpoint.GetBySystemName(_awsRegion));

                var response = await clnt.GetSecretValueAsync(new Amazon.SecretsManager.Model.GetSecretValueRequest() { SecretId = _awsSecretName, VersionStage = "AWSCURRENT" });

                secret = response?.SecretString;

                if (String.IsNullOrEmpty(secret) && response?.SecretBinary != null)
                {

                    var reader = new StreamReader(response.SecretBinary);
                    secret = System.Text.Encoding.UTF8
                                  .GetString(Convert.FromBase64String(reader.ReadToEnd()));
                }

            }
            catch (Exception)
            {
                throw;
            }

            return secret;
        }
    }
}