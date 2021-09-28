// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Configuration;
using System.ServiceModel;
using SD.Toolkits.CoreWCF.Client.Configurations.Commons;

namespace SD.Toolkits.CoreWCF.Client.Configurations.Bindings
{
    public class MessageSecurityOverHttpElement : ServiceModelConfigurationElement
    {
        [ConfigurationProperty(ConfigurationStrings.ClientCredentialType, DefaultValue = MessageCredentialType.Windows)]
        public MessageCredentialType ClientCredentialType
        {
            get { return (MessageCredentialType)base[ConfigurationStrings.ClientCredentialType]; }
            set { base[ConfigurationStrings.ClientCredentialType] = value; }
        }

        [ConfigurationProperty(ConfigurationStrings.NegotiateServiceCredential, DefaultValue = true)]
        public bool NegotiateServiceCredential
        {
            get { return (bool)base[ConfigurationStrings.NegotiateServiceCredential]; }
            set { base[ConfigurationStrings.NegotiateServiceCredential] = value; }
        }

        internal void ApplyConfiguration(MessageSecurityOverHttp security)
        {
            if (security == null)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull(nameof(security));
            }

            security.ClientCredentialType = this.ClientCredentialType;
            security.NegotiateServiceCredential = this.NegotiateServiceCredential;
        }
    }
}
