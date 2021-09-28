// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Configuration;
using System.ServiceModel;
using SD.Toolkits.CoreWCF.Client.Configurations.Commons;

namespace SD.Toolkits.CoreWCF.Client.Configurations.Bindings
{
    public class NonDualMessageSecurityOverHttpElement : MessageSecurityOverHttpElement
    {
        [ConfigurationProperty(ConfigurationStrings.EstablishSecurityContext, DefaultValue = true)]
        public bool EstablishSecurityContext
        {
            get { return (bool)base[ConfigurationStrings.EstablishSecurityContext]; }
            set { base[ConfigurationStrings.EstablishSecurityContext] = value; }
        }

        internal void ApplyConfiguration(NonDualMessageSecurityOverHttp security)
        {
            base.ApplyConfiguration(security);
            security.EstablishSecurityContext = this.EstablishSecurityContext;
        }

    }
}
