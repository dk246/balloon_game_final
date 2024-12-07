using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{

    public void restartMission()
    {
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }

    public void home()
    {
        SceneManager.LoadScene(1);
    }

    public void NextMission()
    {
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex+1);
    }

    public void Level(int num)
    {
        SceneManager.LoadScene(num);
    }
}
