using UnityEngine;

public class Brick : MonoBehaviour
{
    public int level = 1; // Niveau de la brique (1, 2, ou 3)
    private int hitsRemaining;

    void Start()
    {
        hitsRemaining = level; // Initialiser le nombre de coups restants en fonction du niveau
        Debug.Log(hitsRemaining);
    }

    public void TakeHit()
    {
        hitsRemaining--;
        if (hitsRemaining <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            // Optionnel : Vous pouvez ajouter un effet visuel pour montrer que la brique a été touchée
        }
    }
}
