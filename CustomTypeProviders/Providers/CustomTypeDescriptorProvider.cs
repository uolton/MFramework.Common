using System.ComponentModel;
using MFramework.Common.CustomTypeProviders.Descriptors;

namespace MFramework.Common.CustomTypeProviders.Providers
{

    public interface ICustomTypeDescriptorProviderConfigurer
    {
        void SetConfig(ICustomTypeDescriptorConfigurator cfg);
        bool RegisterProvider();
    }
    public interface ICustomTypeDescriptorConfiguratorBuilder
    {
        ICustomTypeDescriptorConfiguratorBuilder ActOn(ICustomTypeDescriptorProviderConfigurer configurer);
    }

    public abstract class CustomTypeDescriptorConfiguratorBuilderBase : ICustomTypeDescriptorConfiguratorBuilder
    
    {
        protected ICustomTypeDescriptorProviderConfigurer _provider;
        public ICustomTypeDescriptorConfiguratorBuilder ActOn(ICustomTypeDescriptorProviderConfigurer provider)
            
        {
            _provider=provider;
            return this;
        }
    }

    
    public class CustomTypeDescriptorProvider<TType>:CustomTypeDescriptorProvider<TType,DefaultCustomTypeDescriptor<TType>>
                where TType : class { }
    
    public class CustomTypeDescriptorProvider<TType,TTypeDescriptor>
        where TType:class 
        where TTypeDescriptor:CustomTypeDescriptorBase<TType>
        
    {
        internal  CustomTypeDescriptorProvider() { }
            
        
    public static TConfigurationBuilder Register<TConfigurationBuilder> ()
        where TConfigurationBuilder : class,ICustomTypeDescriptorConfiguratorBuilder, new()
        
        {
            TConfigurationBuilder b = new TConfigurationBuilder();
            return (TConfigurationBuilder ) b.ActOn(new Configurer(new CustomTypeDescriptorProvider<TType, TTypeDescriptor>()));
        }
        
        private void Register(ICustomTypeDescriptorConfigurator cfg )
        {
            TypeDescriptor.AddProvider(new DefaultCustomTypeDescriptorProvider<TTypeDescriptor, TType>(TypeDescriptor.GetProvider(typeof(TType)), cfg)
                                     , typeof(TType));  
        }

        private class Configurer : ICustomTypeDescriptorProviderConfigurer
        {
            private CustomTypeDescriptorProvider<TType, TTypeDescriptor> _provider;
            private ICustomTypeDescriptorConfigurator _cfg;
            public Configurer(CustomTypeDescriptorProvider<TType, TTypeDescriptor> provider)
            {
                _provider = provider;
            }

            public void SetConfig(ICustomTypeDescriptorConfigurator cfg)
            {
                _cfg=cfg;
            }

            public bool RegisterProvider()
            {
                _provider.Register(_cfg);
                return true;
            }
        }

    }
}