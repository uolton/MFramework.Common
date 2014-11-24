using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFramework.Common.Factory
{
    /// <summary>
    /// factory: Viene eseguito il delegate passando l'istanza appena creata ad ogni creazione
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FactoryAction<T> : IFactory<T> where T : class
    {
        private IFactory<T> _factory;
        private Action<T> _action;

        public FactoryAction(Action<T> action, IFactory<T> factory)
        {
            _action = action;
            _factory = factory;
        }

        public T CreateInstance()
        {
            T instance = _factory.CreateInstance();
            _action(instance);
            return instance;
        }


    }
}
