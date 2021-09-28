// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SD.Toolkits.CoreWCF.Client.Configurations.Commons;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace SD.Toolkits.CoreWCF.Client.Configurations.Bindings
{
    public class WSHttpBindingElement : WSHttpBindingBaseElement
    {
        public WSHttpBindingElement(string name)
        : base(name)
        {
        }

        public WSHttpBindingElement()
            : this(null)
        {
        }

        [ConfigurationProperty(ConfigurationStrings.AllowCookies, DefaultValue = false)]
        public bool AllowCookies
        {
            get { return (bool)base[ConfigurationStrings.AllowCookies]; }
            set { base[ConfigurationStrings.AllowCookies] = value; }

        }

        [ConfigurationProperty(ConfigurationStrings.Security)]
        public WSHttpSecurityElement Security
        {
            get { return (WSHttpSecurityElement)base[ConfigurationStrings.Security]; }
        }

        public override Binding CreateBinding()
        {
            WSHttpBinding binding = new WSHttpBinding(this.Security.Mode)
            {
                Name = this.Name,
                MaxBufferPoolSize = this.MaxBufferPoolSize,
                MaxReceivedMessageSize = this.MaxReceivedMessageSize,
                CloseTimeout = this.CloseTimeout,
                OpenTimeout = this.OpenTimeout,
                ReceiveTimeout = this.ReceiveTimeout,
                SendTimeout = this.SendTimeout,
                ReaderQuotas = this.ReaderQuotas.Clone()
            };

            this.Security.ApplyConfiguration(binding.Security);
            return binding;
        }
    }
}
