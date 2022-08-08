using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    public GameObject pausePanel;
    bool isPause = false;

    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPause = !isPause; //reverses bool

        if(isPause)
        {

            pausePanel.SetActive(true);
            Time.timeScale = 0; //pauses time in game?

        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1; //unpauses time in game?
        }
    }
}
