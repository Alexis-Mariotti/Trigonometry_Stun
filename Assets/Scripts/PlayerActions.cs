using System.Collections;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public ParticleSystem deathParticules;
    public Vector2 spawnCoordinates;

    private Transform playerTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTransform = transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Dead()
    {

        // TODO: increase death score
        // TODO: play death sound


        Instantiate(deathParticules, transform.position, Quaternion.identity);
        // wait for particules to end and teleport to spawwn point
        StartCoroutine(waitForParticulesEndAndRespawn(deathParticules.main.duration));
    }

    private IEnumerator waitForParticulesEndAndRespawn(float particuleDuration)
    {
        if (gameObject.activeInHierarchy)
            gameObject.SetActive(false); // disabling the player

        yield return new WaitForSeconds(particuleDuration);

        gameObject.SetActive(true);
        Debug.Log("je suis pas la");

        // enabling the player
        gameObject.SetActive(true);
        respawn();
    }

    private void respawn()
    {
        // set the player at spawn point 
        playerTransform.SetPositionAndRotation(spawnCoordinates, Quaternion.identity);
    }

    // handle all player colisions 
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            onGroundColissionEnter(collision);
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            onObstacleColissionEnter(collision);
        }


    }

    // handle collisions with ground
    private void onGroundColissionEnter(Collision2D collision)
    {
        // Print how many points are colliding with this transform
        Debug.Log("Points colliding: " + collision.contacts.Length);
        // foreach (var colisionItem in collision.contacts)
        //{
        var colisionItem = collision.contacts[0];


            // Print the normal of the first point in the collision.
            Debug.Log("Normal of the first point: " + colisionItem.normal);
            // left size
            if (colisionItem.normal.y <= -0.99)
            {
                Dead();
            }
            // top size
            else if (colisionItem.normal.x >= 0.99)
            {

            }
            // rigth size
            else if (colisionItem.normal.y >= 0.99)
            {

            }
            // bottom size
            else if (colisionItem.normal.x <= -0.99)
            {
                Dead();
            }
        // }
    }

    // handle colisions with obstacle
    private void onObstacleColissionEnter(Collision2D collision)
    {
        //foreach (var colisionItem in collision.contacts)
        //{
        var colisionItem = collision.contacts[0];
            // left size
            if (colisionItem.normal.y <= -0.99)
            {
                Dead();

            }
            // top size
            else if(colisionItem.normal.x >= 0.99)
            {
                Dead();

            }
            // rigth size
            else if(colisionItem.normal.y >= 0.99)
            {
                Dead();

            }
            // bottom size
            else if(colisionItem.normal.x <= -0.99)
            {
                Dead();

            }
        //}
    }
}
