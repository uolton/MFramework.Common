using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MFramework.Common.Threading.Sync;

namespace MFramework.Common.Factory
{

    /// <summary>
    /// Static Class Factory di elencazione e creazione dei possibili contesti di instanziazione del singleton
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class SingletonFactoryContexts<TInstance>
     where TInstance : class
    {
        public interface IContextCreator
        {
            ISingletonContext Context { get; }
        }

        
        public class ClassContext<TClass> : IContextCreator
            where TClass :class
        {
            
            public ISingletonContext Context
            {
                get { return CreateContext(); } 
            }

            public static ISingletonContext CreateContext() 
            {
                return CreateClassContext<TClass>();
            }
        }
        /// <summary>
        /// Creazione di un oggetto context di tipo classe
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <returns></returns>
        private  static ISingletonContext CreateClassContext<TClass>() where TClass : class
        {
            return SingletonFactoryContextClass<TInstance>.CreateContext<TClass>() ;
        }
        /// <summary>
        /// Creazione di un oggetto context di tipo  classe 
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <returns></returns>
        private  static ISingletonContext CreateClassContext<TClass>(Type classType)
            
        {
            return SingletonFactoryContextClass<TInstance>.CreateContext(classType) ;
        }

        /// <summary>
        /// Contesto di istanziazione del singleton definito dalla classe di appartenenza del singleton
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TClass"></typeparam>
        public  class SingletonFactoryContextClass<TInstance>: SingletonFactory<TInstance>.InstancerContextBase
            where TInstance : class
        {
            private Type _classType;
            private  SingletonFactoryContextClass(Type classType)
             {
                 _classType = classType;
             }
            protected override string GetKey<T>(SingletonFactory<T> s)
            {
                return (_classType.FullName);
            }

            public static SingletonFactoryContextClass<TInstance> CreateContext<TClass>() where TClass:class
            {
                return CreateContext(typeof (TClass));
            }
            public static SingletonFactoryContextClass<TInstance> CreateContext(Type classType)
            {
                return new SingletonFactoryContextClass<TInstance>(classType);
            }
        }
        
    
    }
        
     /// <summary>
     /// Interfaccia generica implementata dai contesti di istanziazione del singleton
     /// </summary>
   
     public interface ISingletonContext
     {
         T Instance<T>(SingletonFactory<T> s)
             where T: class;
             

         bool IsRegistred<T>(SingletonFactory<T> s)
             where T : class;

         bool RegistrerOn<T>(SingletonFactory<T> s)
             where T: class;


     }

    /// <summary>
    /// Singleton : factory di instanziazione degli oggetti dove viene creato un singolo oggetto per ciascun contesto  
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonFactory<T>:IFactory<T> where T: class
     {
         private static volatile MutexSync<Dictionary<string, T>> _registry =
             new MutexSync<Dictionary<string, T>>(new Dictionary<string, T>());

         private IFactory<T> _factory;
         private ISingletonContext _context;

         public SingletonFactory(ISingletonContext context, IFactory<T> f)
         {
             _context = context;
             _factory = f;
             Instantiate(this);
         }

         private  static bool ContextRegistred(string contextKey)
         {
             return _registry.Execute(r => r.ContainsKey(contextKey));
         }

         private static void NewContext(string contextKey,IFactory<T> factory)
         {
             _registry.Execute(r => r.Add(contextKey, factory.CreateInstance()));
         }

         private static T Value(string contextKey)
         {
             return (_registry.Execute(r => r[contextKey]));
         }

         public T CreateInstance()
         {
             return _context.Instance(this);
         } 

         private static void Instantiate(SingletonFactory<T> handler)
         {

             if (!(handler._context).IsRegistred(handler))
             {
                 
                 handler._context.RegistrerOn(handler);
             }
         }
         public   abstract class InstancerContextBase : ISingletonContext
         {
             protected abstract string GetKey<T>(SingletonFactory<T> s)
                 where T : class;

             public bool IsRegistred<T>(SingletonFactory<T> s)
                 where T : class
             {
                 return SingletonFactory<T>.ContextRegistred(GetKey(s));
             }

             public bool RegistrerOn<T>(SingletonFactory<T> s)
                 where T : class
            {
                if (!IsRegistred(s)) SingletonFactory<T>.NewContext(GetKey(s),s._factory);
                return false;
            }

             public T Instance<T>(SingletonFactory<T> s)
                 where T : class
             {
                 return IsRegistred(s) ? SingletonFactory<T>.Value(GetKey(s)) : null;
             }
         }

     }
    }


