using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MFramework.Common.Core.Collections.Extensions;
using MFramework.Common.Core.Types.Extensions;


namespace MFramework.Common.Assemblies.Extensions
{
    /// <summary>
    /// AssemblyExtension Extension Methods per la classe Assembly
    /// </summary>
    public static class AssemblyExtension
    {
        
        /// <summary>
        /// ClassThatImplement . Restituisce una collection delle classi che implementano /derivano da una interfaccia
        /// </summary>
        /// <param name="a">Assembly </param>
        /// <param name="t">Classe/ Interfaccia da cui derivare </param>
        /// <returns></returns>
        public static IList<Type> ClassThatImplement(this Assembly a, Type t)
        {
            return (from c in a.GetTypes()
                where c != t && c.Implement(t)  &&  ! c.IsAbstract
                    select c).ToList();

        }

        public static IList<Type> ClassThatImplement<T>(this Assembly a)
        {
            return ClassThatImplement(a,typeof (T));
        }
        public static IList<Type> ClassThatImplement(this IEnumerable<Assembly> a, Type t)
        {
            List<Type> allTypes= new List<Type>();
            (from c in a
                select c.ClassThatImplement(t)).ToList().ForEach(l => l.PushIn(allTypes));
            return allTypes;

        }
        public static IList<Type> ClassThatImplement<T>(this IEnumerable<Assembly> a)
        {
            return a.ClassThatImplement(typeof (T));

        }
    }
}
