﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Configuration;
using System.ServiceModel;
using SD.Toolkits.CoreWCF.Client.Configurations.Commons;

namespace SD.Toolkits.CoreWCF.Client.Configurations.Bindings
{
    public class NetTcpSecurityElement : ServiceModelConfigurationElement
    {
        [ConfigurationProperty(ConfigurationStrings.Mode, DefaultValue = SecurityMode.Transport)]
        public SecurityMode Mode
        {
            get { return (SecurityMode)base[ConfigurationStrings.Mode]; }
            set { base[ConfigurationStrings.Mode] = value; }
        }

        [ConfigurationProperty(ConfigurationStrings.Transport)]
        public TcpTransportSecurityElement Transport
        {
            get { return (TcpTransportSecurityElement)base[ConfigurationStrings.Transport]; }
        }

        [ConfigurationProperty(ConfigurationStrings.Message)]
        public MessageSecurityOverTcpElement Message
        {
            get { return (MessageSecurityOverTcpElement)base[ConfigurationStrings.Message]; }
        }

        internal void ApplyConfiguration(NetTcpSecurity security)
        {
            if (security == null)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull(nameof(security));
            }

            security.Mode = this.Mode;
            this.Transport.ApplyConfiguration(security.Transport);
            this.Message.ApplyConfiguration(security.Message);
        }
    }
}
