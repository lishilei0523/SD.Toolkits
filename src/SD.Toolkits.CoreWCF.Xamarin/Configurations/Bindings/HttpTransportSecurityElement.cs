// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Configuration;
using System.ServiceModel;
using SD.Toolkits.CoreWCF.Xamarin.Configurations.Commons;

namespace SD.Toolkits.CoreWCF.Xamarin.Configurations.Bindings
{
    public class HttpTransportSecurityElement : ServiceModelConfigurationElement
    {
        [ConfigurationProperty(ConfigurationStrings.ClientCredentialType, DefaultValue = HttpClientCredentialType.None)]
        public HttpClientCredentialType ClientCredentialType
        {
            get { return (HttpClientCredentialType)base[ConfigurationStrings.ClientCredentialType]; }
            set { base[ConfigurationStrings.ClientCredentialType] = value; }
        }

        [ConfigurationProperty(ConfigurationStrings.Realm, DefaultValue = "")]
        [StringValidator(MinLength = 0)]
        public string Realm
        {
            get { return (string)base[ConfigurationStrings.Realm]; }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    value = String.Empty;
                }
                base[ConfigurationStrings.Realm] = value;
            }
        }

        internal void ApplyConfiguration(HttpTransportSecurity security)
        {
            if (security == null)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull(nameof(security));
            }

            security.ClientCredentialType = this.ClientCredentialType;
        }
    }
}
