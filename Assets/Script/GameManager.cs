using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Variables publiques pour l'interface utilisateur et les préfabriqués
    public Text scoreText; // Texte pour afficher le score
    public Text livesText; // Texte pour afficher les vies restantes
    public Text endGameText; // Texte pour afficher le message de fin de jeu
    public Text timerText; // Texte pour afficher le timer

    public Button rejouer; // Bouton pour rejouer
    public Button quitter; // Bouton pour quitter
    public GameObject brickPrefabLevel1; // Préfabriqué de la brique de niveau 1
    public GameObject brickPrefabLevel2; // Préfabriqué de la brique de niveau 2
    public GameObject brickPrefabLevel3; // Préfabriqué de la brique de niveau 3
    public GameObject brickPrefabMalus; // Préfabriqué de la brique malus
    public GameObject brickPrefabBonus; // Préfabriqué de la brique bonus

    // Variables pour la génération des briques et le jeu
    public float startX = 300f; // Position de départ en X
    public float startY = 244f; // Position de départ en Y
    public float startZ = -230f; // Position de départ en Z
    public float spacing = 30f; // Espacement entre les briques
    public float brickRadius = 1f; // Rayon utilisé pour vérifier si l'emplacement est libre

    private int score = 0; // Score actuel
    private int lives = 3; // Nombre de vies restantes
    private float timeRemaining = 300f; // Temps restant en secondes

    private BallController ballController; // Référence au BallController

    // Méthode appelée au début de la scène
    void Start()
    {
        ballController = FindObjectOfType<BallController>(); // Trouver le BallController dans la scène

        // Vérifications pour s'assurer que les éléments de l'interface utilisateur sont assignés
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

        // Initialiser les textes de l'interface utilisateur
        UpdateScoreText();
        UpdateLivesText();
        UpdateTimerText();

        // Masquer les éléments de fin de jeu au début
        endGameText.gameObject.SetActive(false);
        rejouer.gameObject.SetActive(false);
        quitter.gameObject.SetActive(false);

        // Générer quelques briques initiales
        for (int i = 0; i < 5; i++)
        {
            GenerateRandomBrick();
        }
    }

    // Méthode appelée à chaque frame
    void Update()
    {
        // Mettre à jour le timer si le jeu n'est pas terminé
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerText();

            // Terminer le jeu si le temps est écoulé
            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                EndGame();
            }
        }
    }

    // Ajouter des points au score
    public void AddScore(int value)
    {
        score += value;
        UpdateScoreText();
    }

    // Générer une nouvelle brique lorsqu'une brique est détruite
    public void BrickDestroyed()
    {
        GenerateRandomBrick();
    }

    // Mettre à jour le texte du score
    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    // Mettre à jour le texte des vies
    void UpdateLivesText()
    {
        livesText.text = "Lives: " + lives;
    }

    // Mettre à jour le texte du timer
    void UpdateTimerText()
    {
        int seconds = Mathf.FloorToInt(timeRemaining);
        timerText.text = "Time: " + seconds.ToString();
    }

    // Terminer le jeu
    void EndGame()
    {
        endGameText.gameObject.SetActive(true);
        rejouer.gameObject.SetActive(true);
        quitter.gameObject.SetActive(true);
        endGameText.text = "Game Over! Final Score: " + score;
        SaveScoreToCSV();
        Time.timeScale = 0; // Arrêter le temps
    }

    // Rejouer le jeu
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

    // Retourner au menu principal
    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    // Sauvegarder le score dans un fichier CSV
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

    // Réduire le nombre de vies et réinitialiser la balle si nécessaire
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

    // Générer une brique aléatoire à une position aléatoire
    void GenerateRandomBrick()
    {
        int randomValue = Random.Range(1, 101); // Générer un nombre aléatoire entre 1 et 100
        GameObject brickPrefab;

        // Déterminer le type de brique à générer
        if (randomValue <= 70)
        {
            // 70% de chances d'obtenir une brique normale
            int randomLevel = Random.Range(1, 4);
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
        }
        else if (randomValue <= 85)
        {
            // 15% de chances d'obtenir une brique malus
            brickPrefab = brickPrefabMalus;
        }
        else
        {
            // 15% de chances d'obtenir une brique bonus
            brickPrefab = brickPrefabBonus;
        }

        Vector3 position;
        int attempts = 0;
        bool positionFound = false;

        // Essayer de trouver une position libre pour la nouvelle brique
        do
        {
            position = new Vector3(startX + Random.Range(-4, 12) * spacing, startY, startZ + Random.Range(-1, 4) * spacing);
            positionFound = !Physics.CheckSphere(position, brickRadius);
            attempts++;
        } while (!positionFound && attempts < 100); // Limiter le nombre d'essais pour éviter une boucle infinie

        // Instancier la brique si une position valide a été trouvée
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
