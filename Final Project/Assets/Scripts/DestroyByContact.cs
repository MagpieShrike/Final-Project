using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public GameObject pickup;
    public int scoreValue;
    public int speed;

    private GameController gameController;
    private PlayerController player;
    private Rigidbody rbPickup;
    private int bossHealth;

    private Vector3 direction = new Vector3(0, 0, 0);

    private void Start()
    {
        bossHealth = 10;

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent < GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.GetComponent<PlayerController>();
        }
        if (player == null)
        {
            Debug.Log("Cannot find 'PlayerController' script");
        }

        GameObject pickupObject = GameObject.FindWithTag("Pickup");
        if (pickupObject != null)
        {
            rbPickup = pickupObject.GetComponent<Rigidbody>();
        }
        if (rbPickup == null)
        {
            Debug.Log("Cannot find 'Rigidbody' script");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            return;
        }

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        if (other.CompareTag("Boss"))
        {
            Debug.Log("damage");
            bossHealth -= 1;
            Debug.Log("damage");
            Debug.Log(bossHealth);
            Destroy(gameObject);
        }

        if (bossHealth <= 0)
        {
            gameController.AddScore(scoreValue);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.CompareTag("Player"))
        {
            if (playerExplosion != null)
            {
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                gameController.GameOver();
            }
            
            Debug.Log("Pickup");

            player.Pickup();
        }

        if (playerExplosion != null)
        {
            gameController.AddScore(scoreValue);
            Destroy(other.gameObject);
        }

        Destroy(gameObject);
        Instantiate(pickup, transform.position, transform.rotation);
        rbPickup.velocity = transform.forward * speed;
    }

}
