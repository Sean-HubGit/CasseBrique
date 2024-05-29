using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed = 10f;
    public float minX = 200; // Position minimale en X pour le paddle
    public float maxX = 655; // Position maximale en X pour le paddle

    void Update()
    {
        // Lire l'entrée des touches A et D
        float inputX = 0;
        if (Input.GetKey(KeyCode.A))
        {
            inputX = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            inputX = 1;
        }

        // Calculer la nouvelle position du paddle
        Vector3 velocity = Vector3.right * inputX * speed * Time.deltaTime;
        Vector3 newPosition = transform.position + velocity;

        // Limiter la position du paddle entre minX et maxX
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

        // Appliquer la nouvelle position au paddle
        transform.position = newPosition;
    }
}
