using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class HighScoresManager : MonoBehaviour
{
    public Text scoresText; // Assignez le texte pour afficher les scores dans l'inspecteur
    private string filePath; // Chemin du fichier CSV contenant les scores

    // Méthode appelée au début de la scène
    void Start()
    {
        // Définir le chemin du fichier CSV des scores
        filePath = Application.dataPath + "/scores.csv";
        // Afficher les meilleurs scores au démarrage
        DisplayHighScores();
    }

    // Méthode pour afficher les meilleurs scores
    void DisplayHighScores()
    {
        // Vérifier si le fichier de scores existe
        if (!File.Exists(filePath))
        {
            // Afficher un message si aucun score n'est disponible
            scoresText.text = "No scores available.";
            return;
        }

        // Lire tous les scores à partir du fichier CSV
        List<ScoreEntry> scores = new List<ScoreEntry>();
        string[] lines = File.ReadAllLines(filePath);
        for (int i = 1; i < lines.Length; i++) // Commencer à 1 pour ignorer l'en-tête
        {
            string[] parts = lines[i].Split(',');
            if (parts.Length == 2)
            {
                string date = parts[0];
                int score;
                if (int.TryParse(parts[1], out score))
                {
                    // Ajouter l'entrée de score à la liste
                    scores.Add(new ScoreEntry { Date = date, Score = score });
                }
            }
        }

        // Trier les scores par ordre décroissant et ne prendre que les 10 meilleurs scores
        scores = scores.OrderByDescending(s => s.Score).Take(10).ToList();
        
        // Afficher les scores dans le texte de l'interface utilisateur
        scoresText.text = "Meilleurs Scores :\n";
        foreach (ScoreEntry entry in scores)
        {
            scoresText.text += entry.Date + ": " + entry.Score + "\n";
        }
    }

    // Méthode pour retourner au menu principal
    public void BackToMenu()
    {
        // Charger la scène du menu principal (Assurez-vous que le nom de la scène est correct)
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    // Classe interne pour représenter une entrée de score
    class ScoreEntry
    {
        public string Date { get; set; } // Date du score
        public int Score { get; set; } // Valeur du score
    }
}
