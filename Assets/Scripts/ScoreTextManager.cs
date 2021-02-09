using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreTextManager : MonoBehaviour
{
    Text text;

    public Player player;

    int score, lives;
    float bonusScore, timer;
    
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>(); // Gets the Text component from the inspector
    }

    // Update is called once per frame
    void Update()
    {
        score = player.score;
        lives = player.lives;
        bonusScore = player.bonusScore;
        timer = player.timer;
        text.text = $"SCORE {score.ToString()} " +
            $"\nLIVES {lives.ToString()}" +
            $"\nPLAYER1   LEVEL 01   BONUS {bonusScore}   TIME {timer.ToString("0")}"; // This is just a bunch of text formatting with variables, which will show them in real time inside the game

        if(timer <= 0) // Checks if the timer is under or equal to zero
        {
            SceneManager.LoadScene("Defeat"); // Loads a different scene through name
        }
        else if(lives <= 0) // If the condition above isn't true, this checks if the lives are under or equal to zero
        {
            SceneManager.LoadScene("Defeat"); // Loads a different scene through name
        }
    }
}
