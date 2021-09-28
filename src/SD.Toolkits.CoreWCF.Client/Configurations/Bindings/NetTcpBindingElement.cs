// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SD.Toolkits.CoreWCF.Client.Configurations.Commons;
using System;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace SD.Toolkits.CoreWCF.Client.Configurations.Bindings
{
    public class NetTcpBindingElement : StandardBindingElement
    {
        public NetTcpBindingElement(string name)
            : base(name)
        {
        }

        public NetTcpBindingElement()
            : this(null)
        {
        }

        [ConfigurationProperty(ConfigurationStrings.TransactionFlow, DefaultValue = false)]
        public bool TransactionFlow
        {
            get { throw new PlatformNotSupportedException(); }
        }

        [ConfigurationProperty(ConfigurationStrings.TransferMode, DefaultValue = TransferMode.Buffered)]
        public TransferMode TransferMode
        {
            get { return (TransferMode)base[ConfigurationStrings.TransferMode]; }
            set { base[ConfigurationStrings.TransferMode] = value; }
        }

        [ConfigurationProperty(ConfigurationStrings.MaxBufferPoolSize, DefaultValue = 1L)]
        [LongValidator(MinValue = 0)]
        public long MaxBufferPoolSize
        {
            get { return (long)base[ConfigurationStrings.MaxBufferPoolSize]; }
            set { base[ConfigurationStrings.MaxBufferPoolSize] = value; }
        }

        [ConfigurationProperty(ConfigurationStrings.MaxBufferSize, DefaultValue = 65536)]
        [IntegerValidator(MinValue = 1)]
        public int MaxBufferSize
        {
            get { return (int)base[ConfigurationStrings.MaxBufferSize]; }
            set { base[ConfigurationStrings.MaxBufferSize] = value; }
        }

        [ConfigurationProperty(ConfigurationStrings.MaxConnections, DefaultValue = 1)]
        [IntegerValidator(MinValue = 0)]
        public int MaxConnections
        {
            get { return (int)base[ConfigurationStrings.MaxConnections]; }
            set { base[ConfigurationStrings.MaxConnections] = value; }
        }

        [ConfigurationProperty(ConfigurationStrings.MaxReceivedMessageSize, DefaultValue = 65536L)]
        [LongValidator(MinValue = 1)]
        public long MaxReceivedMessageSize
        {
            get { return (long)base[ConfigurationStrings.MaxReceivedMessageSize]; }
            set { base[ConfigurationStrings.MaxReceivedMessageSize] = value; }
        }

        [ConfigurationProperty(ConfigurationStrings.PortSharingEnabled, DefaultValue = false)]
        public bool PortSharingEnabled
        {
            get { throw new PlatformNotSupportedException(); }
        }

        [ConfigurationProperty(ConfigurationStrings.ReliableSession)]
        public string ReliableSession
        {
            get { throw new PlatformNotSupportedException(); }
        }

        [ConfigurationProperty(ConfigurationStrings.Security)]
        public NetTcpSecurityElement Security
        {
            get { return (NetTcpSecurityElement)base[ConfigurationStrings.Security]; }
        }

        [ConfigurationProperty(ConfigurationStrings.ReaderQuotas)]
        public XmlDictionaryReaderQuotasElement ReaderQuotas
        {
            get { return (XmlDictionaryReaderQuotasElement)base[ConfigurationStrings.ReaderQuotas]; }
        }

        public override Binding CreateBinding()
        {
            NetTcpBinding binding = new NetTcpBinding(this.Security.Mode)
            {
                Name = this.Name,
                MaxBufferPoolSize = this.MaxBufferPoolSize,
                MaxBufferSize = this.MaxBufferSize,
                MaxReceivedMessageSize = this.MaxReceivedMessageSize,
                CloseTimeout = this.CloseTimeout,
                OpenTimeout = this.OpenTimeout,
                ReceiveTimeout = this.ReceiveTimeout,
                SendTimeout = this.SendTimeout,
                ReaderQuotas = this.ReaderQuotas.Clone(),
                TransferMode = this.TransferMode
            };

            this.Security.ApplyConfiguration(binding.Security);
            return binding;
        }
    }
}
