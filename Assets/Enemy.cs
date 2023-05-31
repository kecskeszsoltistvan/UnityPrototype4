using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public PlayerController PC;
    public ParticleSystem Charging;
    public ParticleSystem Charged;
    private Rigidbody enemyRB;
    private GameObject player;

    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRB.AddForce(lookDirection * speed);
        if (transform.position.y < -5 || transform.position.y > 1)
        {
            Destroy(gameObject);
        }
    }
}
