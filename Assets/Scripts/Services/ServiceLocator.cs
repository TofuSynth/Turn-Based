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
            DontDestroyOnLoad(this.gameObject);
        }

        public static void Register(Type registrationType,ServiceBase service)
        {
            m_instance.m_services.Add(registrationType, service);
        }
        
        public static void Deregister(Type registrationType)
        {
            m_instance.m_services.Remove(registrationType);
        }

        public static TServiceType GetService<TServiceType>() where TServiceType : ServiceBase
        {
            if ( m_instance.m_services.ContainsKey(typeof(TServiceType)))
            {
                return m_instance.m_services[typeof(TServiceType)] as TServiceType;
            }
            return null;
        }
    }
}