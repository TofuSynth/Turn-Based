using System;
using System.Collections;
using System.Collections.Generic;
using Tofu.TurnBased.Services;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private ControlsService controlsService = ServiceLocator.GetService<ControlsService>();

    
    void Movement()
    {
        Vector3 inputVector = Vector3.zero;
        float forwardMovement = Convert.ToInt32(controlsService.isForwardDown) - 
                                Convert.ToInt32(controlsService.isBackDown);
        float sideMovement = Convert.ToInt32(controlsService.isRightDown) -
                             Convert.ToInt32((controlsService.isLeftDown));

    }
}
