using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Gags.AWSSecretManager
{
    public class AWSSecretmanagerConfigSource : IConfigurationSource
    {
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            var _config = builder.Build();
            var _awsRegion =_config["AWSSecret:Region"];
            var _awsSecretName = _config["AWSSecret:SecretName"];
                        
            return new AWSSecretManagerConfigProvider(_awsRegion, _awsSecretName);
        }
    }
}
