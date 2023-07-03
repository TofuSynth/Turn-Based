using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateService : ServiceBase<GameStateService>
{
    public enum GameState {
        Normal,
        Dialogue,
        Menu,
    }

    private GameState currentState;

    private void Start()
    {
        currentState = GameState.Normal;
    }

    public GameState GetState() {
        return currentState;
    }

    public void SetState(GameState state) {
        currentState = state;
    }

    public void DialogueState()
    {
        SetState(GameState.Dialogue);
    }

    public void MenuState()
    {
        SetState(GameState.Menu);
    }

    public void NormalState()
    {
        SetState(GameState.Normal);
    }
}
