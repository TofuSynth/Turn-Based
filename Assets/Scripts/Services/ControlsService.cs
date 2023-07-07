using System;
using System.Collections;
using System.Collections.Generic;
using Tofu.TurnBased.Services;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ControlsService : ServiceBase<ControlsService>
{
    private GameStateService gameState;
    [SerializeField] private GameObject gameStateService;
    public bool isForwardDown;
    public bool isBackDown;
    public bool isRightDown;
    public bool isLeftDown;
    public bool isInteractDown;
    public bool isCancelDown;
    public bool isMenuDown;
    public bool isMenuUpDown;
    public bool isMenuDownDown;
    public bool isMenuRightDown;
    public bool isMenuLeftDown;

    private void Start()
    {
        gameState = ServiceLocator.GetService<GameStateService>();
    }

    private void Update()
    {
        if (gameState.GetState() == GameStateService.GameState.Normal)
        {
            MovementInput();
            InteractInput();
            MenuOpenInput();
        }
        else if (gameState.GetState() == GameStateService.GameState.Dialogue)
        {
            InteractInput();
            StopDirectionalInput();
        }
        else if (gameState.GetState() == GameStateService.GameState.Menu)
        {
            InteractInput();
            CancelInput();
            MenuDirectionInput();
            StopDirectionalInput();
        }
        
    }

    private void MovementInput()
    {
        isForwardDown = Input.GetKey("w");
        isBackDown = Input.GetKey("s");
        isRightDown = Input.GetKey("d");
        isLeftDown = Input.GetKey("a");
    }

    private void MenuDirectionInput()
    {
        isMenuUpDown = Input.GetKeyDown("w");
        isMenuDownDown = Input.GetKeyDown("s");
        isMenuRightDown = Input.GetKeyDown("d");
        isMenuLeftDown = Input.GetKeyDown("a");
    }
    private void InteractInput()
    {
        isInteractDown = Input.GetKeyDown("e");
    }

    private void CancelInput()
    {
        isCancelDown = Input.GetKeyDown("q");
    }

    private void MenuOpenInput()
    {
        isMenuDown = Input.GetKeyDown("tab");
    }

    private void StopDirectionalInput()
    {
        isForwardDown = false;
        isBackDown = false;
        isLeftDown = false;
        isRightDown = false;
    }
}
