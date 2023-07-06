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
    public bool isUpDown;
    public bool isDownDown;
    public bool isRightDown;
    public bool isLeftDown;
    public bool isInteractDown;
    public bool isCancelDown;
    public bool isMenuDown;

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
        }
        
    }

    private void MovementInput()
    {
        isUpDown = Input.GetKey("w");
        isDownDown = Input.GetKey("s");
        isRightDown = Input.GetKey("d");
        isLeftDown = Input.GetKey("a");
    }

    private void MenuDirectionInput()
    {
        isUpDown = Input.GetKeyDown("w");
        isDownDown = Input.GetKeyDown("s");
        isRightDown = Input.GetKeyDown("d");
        isLeftDown = Input.GetKeyDown("a");
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
        isUpDown = false;
        isDownDown = false;
        isLeftDown = false;
        isRightDown = false;
    }
}
