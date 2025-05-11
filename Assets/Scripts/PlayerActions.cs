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

    public MainScript mainScript;

    public AudioManager audioManager;

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
        PlayerPrefs.SetInt($"tryMap{lvlIdx}", currentTry);
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

        // TODO: increase death score

        audioManager.PlayDeathSound();

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

    private void finish()
    {
        audioManager.PlayFinishSound();
        mainScript.GoHome();
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




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            onObstacleColissionEnter(collision);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGroundColissionEnter(collision);
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            finish();
        }
    }

    // handle collisions with ground
    private void onGroundColissionEnter(Collision2D collision)
    {
        /**
        float angleThreshold = 10f;
        bool shouldDie = false;
        float contactMargin = 0.2f;

        foreach (var colisionItem in collision.contacts)
        {
            //var colisionItem = collision.contacts[0];

            Vector2 localPoint = transform.InverseTransformPoint(colisionItem.point);

            // left size
            if (Vector2.Angle(colisionItem.normal, Vector2.left) < angleThreshold && localPoint.x < -contactMargin)
            {
                shouldDie = true;
            }
            // top size
            else if (Vector2.Angle(colisionItem.normal, Vector2.up) < angleThreshold && localPoint.y < -contactMargin)
            {

            }
            // rigth size
            else if (Vector2.Angle(colisionItem.normal, Vector2.right) < angleThreshold && localPoint.x > contactMargin)
            {

            }
            // bottom size
            else if (Vector2.Angle(colisionItem.normal, Vector2.down) < angleThreshold && localPoint.y > contactMargin )
            {
                shouldDie = true;
            }
        }
        // kill if should die
        if (shouldDie)
        {
            Dead();
        }
        */
    }

    // handle colisions with obstacle
    private void onObstacleColissionEnter(Collision2D collision)
    {

        Dead();


        /*
        float angleThreshold = 10f;
         
        foreach (var colisionItem in collision.contacts)
        {
            //var colisionItem = collision.contacts[0];
            // left size
            if (Vector2.Angle(colisionItem.normal, Vector2.left) < angleThreshold)
            {
                Dead();
            }
            // top size
            else if (Vector2.Angle(colisionItem.normal, Vector2.up) < angleThreshold)
            {

            }
            // rigth size
            else if (Vector2.Angle(colisionItem.normal, Vector2.right) < angleThreshold)
            {

            }
            // bottom size
            else if (Vector2.Angle(colisionItem.normal, Vector2.down) < angleThreshold)
            {
                Dead();
            }
        }
        */
    }
}
