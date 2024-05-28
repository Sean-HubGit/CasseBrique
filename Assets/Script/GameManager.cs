using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{
    public Text scoreText; // Assignez le texte du score dans l'inspecteur
    public Text endGameText; // Ajoutez un nouveau texte pour afficher la fin du jeu
    private int score = 0;
    private int totalBricks;

    void Start()
    {
        totalBricks = GameObject.FindGameObjectsWithTag("Brick").Length; // Compter le nombre initial de briques
        if (scoreText == null)
        {
            Debug.LogError("Score Text is not assigned!");
        }
        if (endGameText == null)
        {
            Debug.LogError("End Game Text is not assigned!");
        }
        UpdateScoreText();
        endGameText.gameObject.SetActive(false); // Masquer le texte de fin de partie au début
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateScoreText();
        CheckEndGame(); // Vérifier si le jeu est terminé après avoir ajouté le score
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    void CheckEndGame()
    {
        if (GameObject.FindGameObjectsWithTag("Brick").Length == 1)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        endGameText.gameObject.SetActive(true);
        endGameText.text = "Game Over! Final Score: " + score;
        SaveScoreToCSV();
        Time.timeScale = 0; // Arrêter le temps
    }

    void SaveScoreToCSV()
    {
        string filePath = Application.dataPath + "/scores.csv";

        // Vérifier si le fichier existe déjà
        bool fileExists = File.Exists(filePath);

        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            if (!fileExists)
            {
                // Écrire l'en-tête si le fichier est nouveau
                writer.WriteLine("Date,Score");
            }
            writer.WriteLine($"{System.DateTime.Now},{score}");
        }

        Debug.Log("Score saved to CSV: " + filePath);
    }
}
