using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenController : MonoBehaviour

{
    private void Start()
    {
        TitleScreen();
    }

    [Header("UI")]
    public GameObject TitleScreenObj;
    public GameObject LevelSelectObj;
    public GameObject MiniGameSelectObj;
    public GameObject CameraModesText;
    public void MiniGameSelect()
    {
        TitleScreenObj.SetActive(false);
        MiniGameSelectObj.SetActive(true);
    }

    public void LevelSelect()
    {
        TitleScreenObj.SetActive(false);
        LevelSelectObj.SetActive(true);
    }

    public void TitleScreen()
    {
        TitleScreenObj.SetActive(true);
        LevelSelectObj.SetActive(false);
        MiniGameSelectObj.SetActive(false);

    }
}
