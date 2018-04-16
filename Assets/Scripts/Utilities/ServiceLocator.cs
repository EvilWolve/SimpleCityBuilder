using System;
using System.Collections.Generic;
using Buildings;
using Buildings.Visual;
using Configuration.Building;
using UnityEngine.Assertions;

namespace Utilities
{
    public interface IServiceLocator
    {
        T GetService<T>();
        void ProvideService<T>(object service);
    }
    
    public class ServiceLocator : IServiceLocator
    {
        static IServiceLocator instance;

        readonly IDictionary<Type, object> services;

        public static IServiceLocator Instance
        {
            get
            {
                {
                    if (ServiceLocator.instance == null)
                    {
                        ServiceLocator.instance = new ServiceLocator();
                    }
                }

                return ServiceLocator.instance;
            }
        }

        ServiceLocator()
        {
            this.services = new Dictionary<Type, object>();
        }

        public T GetService<T>()
        {
            Type type = typeof(T);
            Assert.IsTrue(this.services.ContainsKey(type));
            
            return (T)this.services[type];
        }

        public void ProvideService<T>(object service)
        {
            Type type = typeof(T);
            if (this.services.ContainsKey(type))
            {
                this.services[type] = service;
            }
            else
            {
                this.services.Add(type, service);
            }
        }
    }
}