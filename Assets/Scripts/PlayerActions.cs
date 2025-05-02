using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class PlayerActions : MonoBehaviour
{
    public ParticleSystem deathParticules;
    public Vector2 spawnCoordinates;
    public TMP_Text txtTry;
    public string lvlIdx;

    private Transform playerTransform;
    private int currentTry;

    void Start()
    {
        playerTransform = transform;

        currentTry = PlayerPrefs.GetInt($"tryMap{lvlIdx}", 0);
        if (txtTry != null)
        {
            txtTry.text = "Try : " + currentTry.ToString();
        }
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt($"tryMap{lvlIdx}", 0);
        PlayerPrefs.Save();
    }

    public void AddTry()
    {
        currentTry++;
        txtTry.text = "Try : " + currentTry.ToString();
    }

    public void Dead()
    {
        // TODO: play death sound

        Instantiate(deathParticules, transform.position, Quaternion.identity);
        // wait for particules to end and teleport to spawwn point
        StartCoroutine(waitForParticulesEndAndRespawn(deathParticules.main.duration));

        AddTry();
    }

    private IEnumerator waitForParticulesEndAndRespawn(float particuleDuration)
    {
        // disabling the player
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(particuleDuration);

        // enabling the player
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;

        respawn();
    }

    private void respawn()
    {
        // set the player at spawn point 
        playerTransform.SetPositionAndRotation(spawnCoordinates, Quaternion.identity);
    }

    // handle all player colisions 
    /**
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
    */

    private void OnCollisionStay2D(Collision2D collision)
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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            onObstacleColissionEnter(collision);
        }
    }

    // handle collisions with ground
    private void onGroundColissionEnter(Collision2D collision)
    {

        foreach (var colisionItem in collision.contacts)
        {
        //var colisionItem = collision.contacts[0];

            // left size
            if (colisionItem.normal.y <= -1.0)
            {
                Dead();
            }
            // top size
            else if (colisionItem.normal.x >= 1.0)
            {

            }
            // rigth size
            else if (colisionItem.normal.y >= 1.0)
            {

            }
            // bottom size
            else if (colisionItem.normal.x <= -1.0)
            {
                Dead();
            }
        }
    }

    // handle colisions with obstacle
    private void onObstacleColissionEnter(Collision2D collision)
    {
        foreach (var colisionItem in collision.contacts)
        {
        //var colisionItem = collision.contacts[0];
            // left size
            if (colisionItem.normal.y <= -1.0)
            {
                Dead();

            }
            // top size
            else if(colisionItem.normal.x >= 1.0)
            {
                Dead();

            }
            // rigth size
            else if(colisionItem.normal.y >= 1.0)
            {
                Dead();

            }
            // bottom size
            else if(colisionItem.normal.x <= -1.0)
            {
                Dead();

            }
        }
    }
}
