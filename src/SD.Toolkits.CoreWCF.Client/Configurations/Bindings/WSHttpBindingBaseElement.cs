﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Configuration;
using SD.Toolkits.CoreWCF.Client.Configurations.Commons;

namespace SD.Toolkits.CoreWCF.Client.Configurations.Bindings
{
    public abstract class WSHttpBindingBaseElement : StandardBindingElement
    {
        protected WSHttpBindingBaseElement(string name)
            : base(name)
        {
        }

        protected WSHttpBindingBaseElement()
            : this(null)
        {
        }

        [ConfigurationProperty(ConfigurationStrings.BypassProxyOnLocal, DefaultValue = false)]
        public bool BypassProxyOnLocal
        {
            get { return (bool)base[ConfigurationStrings.BypassProxyOnLocal]; }
            set { base[ConfigurationStrings.BypassProxyOnLocal] = value; }
        }

        [ConfigurationProperty(ConfigurationStrings.TransactionFlow, DefaultValue = false)]
        public bool TransactionFlow
        {
            get { return (bool)base[ConfigurationStrings.TransactionFlow]; }
            set { base[ConfigurationStrings.TransactionFlow] = value; }
        }

        [ConfigurationProperty(ConfigurationStrings.HostNameComparisonMode, DefaultValue = HostNameComparisonMode.StrongWildcard)]

        public HostNameComparisonMode HostNameComparisonMode
        {
            get { return (HostNameComparisonMode)base[ConfigurationStrings.HostNameComparisonMode]; }
            set { base[ConfigurationStrings.HostNameComparisonMode] = value; }
        }

        [ConfigurationProperty(ConfigurationStrings.MaxBufferPoolSize, DefaultValue = 65536L)]
        public long MaxBufferPoolSize
        {
            get { return (long)base[ConfigurationStrings.MaxBufferPoolSize]; }
            set { base[ConfigurationStrings.MaxBufferPoolSize] = value; }
        }

        [ConfigurationProperty(ConfigurationStrings.MaxReceivedMessageSize, DefaultValue = 65536L)]
        [LongValidator(MinValue = 1)]
        public long MaxReceivedMessageSize
        {
            get { return (long)base[ConfigurationStrings.MaxReceivedMessageSize]; }
            set { base[ConfigurationStrings.MaxReceivedMessageSize] = value; }
        }

        [ConfigurationProperty(ConfigurationStrings.ProxyAddress, DefaultValue = null)]
        public Uri ProxyAddress
        {
            get { return (Uri)base[ConfigurationStrings.ProxyAddress]; }
            set { base[ConfigurationStrings.ProxyAddress] = value; }
        }

        [ConfigurationProperty(ConfigurationStrings.ReaderQuotas)]
        public XmlDictionaryReaderQuotasElement ReaderQuotas
        {
            get { return (XmlDictionaryReaderQuotasElement)base[ConfigurationStrings.ReaderQuotas]; }
        }

        [ConfigurationProperty(ConfigurationStrings.ReliableSession)]
        public string ReliableSession
        {
            get { throw new PlatformNotSupportedException(); }
        }

        [ConfigurationProperty(ConfigurationStrings.UseDefaultWebProxy, DefaultValue = true)]
        public bool UseDefaultWebProxy
        {
            get { return (bool)base[ConfigurationStrings.UseDefaultWebProxy]; }
            set { base[ConfigurationStrings.UseDefaultWebProxy] = value; }
        }
    }
}
