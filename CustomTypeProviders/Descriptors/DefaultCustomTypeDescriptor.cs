using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MFramework.Common.CustomTypeProviders.Descriptors
{
    public class DefaultCustomTypeDescriptor<TType> : CustomTypeDescriptorBase<TType>, ICustomTypeDescriptorModifier
        where TType : class
    {
        private List<Attribute> _classAttributes;

        public DefaultCustomTypeDescriptor(ICustomTypeDescriptor parent)
            : base(parent)
        {
            _classAttributes = new List<Attribute>(GetTypeAttributes().Cast<Attribute>());
        }
        public  DefaultCustomTypeDescriptor(ICustomTypeDescriptor parent, TType istance) 
            : base(parent,istance)
        {
            _classAttributes = new List<Attribute>(GetTypeAttributes().Cast<Attribute>());
        }

        public override ICustomTypeDescriptorModifier GetTypeDescriptorModifier()
        {
            return this;
        }

        public override List<Attribute> GetAttributesFor(TType instance)
        {
            return _classAttributes;
        }


        public IList<Attribute> TypeAttibutes { get { return _classAttributes; } }

        public ICustomTypeDescriptorModifier AddTypeAttribute(Attribute a)
        {
            _classAttributes.Add(a);
            return this;
        }

       
    }
}