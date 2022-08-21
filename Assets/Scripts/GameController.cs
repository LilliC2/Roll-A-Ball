using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameType { Normal, Speedrun}

public class GameController : MonoBehaviour
{

    public static GameController instance;
    public GameType gameType;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //dont destroy target gameobject when loading a new scene
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //remove gameobject
            Destroy(gameObject);
        }
    }

    public void SetGameType(GameType _gameType)
    {
        gameType = _gameType;

    }

    public void ToggleSpeedRun(bool _speedRun)
    {
        if (_speedRun)
            SetGameType(GameType.Speedrun);
        else
            SetGameType(GameType.Normal);
    }
}
