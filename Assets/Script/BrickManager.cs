using UnityEngine;
using System.Collections.Generic;

public class BrickManager : MonoBehaviour
{
    public GameObject brickPrefabLevel1; // Assignez le préfabriqué de la brique de niveau 1
    public GameObject brickPrefabLevel2; // Assignez le préfabriqué de la brique de niveau 2
    public GameObject brickPrefabLevel3; // Assignez le préfabriqué de la brique de niveau 3
    public int rows = 5; // Nombre de lignes de briques
    public int columns = 10; // Nombre de colonnes de briques
    public int totalBricks = 15; // Nombre total de briques à placer
    public float spacing = 10f; // Espacement entre les briques
    public float startX = 300f; // Position de départ en X
    public float startY = 244f; // Position de départ en Y
    public float startZ = -230f; // Position de départ en Z

    // Méthode appelée au début de la scène
    void Start()
    {
        GenerateBricks(); // Générer les briques à la position de départ
    }

    // Méthode pour générer les briques
    void GenerateBricks()
    {
        List<Vector3> positions = new List<Vector3>();

        // Générer toutes les positions possibles dans la grille
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Vector3 position = new Vector3(startX + j * spacing, startY, startZ + i * spacing);
                positions.Add(position); // Ajouter la position à la liste
            }
        }

        // Mélanger les positions pour les rendre aléatoires
        Shuffle(positions);

        // Placer les briques sur les premières positions mélangées
        for (int i = 0; i < totalBricks; i++)
        {
            InstantiateRandomBrick(positions[i]); // Instancier une brique à une position donnée
        }
    }

    // Méthode pour instancier une brique aléatoire à une position donnée
    void InstantiateRandomBrick(Vector3 position)
    {
        int randomLevel = Random.Range(1, 4); // Générer un nombre aléatoire entre 1 et 3 inclus
        GameObject brickPrefab;

        // Sélectionner le préfabriqué de brique en fonction du niveau aléatoire
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

        // Instancier la brique à la position donnée
        Instantiate(brickPrefab, position, Quaternion.identity);
    }

    // Méthode pour mélanger une liste (algorithme de Fisher-Yates)
    void Shuffle<T>(IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1); // Sélectionner un indice aléatoire
            T value = list[k];
            list[k] = list[n];
            list[n] = value; // Échanger les éléments
        }
    }
}
