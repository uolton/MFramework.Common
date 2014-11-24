using System;
using System.ComponentModel;
using Fasterflect;
using MFramework.Common.CustomTypeProviders.Descriptors;

namespace MFramework.Common.CustomTypeProviders.Providers
{
    internal class DefaultCustomTypeDescriptorProvider<TTypeDescriptor,TType> : TypeDescriptionProvider
        where TType : class
        where TTypeDescriptor:CustomTypeDescriptorBase<TType>
    {
        private ICustomTypeDescriptorConfigurator _cfg;

        public DefaultCustomTypeDescriptorProvider(TypeDescriptionProvider parent,
            ICustomTypeDescriptorConfigurator cfg) : base(parent)
        {
            _cfg = cfg;
        }
        
        /// <summary>
        /// InstantiateTypeDescriptor Crea una nuova istanza del Type Descriptor 
        /// </summary>
        /// <param name="objectType"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        private TTypeDescriptor InstantiateTypeDescriptor(Type objectType)
        {

            return (TTypeDescriptor) typeof (TTypeDescriptor).CreateInstance(base.GetTypeDescriptor(objectType,null));
        }

        private TTypeDescriptor InstantiateTypeDescriptor(Type objectType, object instance)
        {

            return (TTypeDescriptor)typeof(TTypeDescriptor).CreateInstance(base.GetTypeDescriptor(objectType, instance), instance);
        }
        #region TypeDescriptionProvider Interface

        /// <summary>
        /// GetTypeDescriptor : CallBack method 
        /// </summary>
        /// <param name="objectType"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        
        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType,object instance)  
        {
            // Create a new istance of type descriptor 
            TTypeDescriptor d = instance == null ? InstantiateTypeDescriptor(objectType):InstantiateTypeDescriptor(objectType, instance);
            // Configure the istance
            d.Configure(_cfg);
            return d;
        }

        #endregion TypeDescriptionProvider Interface
    }
}