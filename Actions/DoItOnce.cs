using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using  C5;
using MFramework.Common.Core.Delegator;

using MFramework.Common.Factory;

namespace MFramework.Common.Actions
{
/// <summary>
/// DoitOnceAction classe che consente l'esecuzione di una azione una sola volta in un determinato contesto di attivazione 
/// </summary>
/// <typeparam name="TContext"></typeparam>

    public interface IDoItOnceActionContext : SingletonFactoryContexts<DelegatorAction>.IContextCreator
    {
    };
    public class DoItOnceActionClassContext<TClass> : SingletonFactoryContexts<DelegatorAction>.ClassContext<TClass>, IDoItOnceActionContext
        where TClass : class
    { }
    public class DoItOnceAction<TContext> where TContext : class,IDoItOnceActionContext, new()
{
        
        private SingletonFactory<DelegatorAction> _istance;
        public DoItOnceAction(Action action)
        {
            _istance = BuildSingleton(new TContext(), action);
        }

        private SingletonFactory<DelegatorAction> BuildSingleton(TContext contextCreator,Action act)
        {
            // Configuro il singleton con il contesto di istanziazione ed il factory composto 
            return new SingletonFactory<DelegatorAction>(contextCreator.Context
                                                            , new FactoryAction<DelegatorAction>((a) => a.Call(), new FactoryByCloning<DelegatorAction>(act.ToDelegator())));
        }
        
    }

}
