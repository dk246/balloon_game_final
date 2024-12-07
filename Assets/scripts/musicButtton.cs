using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class musicButtton : MonoBehaviour
{
    public Image Img;
    public Sprite m_ON;
    public Sprite m_OFF;
    public int sound;

    public void muteON()
    {
        
        if (AudioListener.volume == 0)
        {
            PlayerPrefs.SetInt("mute", 1);
            AudioListener.volume = 1;
            Img.sprite = m_ON;
            
        }
        else
        {
            PlayerPrefs.SetInt("mute", 0);
            AudioListener.volume = 0;
            Img.sprite = m_OFF;

        }
        
    }

    private void Awake()
    {

   
    }

    void Start()
    {

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (AudioListener.volume == 0)
            {
                Img.sprite = m_OFF;
                
            }
            else if (AudioListener.volume == 1)
            {
                //print("soundOn");
                Img.sprite = m_ON;
          
            }
        }
        sound = PlayerPrefs.GetInt("mute");
        //print(sound);
        
       
        if (sound == 1)
        {
            AudioListener.volume = 1;
            Img.sprite = m_ON;

        }
        else if (sound == 0)
        {
            AudioListener.volume = 0;
            Img.sprite = m_OFF;
           
        }
      
        
    }
    private void Update()
    {
        print(AudioListener.volume);
    }

}
