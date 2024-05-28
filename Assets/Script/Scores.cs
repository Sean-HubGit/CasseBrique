using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScores : MonoBehaviour
{
    // public Text[] scoreTexts; // Assignez ces champs dans l'inspecteur avec les éléments Text correspondants

    // void Start()
    // {
    //     LoadHighScores();
    // }

    // void LoadHighScores()
    // {
    //     for (int i = 0; i < scoreTexts.Length; i++)
    //     {
    //         int score = PlayerPrefs.GetInt("HighScore" + i, 0);
    //         scoreTexts[i].text = "Score " + (i + 1) + ": " + score;
    //     }
    // }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

