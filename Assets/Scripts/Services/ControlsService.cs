using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsService : ServiceBase<ControlsService>
{
    public bool isForwardDown;
    public bool isBackDown;
    public bool isRightDown;
    public bool isLeftDown;
    public bool isInteractDown;
    public bool isCancelDown;
    public bool isMenuDown;
    private void Update()
    {
        isForwardDown = Input.GetKey("w");
        isBackDown = Input.GetKey("s");
        isRightDown = Input.GetKey("d");
        isLeftDown = Input.GetKey("a");
        isInteractDown = Input.GetKey("e");
        isCancelDown = Input.GetKey("q");
        isMenuDown = Input.GetKey("tab");
    }
}
