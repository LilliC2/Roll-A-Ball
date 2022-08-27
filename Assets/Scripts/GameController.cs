using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameType { Normal, Speedrun}
public enum ControlType { Normal, WorldTilt}

public class GameController : MonoBehaviour
{
    //speedrun
    public static GameController instance;
    public GameType gameType;

    //world tilt
    public ControlType controlType;

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

    public void ToggleWorldTilt(bool _tilt)
    {
        if (_tilt)
            controlType = ControlType.WorldTilt;
        else
            controlType = ControlType.Normal;
            
    }

    public void ToggleSpeedRun(bool _speedRun)
    {
        if (_speedRun)
            SetGameType(GameType.Speedrun);
        else
            SetGameType(GameType.Normal);
    }
}
