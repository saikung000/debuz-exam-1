using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameManager : MonoSingleton<GameManager>
{
    private ReactiveProperty<GameState> gameState = new ReactiveProperty<GameState>(GameState.StartMenu);

    public ReactiveProperty<GameState> GetState() => gameState;
    
    public void ChangeState(GameState gameState)
    {
        this.gameState.Value = gameState;
    }
}
