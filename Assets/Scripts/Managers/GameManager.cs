using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static event Action<GameState> OnStateChanged;

    public GameState currentState;

    #region Singleton
    private void CreateInstance()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private void Awake()
    {
        CreateInstance();
    }

    void Start()
    {
        SetGameState(GameState.Tutorial);
    }
    
    void Update()
    {

    }

    public void SetGameState(GameState state)
    {
        currentState = state;
        switch (currentState)
        {
            case GameState.Tutorial:
                break;
            case GameState.Waves:
                break;
            case GameState.Cooldown:
                break;
        }

        OnStateChanged?.Invoke(currentState);
    }
}

public enum GameState
{
    Tutorial,
    Waves,
    Cooldown
}
