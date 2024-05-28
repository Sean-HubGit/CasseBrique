using UnityEngine;

public class BallController : MonoBehaviour
{
    public float initialSpeed = 10.0f; // Vitesse initiale de la balle
    private Rigidbody rb;
    private bool isStarted = false;
    private Transform paddleTransform;
    private GameManager gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero; // Assurez-vous que la balle est initialement immobile
        paddleTransform = GameObject.Find("Paddle").transform; // Assurez-vous que le paddle est nommé "Paddle"
        gameManager = FindObjectOfType<GameManager>(); // Trouver le GameManager dans la scène

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

    void Update()
    {
        if (!isStarted)
        {
            // Fixer la balle au paddle
            transform.position = new Vector3(paddleTransform.position.x, paddleTransform.position.y + 0.5f, paddleTransform.position.z); // Ajuster la position selon vos besoins
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Lancer la balle tout droit sur le plan Z
                rb.velocity = Vector3.forward * initialSpeed;
                isStarted = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (isStarted)
        {
            // Réapplique une vitesse constante à chaque frame
            rb.velocity = rb.velocity.normalized * initialSpeed;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            Destroy(collision.gameObject);
            gameManager.AddScore(10); // Ajouter 10 points pour chaque brique détruite
        }

        // Inverser la direction de la balle après une collision avec un angle aléatoire contrôlé
        Vector3 normal = collision.contacts[0].normal;
        Vector3 newDirection = Vector3.Reflect(rb.velocity, normal);

        // Ajouter un angle aléatoire contrôlé
        float angleVariation = Random.Range(-10f, 10f); // Ajustez l'amplitude de la variation selon vos besoins
        newDirection = Quaternion.Euler(0, angleVariation, 0) * newDirection;

        // Assurez-vous que la balle reste sur le plan XZ en annulant toute composante Y
        newDirection.y = 0;

        rb.velocity = newDirection.normalized * initialSpeed;
    }
}
