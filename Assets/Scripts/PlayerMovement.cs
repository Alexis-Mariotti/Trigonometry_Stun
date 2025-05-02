using System.Diagnostics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;         // Vitesse de déplacement horizontale
    public float jumpForce = 10f;        // Force de saut (mode normal)
    public float shipThrust = 10f;       // Poussée verticale (mode vaisseau)
    public float rocketBoostForce = 30f; // Force de propulsion de la fusée
    public float flyingModeSpeed = 20f;  // Vitesse horizontale en mode vol
    public float flyingAngleChange = 20f; // Changement d’angle en mode vol
    public float flyingLiftForce = 5f;   // Petite poussée vers le haut lors de l’entrée en mode vol

    public bool isShipMode = false;      // Le mode vaisseau est-il actif
    public bool isFlyingMode = false;    // Le mode vol est-il actif
    private bool isRocketActive = false; // La propulsion est-elle active
    private bool isFlyingUp = false;     // Direction actuelle du vol

    private Rigidbody2D rb;
    private bool isGrounded = true;
    private ParticleSystem rocketParticles;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rocketParticles = GetComponentInChildren<ParticleSystem>();
        if (rocketParticles) rocketParticles.Stop();
    }

    void Update()
    {
        // Basculer le mode vol avec la touche O (prioritaire sur les autres modes)
        if (Input.GetKeyDown(KeyCode.O))
        {
            SetFlyingMode(!isFlyingMode);
            if (isFlyingMode)
            {
                // Petite poussée vers le haut pour décoller du sol
                rb.linearVelocity = new Vector2(flyingModeSpeed, flyingLiftForce);
                isGrounded = false;
            }
        }

        // N’autoriser le changement de mode que si on n’est pas en mode vol
        if (!isFlyingMode)
        {
            // Basculer entre le mode normal et le mode vaisseau avec V
            if (Input.GetKeyDown(KeyCode.V))
            {
                SetShipMode(!isShipMode);
            }
        }

        if (isFlyingMode)
        {
            // Déplacement dans la direction inclinée
            Vector2 direction = transform.right.normalized;
            rb.linearVelocity = direction * flyingModeSpeed;

            // Changer la direction du vol (angle) avec la barre espace
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isFlyingUp = !isFlyingUp;
                float angle = isFlyingUp ? flyingAngleChange : -flyingAngleChange;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }

        else if (isShipMode)
        {
            // Déplacement horizontal normal
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);

            // Système de contrôle de la fusée
            if (Input.GetKey(KeyCode.Space)) // Maintenir Espace pour la propulsion
            {
                if (!isRocketActive) StartRocket();
                rb.AddForce(Vector2.up * rocketBoostForce);
            }
            else if (isRocketActive) // Relâcher Espace pour arrêter la propulsion
            {
                EndRocket();
            }

            // Rotation en fonction de la vitesse verticale
            float angle = Mathf.Clamp(rb.linearVelocity.y * 3f, -45f, 45f);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else // Mode normal
        {
            // Déplacement horizontal normal
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);

            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                isGrounded = false;
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
        }
    }

    private void StartRocket()
    {
        isRocketActive = true;
        if (rocketParticles) rocketParticles.Play();
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    private void EndRocket()
    {
        isRocketActive = false;
        if (rocketParticles) rocketParticles.Stop();
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isShipMode && !isFlyingMode && collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            transform.rotation = Quaternion.identity;
        }
    }

    public void SetShipMode(bool active)
    {
        isShipMode = active;
        rb.gravityScale = active ? 1f : 3f;
        transform.rotation = Quaternion.identity;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        if (isRocketActive) EndRocket();
    }

    public void SetFlyingMode(bool active)
    {
        isFlyingMode = active;
        if (active)
        {
            rb.gravityScale = 0f; // Pas de gravité en mode vol
            rb.linearVelocity = new Vector2(flyingModeSpeed, rb.linearVelocity.y);
            transform.rotation = Quaternion.identity;
            isFlyingUp = false;
        }
        else
        {
            rb.gravityScale = isShipMode ? 1f : 3f; // Rétablir la gravité selon le mode
        }
    }
}
