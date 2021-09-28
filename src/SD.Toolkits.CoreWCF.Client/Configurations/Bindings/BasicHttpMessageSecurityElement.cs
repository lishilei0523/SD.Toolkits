// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Configuration;
using System.ServiceModel;
using SD.Toolkits.CoreWCF.Client.Configurations.Commons;

namespace SD.Toolkits.CoreWCF.Client.Configurations.Bindings
{
    public class BasicHttpMessageSecurityElement : ServiceModelConfigurationElement
    {
        internal const BasicHttpMessageCredentialType DefaultClientCredentialType = BasicHttpMessageCredentialType.UserName;

        [ConfigurationProperty(ConfigurationStrings.ClientCredentialType, DefaultValue = BasicHttpMessageSecurityElement.DefaultClientCredentialType)]
        public BasicHttpMessageCredentialType ClientCredentialType
        {
            get { return (BasicHttpMessageCredentialType)base[ConfigurationStrings.ClientCredentialType]; }
            set { base[ConfigurationStrings.ClientCredentialType] = value; }
        }

        internal void ApplyConfiguration(BasicHttpMessageSecurity security)
        {
            if (security == null)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull(nameof(security));
            }

            security.ClientCredentialType = this.ClientCredentialType;
        }
    }
}
