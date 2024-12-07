using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class game_controller : MonoBehaviour
{
    public Transform[] balloon_positions;
    public Transform[] enemy_positions;

    [SerializeField]private int Final_ballonCount;
    private int balloon_Count;
    public static int CurrentBalloonCount;
    public Text balloonPopText;
    public Text balloonCountText;

    public static int balloonPops;

    [SerializeField] private int delay;
    [SerializeField] private int enemy_delay;

    public GameObject balloonPrefab;
    public GameObject enemyPrefab;

    public static int score;
    public Text scoreCount;
    public Text WinMenuScoreCount;

    public GameObject failPanel;
    public GameObject winPanel;

    public Text PlayerName;

    public GameObject player;
    public int[] scores = new int[5];
    public Text[] pauseScores = new Text[5];
    void Start()
    {
        StartCoroutine("balloon_spawner");
        CurrentBalloonCount = Final_ballonCount;
        balloonPops = 3;
        StartCoroutine("enemy_spawner");
        player.SetActive(true);
        winPanel.SetActive(false);
        score = 0;
        PlayerName.text = PlayerPrefs.GetString("name");

        for(int i = 0; i < scores.Length; i++)
        {
            scores[i] = PlayerPrefs.GetInt("score" + i.ToString());
            pauseScores[i].text = (i + 1).ToString() + ".  " + scores[i].ToString();
        }
    }

    public void scoreControll()
    {
        if (score > scores[0])
        {
            scores[4] = scores[3];
            PlayerPrefs.SetInt("score4", scores[3]);
            scores[3] = scores[2];
            PlayerPrefs.SetInt("score3", scores[2]);
            scores[2] = scores[1];
            PlayerPrefs.SetInt("score2", scores[1]);
            scores[1] = scores[0];
            PlayerPrefs.SetInt("score1", scores[0]);
            scores[0] = score;
            PlayerPrefs.SetInt("score0", score);
        }
        else if (score > scores[1])
        {
            scores[4] = scores[3];
            PlayerPrefs.SetInt("score4", scores[3]);
            scores[3] = scores[2];
            PlayerPrefs.SetInt("score3", scores[2]);
            scores[2] = scores[1];
            PlayerPrefs.SetInt("score2", scores[1]);
            scores[1] = score;
            PlayerPrefs.SetInt("score1", score);
        }
        else if (score > scores[2])
        {
            scores[4] = scores[3];
            PlayerPrefs.SetInt("score4", scores[3]);
            scores[3] = scores[2];
            scores[2] = score;
            PlayerPrefs.SetInt("score2", score);
        }
        else if (score > scores[3])
        {
            scores[4] = scores[3];
            PlayerPrefs.SetInt("score4", scores[3]);
            scores[3] = score;
            PlayerPrefs.SetInt("score3", score);
        }
        else
        {
            scores[4] = score;
            PlayerPrefs.SetInt("score4", score);
        }
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < scores.Length; i++)
        {
            scores[i] = PlayerPrefs.GetInt("score" + i.ToString());
            pauseScores[i].text = (i+1).ToString()+".  "+scores[i].ToString();
        }
        if (balloonPops < 0 )
        {
            balloonPops = 0;
        }
        if (CurrentBalloonCount < 0)
        {
            CurrentBalloonCount = 0;
        }

        if(CurrentBalloonCount == 0)
        {
            winPanel.SetActive(true);
            //Time.timeScale = 0;
            this.enabled = false;
            CurrentBalloonCount = 99;
            player.SetActive(false);
        }
        if(balloonPops == 0)
        {
            failPanel.SetActive(true);
            this.enabled = false;
            player.SetActive(false);
            balloonPops = 99;
        }
     
        balloonCountText.text = "x " + CurrentBalloonCount;
        balloonPopText.text = "x " + balloonPops;
        scoreCount.text = score.ToString();
        WinMenuScoreCount.text = score.ToString();
    }

    IEnumerator balloon_spawner()
    {
        yield return new WaitForSeconds(0f);

        while (balloon_Count < Final_ballonCount)
        {
            int randomBalloon_Num, randomEnemy_num = 0;
            randomBalloon_Num = Random.Range(0, balloon_positions.Length);
            randomEnemy_num = Random.Range(0, enemy_positions.Length);


            Instantiate(balloonPrefab, new Vector3( balloon_positions[randomBalloon_Num].position.x, balloon_positions[randomBalloon_Num].position.y,1), Quaternion.identity);
            balloon_Count++;

            
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator enemy_spawner()
    {
        yield return new WaitForSeconds(0f);

        while (true)
        {
            int randomEnemy_num = 0;   
            randomEnemy_num = Random.Range(0, enemy_positions.Length);


            Instantiate(enemyPrefab, new Vector3(enemy_positions[randomEnemy_num].position.x, enemy_positions[randomEnemy_num].position.y, 1), Quaternion.identity);
            yield return new WaitForSeconds(enemy_delay);
        }
    }
}
