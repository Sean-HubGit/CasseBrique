using UnityEngine;

public class Brick : MonoBehaviour
{
    public int level = 1; // Niveau de la brique (1, 2, ou 3)
    private int hitsRemaining;
    private GameManager gameManager;
    public Color level1Color = Color.red; // Couleur pour le niveau 1
    public Color level2Color = Color.yellow; // Couleur pour le niveau 2
    public Color level3Color = Color.green; // Couleur pour le niveau 3
    private Renderer brickRenderer;

    void Start()
    {
        hitsRemaining = level; // Initialiser le nombre de coups restants en fonction du niveau
        gameManager = FindObjectOfType<GameManager>();
        brickRenderer = GetComponent<Renderer>();
        UpdateColor(); // Mettre à jour la couleur initiale de la brique
    }

    public void TakeHit()
    {
        gameManager.AddScore(10); // Ajouter des points chaque fois qu'une brique est touchée
        hitsRemaining--;
        if (hitsRemaining <= 0)
        {
            Destroy(gameObject);
            gameManager.BrickDestroyed(); // Notifier le GameManager qu'une brique a été détruite
        }
        else
        {
            UpdateColor(); // Mettre à jour la couleur de la brique en fonction du niveau
        }
    }

    void UpdateColor()
    {
        switch (hitsRemaining)
        {
            case 1:
                brickRenderer.material.color = level1Color;
                break;
            case 2:
                brickRenderer.material.color = level2Color;
                break;
            case 3:
                brickRenderer.material.color = level3Color;
                break;
            default:
                brickRenderer.material.color = level1Color; // Par défaut à la couleur du niveau 1
                break;
        }
    }
}
