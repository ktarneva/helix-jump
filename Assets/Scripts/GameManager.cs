using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int best;
    public int score;

    public int currentLvl = 0;

    public static GameManager gameManager;
    public static HelixController helixController;
    private void Awake()
    {
        if (gameManager == null) 
            gameManager = this;        
         else if (gameManager != this)
            Destroy(gameObject);
        

        best = PlayerPrefs.GetInt("Highscore");

    }
    public void NextLvl()
    {
        currentLvl++;
        FindObjectOfType<BallController>().ReserBall();
        FindObjectOfType<HelixController>().LoadStage(currentLvl);
        Debug.Log("NextLvl");
    }

    public void RestartLvl()
    {
        Debug.Log("GameOver");
        gameManager.score = 0;
        FindObjectOfType<BallController>().ReserBall();
        FindObjectOfType<HelixController>().LoadStage(currentLvl);
        
    }

    public void AddScore(int addScore)
    {
        score += addScore;

        if (score > best)
            best = score;
        PlayerPrefs.SetInt("Highscore", score);
    }
}
