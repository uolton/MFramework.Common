using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFramework.Common.CustomTypeProviders.Extensions
{
    public static class AttributeCollectionExtension
    {
        public static IList<Attribute> ToList(this AttributeCollection @this)
        {
            List<Attribute> l = new List<Attribute>();
            foreach (Attribute  a in @this)
            {
                l.Add(a);
            }
            return l;
        }
    }
}
