namespace MFramework.Common.Factory
{
    public class PreallocateFactory<T,TFactory> 
        where T : class
        where TFactory:class,IFactory<T>,new()
    {
        private T _instance;
        private IFactory<T> _factory;

        public PreallocateFactory()
        {
            _factory = new TFactory();
            _instance = _factory.CreateInstance();
        }

        public T CreateInstance()
        {
            T retInstance=_instance;
            _instance = _factory.CreateInstance();
            return retInstance;
        }
    }
}