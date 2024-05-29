using UnityEngine;

public class BrickManager : MonoBehaviour
{
    public GameObject brickPrefabLevel1; // Assignez le préfabriqué de la brique de niveau 1
    public GameObject brickPrefabLevel2; // Assignez le préfabriqué de la brique de niveau 2
    public GameObject brickPrefabLevel3; // Assignez le préfabriqué de la brique de niveau 3
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
                Vector3 position = new Vector3(startX + j * spacing, startY, startZ + i * spacing);
                InstantiateRandomBrick(position);
            }
        }
    }

    void InstantiateRandomBrick(Vector3 position)
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

        Instantiate(brickPrefab, position, Quaternion.identity);
    }
}
