using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject shot;
    public Transform[] shotSpawns;
    public float fireRate;
    public float delay;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Fire", delay, fireRate);
    }

    // Update is called once per frame
    void Fire()
    {
        foreach (var shotSpawn in shotSpawns)
        {
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }
        
        audioSource.Play();
    }
}
