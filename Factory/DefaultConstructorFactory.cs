namespace MFramework.Common.Factory
{
    public class DefaultConstructorFactory<T> : IFactory<T> where T : class,new()
    {
        public T CreateInstance()
        {
            return new T();
        }
    }
}