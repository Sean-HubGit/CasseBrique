using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text scoreText; // Assignez le texte du score dans l'inspecteur
    public Text livesText; // Assignez le texte des vies dans l'inspecteur
    public Text endGameText; // Ajoutez un nouveau texte pour afficher la fin du jeu
    public Text timerText; // Assignez le texte du timer dans l'inspecteur

    public Button rejouer; // Bouton pour rejouer
    public Button quitter; // Bouton pour quitter
    public GameObject brickPrefabLevel1; // Assignez le préfabriqué de la brique de niveau 1
    public GameObject brickPrefabLevel2; // Assignez le préfabriqué de la brique de niveau 2
    public GameObject brickPrefabLevel3; // Assignez le préfabriqué de la brique de niveau 3

    public float startX = 300f; // Position de départ en X
    public float startY = 244f; // Position de départ en Y
    public float startZ = -230f; // Position de départ en Z
    public float spacing = 30f; // Espacement entre les briques
    public float brickRadius = 1f; // Rayon utilisé pour vérifier si l'emplacement est libre

    private int score = 0;
    private int lives = 3; // Nombre de vies
    private float timeRemaining = 300f; // Temps restant en secondes

    private BallController ballController; // Référence au BallController

    void Start()
    {
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
        if (timerText == null)
        {
            Debug.LogError("Timer Text is not assigned!");
        }
        if (brickPrefabLevel1 == null || brickPrefabLevel2 == null || brickPrefabLevel3 == null)
        {
            Debug.LogError("Brick prefabs are not assigned!");
        }

        UpdateScoreText();
        UpdateLivesText();
        UpdateTimerText();

        endGameText.gameObject.SetActive(false); // Masquer le texte de fin de partie au début
        rejouer.gameObject.SetActive(false); // Masquer le bouton rejouer au début
        quitter.gameObject.SetActive(false); // Masquer le bouton quitter au début

        // Générer quelques briques initiales
        for (int i = 0; i < 5; i++)
        {
            GenerateRandomBrick();
        }
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerText();

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                EndGame();
            }
        }
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateScoreText();
        GenerateRandomBrick(); // Générer une nouvelle brique chaque fois qu'une brique est cassée
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    void UpdateLivesText()
    {
        livesText.text = "Lives: " + lives;
    }

    void UpdateTimerText()
    {
        int seconds = Mathf.FloorToInt(timeRemaining);
        timerText.text = "Time: " + seconds.ToString();
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
        timeRemaining = 300f; // Réinitialiser le timer
        UpdateScoreText();
        UpdateLivesText();
        UpdateTimerText();

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

    void GenerateRandomBrick()
    {
        int randomLevel = Random.Range(1, 4); // Générer un nombre aléatoire entre 1 et 3 inclus
        GameObject brickPrefab;

        switch (randomLevel)
        {
            case 1:
                brickPrefab = brickPrefabLevel1;
                break;
            case 2:
                brickPrefab = brickPrefabLevel2;
                break;
            case 3:
                brickPrefab = brickPrefabLevel3;
                break;
            default:
                brickPrefab = brickPrefabLevel1;
                break;
        }

        Vector3 position;
        int attempts = 0;
        bool positionFound = false;

        do
        {
            position = new Vector3(startX + Random.Range(-4, 12) * spacing, startY, startZ + Random.Range(-1, 4) * spacing);
            positionFound = !Physics.CheckSphere(position, brickRadius);
            attempts++;
        } while (!positionFound && attempts < 1000); // Limiter le nombre d'essais pour éviter une boucle infinie

        if (positionFound)
        {
            Instantiate(brickPrefab, position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Could not find a valid position to spawn a new brick.");
        }
    }
}
