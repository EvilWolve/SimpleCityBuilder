using System;
using System.Collections.Generic;
using BuildingManagement;
using Configuration.Building;
using UnityEngine.Assertions;

namespace Utilities
{
    public interface IServiceLocator
    {
        T GetService<T>();
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
            
            this.InitialiseServices();
        }

        public T GetService<T>()
        {
            Type type = typeof(T);
            Assert.IsTrue(this.services.ContainsKey(type));
            
            return (T)this.services[type];
        }

        void InitialiseServices()
        {
            this.services.Add(typeof(IBuildingConfigurationService), new BuildingConfigurationService());
        }
    }
}