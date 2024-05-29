using UnityEngine;

public class Brick : MonoBehaviour
{
    public int level = 1; // Niveau de la brique (1, 2, ou 3)
    private int hitsRemaining;
    private GameManager gameManager;

    void Start()
    {
        hitsRemaining = level; // Initialiser le nombre de coups restants en fonction du niveau
        gameManager = FindObjectOfType<GameManager>();
    }

    public void TakeHit()
    {
        gameManager.AddScore(10); // Ajouter des points chaque fois qu'une brique est touchée
        hitsRemaining--;
        if (hitsRemaining <= 0)
        {
            Destroy(gameObject);
        }
    }
}
