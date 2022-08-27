using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    //private string bestTimeStr = "Best Time";
    float currentTime = 0;
    float bestTime;
    
    bool timing = false;

    public GameObject player;

    SceneController sceneController;
    SoundController soundController;




    [Header("UI Countdown Panel")]
    public GameObject countdownPanel;
    public TextMeshProUGUI countdownText;

    [Header("UI In Game Panel")]
    public TextMeshProUGUI timerText;

    [Header("UI Game Over Panel")]
    public GameObject timesePanell;
    public TextMeshProUGUI myTimeResult;
    public TextMeshProUGUI bestTimeResult;

    // Start is called before the first frame update
    void Start()
    {
        timesePanell.SetActive(false);
        //countdownPanel.SetActive(false);
        timerText.text = "";
        sceneController = FindObjectOfType<SceneController>();
        bestTime = PlayerPrefs.GetFloat("BestTime" + sceneController.GetSceneName());
       


    }

    // Update is called once per frame
    void Update()
    {
        if(timing)
        {
            currentTime += Time.deltaTime;
            timerText.text = currentTime.ToString("F3");
        }

        
    }

    public IEnumerator StartCountdown()
    {
        
        //bestTime = PlayerPrefs.GetFloat(bestTimeStr + sceneController.GetSceneName());

        if (bestTime == 0f)bestTime = 600f;
        

        countdownPanel.SetActive(true);

        

        countdownText.text = "3";
        yield return new WaitForSeconds(1);
        countdownText.text = "2";
        yield return new WaitForSeconds(1);
        countdownText.text = "1";
        yield return new WaitForSeconds(1);
        countdownText.text = "Go";
        yield return new WaitForSeconds(1);
        StartTimer();
        countdownPanel.SetActive(false);

    }

    public void StartTimer()
    {
        currentTime = 0;
        timing = true;
    }

    public void StopTimer()
    {
        timing = false;
        timesePanell.SetActive(true);
        myTimeResult.text = currentTime.ToString("F3");
        bestTimeResult.text = bestTime.ToString("F3");

        if(currentTime <= bestTime)
        {
            bestTime = currentTime;
            PlayerPrefs.SetFloat("BestTime" + sceneController.GetSceneName(), bestTime);
            bestTimeResult.text = bestTime.ToString("F3") + " !! NEW BEST !!";
        }

    }

    public bool IsTiming()
    {
        return timing;
    }
}
