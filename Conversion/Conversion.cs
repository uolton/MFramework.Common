using System;
using System.Collections.Generic;
using MFramework.Common.Generics;

namespace MFramework.Common.Conversion
{
    public static class Conversion
    {
        public static TTo ChangeType<TFrom, TTo>(TFrom v1)
        {
            return((TTo) Convert.ChangeType(v1, typeof (TTo)));
        }
        public static IList<TTo> ListConvert<TFrom, TTo>(IList<TFrom> l)where TFrom:TTo
        {
            IList<TTo> x = l.ToCovariant<TFrom, TTo>();
            return(x);
        }
    }
}
