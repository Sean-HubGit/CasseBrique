using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed = 10f; // Vitesse de déplacement du paddle
    public float minX = 200; // Position minimale en X pour le paddle
    public float maxX = 655; // Position maximale en X pour le paddle

    // Méthode appelée à chaque frame
    void Update()
    {
        // Lire l'entrée des touches A et D pour déplacer le paddle
        float inputX = 0;
        if (Input.GetKey(KeyCode.A))
        {
            inputX = -1; // Déplacement vers la gauche
        }
        else if (Input.GetKey(KeyCode.D))
        {
            inputX = 1; // Déplacement vers la droite
        }

        // Calculer la nouvelle position du paddle
        Vector3 velocity = Vector3.right * inputX * speed * Time.deltaTime; // Calculer la vitesse
        Vector3 newPosition = transform.position + velocity; // Calculer la nouvelle position

        // Limiter la position du paddle entre minX et maxX
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

        // Appliquer la nouvelle position au paddle
        transform.position = newPosition;
    }
}
