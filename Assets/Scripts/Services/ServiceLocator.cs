using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

namespace Tofu.TurnBased.Services
{
    public class ServiceLocator : MonoBehaviour
    {
        private static ServiceLocator m_instance;
        private Dictionary<Type, ServiceBase> m_services = new Dictionary<Type, ServiceBase>();
        
        void Awake()
        {
            m_instance = this;
        }

        public static void Register<TServiceType>(TServiceType service) where TServiceType : ServiceBase
        {
            Type type = typeof(TServiceType);
            m_instance.m_services.Add(type, service); 
        }

        public static void Register(Type registrationType,ServiceBase service)
        {
            m_instance.m_services.Add(registrationType, service);
        }
        
    }
}