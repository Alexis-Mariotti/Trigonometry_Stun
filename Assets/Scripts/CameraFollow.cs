using System.Diagnostics;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;          
    public Vector3 offset = new Vector3(3f, 0f, 0f);  
    public float smoothSpeed = 5f;     

    private float initialY;            
    private float initialZ;            

    void Start()
    {
        if (player == null)
        {
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

        initialY = transform.position.y;
        initialZ = transform.position.z;  
    }

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 targetPosition = new Vector3(player.position.x + offset.x, initialY + offset.y, initialZ);

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
