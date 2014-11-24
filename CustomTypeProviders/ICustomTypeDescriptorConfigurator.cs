using MFramework.Common.CustomTypeProviders.Providers;

namespace MFramework.Common.CustomTypeProviders
{
    public interface ICustomTypeDescriptorConfigurator 
        
    {
        bool Configure(ICustomTypeDescriptorModifier modifier);
    }
}