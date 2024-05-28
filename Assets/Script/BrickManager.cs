using UnityEngine;

public class BrickManager : MonoBehaviour
{
    public GameObject brickPrefab; // Assignez le préfabriqué de la brique dans l'inspecteur
    public int rows = 3; // Nombre de lignes de briques
    public int columns = 5; // Nombre de colonnes de briques
    public float spacing = 10f; // Espacement entre les briques
    public float startX = 438f; // Position de départ en X
    public float startY = 284f; // Position de départ en Y
    public float startZ = -175f; // Position de départ en Z

    void Start()
    {
        GenerateBricks();
    }

    void GenerateBricks()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Vector3 position = new Vector3(startX + j + j * spacing, startY, startZ + i + i * spacing);
                Instantiate(brickPrefab, position, Quaternion.identity);
            }
        }
    }
}
