using UnityEngine;

public class BallController : MonoBehaviour
{
    public float initialSpeed = 10.0f; // Vitesse initiale de la balle
    private Rigidbody rb; // Référence au Rigidbody de la balle
    private bool isStarted = false; // Indique si la balle a été lancée
    private Transform paddleTransform; // Référence au Transform du paddle
    private GameManager gameManager; // Référence au GameManager

    // Méthode appelée au début de la scène
    void Start()
    {
        // Obtenir et stocker les références nécessaires
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero; // Assurez-vous que la balle est initialement immobile

        // Trouver le Transform du paddle
        paddleTransform = GameObject.Find("Paddle").transform;

        // Trouver le GameManager dans la scène
        gameManager = FindObjectOfType<GameManager>();

        // Vérifiez si gameManager est trouvé
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene!");
        }
        else
        {
            Debug.Log("GameManager successfully found.");
        }
    }

    // Méthode appelée à chaque frame
    void Update()
    {
        // Fixer la balle au paddle avant le lancement
        if (!isStarted)
        {
            LockBallToPaddle();

            // Lancer la balle en appuyant sur la touche espace
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LaunchBall();
            }
        }
    }

    // Méthode appelée à chaque frame fixe (utilisée pour la physique)
    void FixedUpdate()
    {
        // Appliquer une vitesse constante à la balle
        if (isStarted)
        {
            MaintainConstantSpeed();
        }
    }

    // Méthode appelée lorsqu'il y a une collision avec un autre objet
    void OnCollisionEnter(Collision collision)
    {
        // Gérer les collisions avec différentes briques et la barre inférieure
        HandleCollision(collision);

        // Ajuster la direction de la balle après une collision
        AdjustBallDirection(collision);
    }

    // Réinitialiser la position et l'état de la balle
    public void ResetBall()
    {
        isStarted = false; // Indique que la balle n'a pas été lancée
        rb.velocity = Vector3.zero; // Réinitialiser la vitesse de la balle
        LockBallToPaddle(); // Fixer la balle au paddle
    }

    // Fixer la balle à la position du paddle
    private void LockBallToPaddle()
    {
        transform.position = new Vector3(paddleTransform.position.x, paddleTransform.position.y + 0.5f, paddleTransform.position.z + 5.5f);
    }

    // Lancer la balle vers l'avant
    private void LaunchBall()
    {
        rb.velocity = Vector3.forward * initialSpeed; // Appliquer la vitesse initiale à la balle
        isStarted = true; // Indique que la balle a été lancée
    }

    // Appliquer une vitesse constante à chaque frame
    private void MaintainConstantSpeed()
    {
        rb.velocity = rb.velocity.normalized * initialSpeed; // Maintenir la vitesse constante
    }

    // Gérer les collisions avec différentes briques et la barre inférieure
    private void HandleCollision(Collision collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            ProcessBrickCollision(collision.gameObject, 10); // Ajouter 10 points pour une brique normale
        }
        else if (collision.gameObject.CompareTag("Malus"))
        {
            ProcessBrickCollision(collision.gameObject, -20); // Enlever 20 points pour une brique malus
        }
        else if (collision.gameObject.CompareTag("Bonus"))
        {
            ProcessBrickCollision(collision.gameObject, 20); // Ajouter 20 points pour une brique bonus
        }
        else if (collision.gameObject.CompareTag("BottomBar"))
        {
            gameManager.LoseLife(); // Perdre une vie lorsque la balle touche la barre du bas
        }
    }

    // Traiter la collision avec une brique et ajuster le score
    private void ProcessBrickCollision(GameObject brickObject, int scoreChange)
    {
        Brick brick = brickObject.GetComponent<Brick>(); // Obtenir le script Brick attaché à l'objet
        if (brick != null)
        {
            gameManager.AddScore(scoreChange); // Ajouter ou enlever des points en fonction du type de brique
            brick.TakeHit(); // Appeler la méthode TakeHit() sur la brique
        }
    }

    // Ajuster la direction de la balle après une collision avec un angle aléatoire contrôlé
    private void AdjustBallDirection(Collision collision)
    {
        Vector3 normal = collision.contacts[0].normal; // Obtenir la normale de la surface de collision
        Vector3 newDirection = Vector3.Reflect(rb.velocity, normal); // Calculer la nouvelle direction en reflétant la vitesse actuelle

        // Ajouter un angle aléatoire contrôlé
        float angleVariation = Random.Range(-10f, 10f); // Ajustez l'amplitude de la variation selon vos besoins
        newDirection = Quaternion.Euler(0, angleVariation, 0) * newDirection;

        // Assurez-vous que la balle reste sur le plan XZ en annulant toute composante Y
        newDirection.y = 0;

        rb.velocity = newDirection.normalized * initialSpeed; // Appliquer la nouvelle direction et maintenir la vitesse constante
    }
}
