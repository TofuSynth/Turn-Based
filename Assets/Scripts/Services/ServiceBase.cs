using System;
using System.Collections;
using System.Collections.Generic;
using Tofu.TurnBased.Services;
using UnityEngine;

public class ServiceBase : MonoBehaviour
{
    
}

public class ServiceBase<TRegistrationType> : ServiceBase where TRegistrationType : ServiceBase
{
    private void Awake()
    {
        ServiceLocator.Register(typeof(TRegistrationType), this);
    }

    private void OnDestroy()
    {
        ServiceLocator.Deregister(typeof(TRegistrationType));
    }
}
