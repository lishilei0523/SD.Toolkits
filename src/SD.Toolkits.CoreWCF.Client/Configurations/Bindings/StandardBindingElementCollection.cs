﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Configuration;
using SD.Toolkits.CoreWCF.Client.Configurations.Commons;

namespace SD.Toolkits.CoreWCF.Client.Configurations.Bindings
{
    public class StandardBindingElementCollection<TBindingConfiguration> : ServiceModelEnhancedConfigurationElementCollection<TBindingConfiguration>
        where TBindingConfiguration : StandardBindingElement, new()
    {
        public StandardBindingElementCollection()
            : base(ConfigurationStrings.Binding)
        {
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            if (element == null)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
            }

            TBindingConfiguration configElementKey = (TBindingConfiguration)element;
            return configElementKey.Name;
        }
    }
}
