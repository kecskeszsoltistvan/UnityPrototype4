using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    private GameObject focalPoint;
    public ParticleSystem ParticleSystem;
    public float powerupStrength;
    public float speed = 5.0f;
    public float Despeed = 2f;
    public bool hasPowerup;
    public bool PUonGoing;
    public bool OG;
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        hasPowerup = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
        if (!hasPowerup && PUonGoing)
        {
            float forwardInput = Input.GetAxis("Vertical");
            playerRB.AddForce(focalPoint.transform.forward * speed * forwardInput);
            
        }
        else if (!hasPowerup && !PUonGoing)
        {
            StartCoroutine(PowerupCDR());
            float forwardInput = Input.GetAxis("Vertical");
            playerRB.AddForce(focalPoint.transform.forward * speed * forwardInput);
        }
        else if (!hasPowerup && !OG)
        {
            float forwardInput = Input.GetAxis("Vertical");
            playerRB.AddForce(focalPoint.transform.forward * speed * forwardInput * 4);
        }
        else
        {
            float forwardInput = Input.GetAxis("Vertical");
            playerRB.AddForce(focalPoint.transform.forward * speed * forwardInput * 2);
        }
        if (Input.GetKey(KeyCode.Space) && OG)
        {
            playerRB.velocity = Vector3.zero;
            playerRB.AddForce(0, -10, 0);
        }
    }

    IEnumerator PowerupCDR()
    {
        PUonGoing = true;
        yield return new WaitForSeconds(5);
        ParticleSystem.Play();
        hasPowerup = true;
        PUonGoing = false;


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody eRB = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            eRB.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            hasPowerup = false;
            ParticleSystem.Stop();
            playerRB.velocity = Vector3.zero;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            playerRB.AddForce(-awayFromPlayer * playerRB.velocity.magnitude * 20, ForceMode.Impulse);
        }
        if (collision.gameObject.CompareTag("Map"))
        {
            OG = true;
        }
        else
        {
            OG = false;
        }
    }
}
