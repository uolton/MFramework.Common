namespace MFramework.Common.Factory
{
    public interface IFactory<T> where T : class
    {
        T CreateInstance();
    }
}