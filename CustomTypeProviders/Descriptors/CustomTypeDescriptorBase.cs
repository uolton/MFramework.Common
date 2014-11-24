using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MFramework.Common.CustomTypeProviders.Descriptors
{


    public abstract class CustomTypeDescriptorBase<TType> : CustomTypeDescriptor
        where TType:class
    {
        private TType _istance;

        protected CustomTypeDescriptorBase(ICustomTypeDescriptor parent)
            : base(parent){}
        protected CustomTypeDescriptorBase(ICustomTypeDescriptor parent, TType istance) : this(parent)
        {
            _istance = istance;
        }


        public bool Configure(ICustomTypeDescriptorConfigurator cfg)
        {
            return  cfg.Configure(GetTypeDescriptorModifier());
            
        }

        #region Callback method
        /// <summary>
        /// Accede all istanza del type modifier
        /// </summary>
        /// <returns></returns>
        public abstract ICustomTypeDescriptorModifier GetTypeDescriptorModifier();    
        /// <summary>
        /// GetTypeDescriptorModifier restituisce i nuovi attributi da aggiungere 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public abstract List<Attribute> GetAttributesFor(TType instance);
        #endregion

        protected AttributeCollection GetTypeAttributes()
        {

            // instance = GetPropertyOwner(base..GetProperties().Cast<PropertyDescriptor>().First()) as TType ;
            return (base.GetAttributes());


        }  
        #region CustomTypeDescriptor Interface
        public override AttributeCollection GetAttributes()
        {
            
           // instance = GetPropertyOwner(base..GetProperties().Cast<PropertyDescriptor>().First()) as TType ;
            return new AttributeCollection(GetAttributesFor(_istance).ToArray());
            

        }  
        
        #endregion 
    }
}
