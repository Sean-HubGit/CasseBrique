using UnityEngine;

public class Brick : MonoBehaviour
{
    public int level = 1; // Niveau de la brique (1, 2, ou 3)
    private int hitsRemaining; // Nombre de coups restants avant la destruction de la brique
    private GameManager gameManager; // Référence au GameManager
    public Color level1Color = Color.red; // Couleur pour le niveau 1
    public Color level2Color = Color.yellow; // Couleur pour le niveau 2
    public Color level3Color = Color.green; // Couleur pour le niveau 3
    private Renderer brickRenderer; // Référence au Renderer de la brique

    // Méthode appelée au début de la scène
    void Start()
    {
        // Initialiser le nombre de coups restants en fonction du niveau
        hitsRemaining = level;
        
        // Trouver le GameManager dans la scène
        gameManager = FindObjectOfType<GameManager>();

        // Obtenir et stocker la référence au Renderer de la brique
        brickRenderer = GetComponent<Renderer>();
        
        // Mettre à jour la couleur initiale de la brique en fonction de son niveau
        UpdateColor();
    }

    // Méthode appelée lorsque la brique est touchée par la balle
    public void TakeHit()
    {
        // Ajouter des points chaque fois qu'une brique est touchée
        gameManager.AddScore(10);
        
        // Réduire le nombre de coups restants
        hitsRemaining--;

        // Vérifier si la brique doit être détruite
        if (hitsRemaining <= 0)
        {
            Destroy(gameObject); // Détruire la brique
            gameManager.BrickDestroyed(); // Notifier le GameManager qu'une brique a été détruite
        }
        else
        {
            UpdateColor(); // Mettre à jour la couleur de la brique en fonction du niveau restant
        }
    }

    // Mettre à jour la couleur de la brique en fonction du niveau restant
    void UpdateColor()
    {
        switch (hitsRemaining)
        {
            case 1:
                brickRenderer.material.color = level1Color; // Couleur pour le niveau 1
                break;
            case 2:
                brickRenderer.material.color = level2Color; // Couleur pour le niveau 2
                break;
            case 3:
                brickRenderer.material.color = level3Color; // Couleur pour le niveau 3
                break;
            default:
                brickRenderer.material.color = level1Color; // Par défaut, couleur pour le niveau 1
                break;
        }
    }
}
