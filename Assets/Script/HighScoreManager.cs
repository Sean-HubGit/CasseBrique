using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class HighScoresManager : MonoBehaviour
{
    public Text scoresText; // Assignez le texte pour afficher les scores dans l'inspecteur
    private string filePath;

    void Start()
    {
        filePath = Application.dataPath + "/scores.csv";
        DisplayHighScores();
    }

    void DisplayHighScores()
    {
        if (!File.Exists(filePath))
        {
            scoresText.text = "No scores available.";
            return;
        }

        List<ScoreEntry> scores = new List<ScoreEntry>();
        string[] lines = File.ReadAllLines(filePath);
        for (int i = 1; i < lines.Length; i++) // Commencez à 1 pour ignorer l'en-tête
        {
            string[] parts = lines[i].Split(',');
            if (parts.Length == 2)
            {
                string date = parts[0];
                int score;
                if (int.TryParse(parts[1], out score))
                {
                    scores.Add(new ScoreEntry { Date = date, Score = score });
                }
            }
        }

        scores = scores.OrderByDescending(s => s.Score).ToList();
        scoresText.text = "Meilleurs Scores :\n";
        foreach (ScoreEntry entry in scores)
        {
            scoresText.text += entry.Date + ": " + entry.Score + "\n";
        }
    }

    public void BackToMenu()
    {
        // Chargez la scène du menu principal (Assurez-vous que le nom de la scène est correct)
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    class ScoreEntry
    {
        public string Date { get; set; }
        public int Score { get; set; }
    }
}
