using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public GameObject pausePanel;
    public bool isPaused = false;

    private void Start()
    {
        pausePanel.SetActive(false);
        resume();
    }
    public void pause()
    {

        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0;

    }
    public void resume()
    {

        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1;

    }


}
