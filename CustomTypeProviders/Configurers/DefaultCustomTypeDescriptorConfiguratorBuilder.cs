using System;
using MFramework.Common.CustomTypeProviders.Providers;

namespace MFramework.Common.CustomTypeProviders.Configurers
{
    public class DefaultCustomTypeDescriptorConfiguratorBuilder : ICustomTypeDescriptorConfiguratorBuilder
    {
        private ICustomTypeDescriptorProviderConfigurer _configurer;
        
    
        public DefaultCustomTypeDescriptorConfiguratorBuilder()
        {
        }

        public void Configure(Func<ICustomTypeDescriptorModifier, bool> configBlock)
        {
            _configurer.SetConfig(new CustomTypeDescriptorConfigurator(configBlock));
            _configurer.RegisterProvider();
        }
        

        public ICustomTypeDescriptorConfiguratorBuilder ActOn(ICustomTypeDescriptorProviderConfigurer configurer)
        {
            _configurer=configurer;
            return this;
        }

        private class CustomTypeDescriptorConfigurator : ICustomTypeDescriptorConfigurator
        {
            private Func<ICustomTypeDescriptorModifier, bool> _configBlock;

            public CustomTypeDescriptorConfigurator(Func<ICustomTypeDescriptorModifier, bool> configBlock)
            {
                _configBlock = configBlock;
            }
            public bool Configure(ICustomTypeDescriptorModifier modifier)
            {
                return _configBlock(modifier);
            }    
        }

    }
}
