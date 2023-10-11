using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gags.AWSSecretManager
{
    public static class AWSSecretmanagerConfigExteansion
    {
        public static IConfigurationBuilder AddAwsSecretmanagerConfig(this IConfigurationBuilder builder)
        {
            return builder.Add(new AWSSecretManager.AWSSecretmanagerConfigSource());
        }
    }
}
