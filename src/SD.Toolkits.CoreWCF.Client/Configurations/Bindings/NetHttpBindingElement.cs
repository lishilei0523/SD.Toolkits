// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SD.Toolkits.CoreWCF.Client.Configurations.Commons;
using System;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace SD.Toolkits.CoreWCF.Client.Configurations.Bindings
{
    public class NetHttpBindingElement : HttpBindingBaseElement
    {
        public NetHttpBindingElement(string name)
            : base(name)
        {
        }

        public NetHttpBindingElement()
            : this(null)
        {
        }

        [ConfigurationProperty(ConfigurationStrings.MessageEncoding, DefaultValue = NetHttpMessageEncoding.Binary)]
        public NetHttpMessageEncoding MessageEncoding
        {
            get { return (NetHttpMessageEncoding)base[ConfigurationStrings.MessageEncoding]; }
            set { base[ConfigurationStrings.MessageEncoding] = value; }
        }


        [ConfigurationProperty(ConfigurationStrings.ReliableSession)]
        public string ReliableSession
        {
            get { throw new PlatformNotSupportedException(); }
        }


        [ConfigurationProperty(ConfigurationStrings.Security)]
        public BasicHttpSecurityElement Security
        {
            get { return (BasicHttpSecurityElement)base[ConfigurationStrings.Security]; }
        }

        public override Binding CreateBinding()
        {
            NetHttpBinding binding = new NetHttpBinding(this.Security.Mode)
            {
                Name = this.Name,
                MaxBufferSize = this.MaxBufferSize,
                MaxBufferPoolSize = this.MaxBufferPoolSize,
                MaxReceivedMessageSize = this.MaxReceivedMessageSize,
                CloseTimeout = this.CloseTimeout,
                OpenTimeout = this.OpenTimeout,
                ReceiveTimeout = this.ReceiveTimeout,
                SendTimeout = this.SendTimeout,
                TransferMode = this.TransferMode,
                MessageEncoding = this.MessageEncoding,
                ReaderQuotas = this.ReaderQuotas.Clone()
            };

            binding.MessageEncoding = this.MessageEncoding;
            this.Security.ApplyConfiguration(binding.Security);
            return binding;
        }
    }
}
