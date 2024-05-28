using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text scoreText; // Assignez le texte du score dans l'inspecteur
    public Text livesText; // Assignez le texte des vies dans l'inspecteur
    public Text endGameText; // Ajoutez un nouveau texte pour afficher la fin du jeu

    public Button rejouer; // Bouton pour rejouer
    public Button quitter; // Bouton pour quitter
    private int score = 0;
    private int totalBricks;
    private int lives = 3; // Nombre de vies

    private BallController ballController; // Référence au BallController

    void Start()
    {
        totalBricks = GameObject.FindGameObjectsWithTag("Brick").Length; // Compter le nombre initial de briques
        ballController = FindObjectOfType<BallController>(); // Trouver le BallController dans la scène

        if (scoreText == null)
        {
            Debug.LogError("Score Text is not assigned!");
        }
        if (livesText == null)
        {
            Debug.LogError("Lives Text is not assigned!");
        }
        if (endGameText == null)
        {
            Debug.LogError("End Game Text is not assigned!");
        }
        UpdateScoreText();
        UpdateLivesText();
        endGameText.gameObject.SetActive(false); // Masquer le texte de fin de partie au début
        rejouer.gameObject.SetActive(false); // Masquer le bouton rejouer au début
        quitter.gameObject.SetActive(false); // Masquer le bouton quitter au début
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

    void UpdateLivesText()
    {
        livesText.text = "Lives: " + lives;
    }

    void CheckEndGame()
    {
        if (GameObject.FindGameObjectsWithTag("Brick").Length == 2)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        endGameText.gameObject.SetActive(true);
        rejouer.gameObject.SetActive(true);
        quitter.gameObject.SetActive(true);
        endGameText.text = "Game Over! Final Score: " + score;
        SaveScoreToCSV();
        Time.timeScale = 0; // Arrêter le temps
    }

    public void PlayGame()
    {
        // Réinitialiser le temps
        Time.timeScale = 1;

        // Réinitialiser le score et les vies
        score = 0;
        lives = 3;
        UpdateScoreText();
        UpdateLivesText();

        // Réinitialiser la balle
        if (ballController != null)
        {
            ballController.ResetBall();
        }

        // Redémarrer le jeu (recharger la scène)
        SceneManager.LoadScene("Jeu");
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
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

    public void LoseLife()
    {
        lives--;
        UpdateLivesText();
        if (lives <= 0)
        {
            EndGame();
        }
        else
        {
            // Réinitialiser la balle
            if (ballController != null)
            {
                ballController.ResetBall();
            }
        }
    }
}
