﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}


public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public Transform[] pickupShotSpawns;
    public float fireRate;

    public AudioSource shotsFired;

    private Rigidbody rb;
    private float nextFire;

    private bool pickup;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            // GameObject clone =
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation); // as GameObject
            

            if (pickup == true)
            {
                Debug.Log("pickup true");
                foreach (var shotSpawns in pickupShotSpawns)
                {
                    Instantiate(shot, shotSpawns.position, shotSpawns.rotation);
                }
            }

            shotsFired.Play();
        }

        if (pickup == true)
        {
            Debug.Log("pickup true");
            if (Input.GetButton("Fire1") && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;

                foreach (var shotSpawns in pickupShotSpawns)
                {
                    Instantiate(shot, shotSpawns.position, shotSpawns.rotation);
                }
                shotsFired.Play();
            }
        }
        
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;

        rb.position = new Vector3
        (
             Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
             0.0f,
             Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }

    public void Pickup()
    {
        pickup = true;

        Debug.Log("Activate Pickup");
    }
}
