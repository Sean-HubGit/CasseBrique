using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed = 10;

    void Update()
    {
        // Lire l'entrée des touches Q et D
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
        transform.Translate(velocity);
    }
}



