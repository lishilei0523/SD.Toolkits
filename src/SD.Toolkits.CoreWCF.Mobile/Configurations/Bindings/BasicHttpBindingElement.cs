// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SD.Toolkits.CoreWCF.Mobile.Configurations.Commons;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace SD.Toolkits.CoreWCF.Mobile.Configurations.Bindings
{
    public class BasicHttpBindingElement : HttpBindingBaseElement
    {
        public BasicHttpBindingElement(string name)
            : base(name)
        {
        }

        public BasicHttpBindingElement()
            : this(null)
        {
        }

        [ConfigurationProperty(ConfigurationStrings.Security)]
        public BasicHttpSecurityElement Security
        {
            get { return (BasicHttpSecurityElement)base[ConfigurationStrings.Security]; }
        }

        public override Binding CreateBinding()
        {
            BasicHttpBinding binding = new BasicHttpBinding(this.Security.Mode)
            {
                Name = this.Name,
                MaxBufferPoolSize = this.MaxBufferPoolSize,
                MaxBufferSize = this.MaxBufferSize,
                MaxReceivedMessageSize = this.MaxReceivedMessageSize,
                CloseTimeout = this.CloseTimeout,
                OpenTimeout = this.OpenTimeout,
                ReceiveTimeout = this.ReceiveTimeout,
                SendTimeout = this.SendTimeout,
                TransferMode = this.TransferMode,
                ReaderQuotas = this.ReaderQuotas.Clone(),
            };

            this.Security.ApplyConfiguration(binding.Security);
            return binding;
        }
    }
}
