// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SD.Toolkits.CoreWCF.Mobile.Configurations.Commons;
using System.Configuration;
using System.ServiceModel;

namespace SD.Toolkits.CoreWCF.Mobile.Configurations.Bindings
{
    public class BasicHttpSecurityElement : ServiceModelConfigurationElement
    {
        [ConfigurationProperty(ConfigurationStrings.Mode, DefaultValue = BasicHttpSecurityMode.None)]
        public BasicHttpSecurityMode Mode
        {
            get { return (BasicHttpSecurityMode)base[ConfigurationStrings.Mode]; }
            set { base[ConfigurationStrings.Mode] = value; }
        }

        [ConfigurationProperty(ConfigurationStrings.Transport)]
        public HttpTransportSecurityElement Transport
        {
            get { return (HttpTransportSecurityElement)base[ConfigurationStrings.Transport]; }
        }

        internal void ApplyConfiguration(BasicHttpSecurity security)
        {
            if (security == null)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull(nameof(security));
            }

            security.Mode = this.Mode;
            this.Transport.ApplyConfiguration(security.Transport);
        }
    }
}
