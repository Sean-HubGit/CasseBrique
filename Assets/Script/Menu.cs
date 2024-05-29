using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Méthode appelée lorsqu'on clique sur le bouton "Play"
    public void PlayGame()
    {
        // Charger la scène du jeu
        SceneManager.LoadScene("Jeu");
    }

    // Méthode appelée lorsqu'on clique sur le bouton "Quit"
    public void QuitGame()
    {
        // Quitter le jeu
        #if UNITY_EDITOR
            // Si nous sommes dans l'éditeur Unity, arrêter le mode jeu
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Si nous sommes dans une build, quitter l'application
            Application.Quit();
        #endif
    }

    // Méthode appelée lorsqu'on clique sur le bouton "High Scores"
    public void OpenHighScores()
    {
        // Charger la scène des scores
        SceneManager.LoadScene("Scores");
    }
}
