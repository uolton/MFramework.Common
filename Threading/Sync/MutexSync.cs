using System;
using System.Threading;

namespace MFramework.Common.Threading.Sync
{
    public interface IMutexSyncLock<T> : IDisposable
    {
        T Value { get; set; }
    }
    public class MutexSync<T>
    {
        private   T _data;
        private static readonly object _mutex = new object();
        private readonly object _locker = new object();
        public MutexSync(T data)
        {
            _data = data;
           
        }
        

        public IMutexSyncLock<T> Lock()
        {
            lock (_mutex)
            {
                return new MutexSyncLock( this);
            }
        }

        public void Access(Action<IMutexSyncLock<T>> a)
        {
            using (IMutexSyncLock<T> l = Lock())
            {
                a(l);
            }
        }
        
        public TReturn Execute<TReturn>(Func<T, TReturn> aFunc)
        {
            using (Lock())
            {
                return aFunc(_data);
            }
        }
        public void Execute(Action<T> aAction)
        {
            using (Lock())
            {
                aAction(_data);
            }
        }
        

        public class MutexSyncLock : IMutexSyncLock<T>
        {
            
            public MutexSync<T> _toSync;
            private bool _acquired; 
            public MutexSyncLock(MutexSync<T> toSync)
            {
                if (!_acquired)
                {
                    _toSync = toSync;
                    Monitor.Enter(_toSync._locker);
                    _acquired = true;
                }
            }
            public T Value
            {
                get
                {
                    if (!_acquired) throw new InvalidOperationException("Resource not aquired");
                    return _toSync._data;

                }
                set
                {
                    if (!_acquired) throw new InvalidOperationException("Resource not aquired");
                    _toSync._data=value;
                }
            }
            public void Dispose()
            {
                Monitor.Exit(_toSync._locker);
                _acquired = false;
            }

        }
    }
}
