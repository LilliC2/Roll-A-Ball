using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedRunToggleFix : MonoBehaviour
{
    GameController gameController;
    Toggle toggle; 

    // Start is called before the first frame update
    void Start()
    {

        gameController = FindObjectOfType<GameController>();
        toggle = GetComponent<Toggle>();
        StartCoroutine(FixSpeedRunToggle());
    }

    IEnumerator FixSpeedRunToggle()
    {
        yield return new WaitForEndOfFrame();
        if (gameController.gameType == GameType.Speedrun)
            toggle.isOn = true;
        else
            toggle.isOn = false;

        toggle.onValueChanged.AddListener((value) => gameController.ToggleSpeedRun(toggle.isOn));
    }
}
