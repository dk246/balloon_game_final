using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class menu_controller : MonoBehaviour
{

    public GameObject[] panels;
    private int isNew;

    public Button okay;
    public InputField inp;

    public int[] scores = new int[5];

    public Text[] scoreList;
    void Start()
    {
        
        for(int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }

        isNew = PlayerPrefs.GetInt("fresher");  // if player is new this is 0 otherwise 1

        if(isNew == 0)
        {
            panels[0].SetActive(true);
        }

        okay.interactable = false;

        for(int i = 0;i < scores.Length;i++)
        {
            scores[i] = PlayerPrefs.GetInt("score"+i.ToString());
            scoreList[i].text = (i+1).ToString()+".  " + scores[i].ToString();
        }
    }

    void Update()
    {
        if(inp.text != "")
        {
            okay.interactable=true;
        }
        else
        {
            okay.interactable=false;
        }
    }

    public void StartGame()
    {
        panels[1].SetActive(true);
    }

    public void Settings()
    {
        panels[2].SetActive(true);
    }

    public void Instructions()
    {
        panels[3].SetActive(true);
    }

    public void HighScores()
    {
        panels[4].SetActive(true);
    }
    public void closeBtn()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
    }

    public void AwakePanelClose()
    {
        PlayerPrefs.SetInt("fresher", 1);
        PlayerPrefs.SetString("name", inp.text);
        panels[0].SetActive(false);
    }

}
