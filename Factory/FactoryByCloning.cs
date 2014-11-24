using MFramework.Common.Core.Types;


namespace MFramework.Common.Factory
{
    public class FactoryByCloning<T> : IFactory<T> where T : class,ICloneableType<T>
    {
        private ICloneableType<T> _instance;

        public FactoryByCloning(ICloneableType<T> instance)
        {
            _instance = instance;
        }
        
        public T CreateInstance()
        {
            return _instance.Clone();
        }
    }
}