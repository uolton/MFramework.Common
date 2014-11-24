using System;
using System.Collections.Generic;

namespace MFramework.Common.CustomTypeProviders
{
    public interface ICustomTypeDescriptorModifier
    {
        IList<Attribute> TypeAttibutes { get; }
        ICustomTypeDescriptorModifier AddTypeAttribute(Attribute a); 
        
    }
}