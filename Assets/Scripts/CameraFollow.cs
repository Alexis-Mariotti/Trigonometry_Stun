using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    //vecteurs de vitesse de la caméra
    public Vector3 offset = new Vector3(3f, 0f, 0f);
    //vitesse de la caméra
    public float smoothSpeed = 5f;

    private float initialZ;

    void Start()
    {
        if (player == null)
        {
            // ou qu'il est le player ???
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                enabled = false;
                return;
            }
        }

        initialZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // recupere la direction du joueur 
        Vector3 direction = player.right.normalized;  // Player's "forward" direction (local X)
        Vector3 targetPosition = player.position + direction * offset.x;

        // transformation de la direction du joueur
        targetPosition.y += offset.y;
        targetPosition.z = initialZ;

        // apu la sacade de mouvement de la caméra
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
