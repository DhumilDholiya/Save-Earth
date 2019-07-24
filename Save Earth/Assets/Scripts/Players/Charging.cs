using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charging : MonoBehaviour
{
    [SerializeField]
    private float chargeSpeed = 50f;
    [SerializeField]
    private float chargingTime = 4f;
    [SerializeField]
    private float chargeStopTime = 3f;
    [SerializeField]
    private GameObject planetParticles;

    private float startChargeTime;
    private Vector3 direction;
    private bool isChargedUp = false;
    private Rigidbody2D rb;
    private Transform target;
    private LineRenderer lineRend;
    private Vector3 pointOfCollision;

    private void OnEnable()
    {
        lineRend = GetComponentInChildren<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
        startChargeTime = chargingTime;
        if(GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        lineRend.SetPosition(0, transform.position);
        lineRend.SetPosition(1,transform.position);
        lineRend.enabled = false;
    }
    private void OnDisable()
    {
    }

    private void Update()
    {
       if(target)
        {
            lineRend.SetPosition(0, transform.position);
            if (startChargeTime >= 0f)
            {
                startChargeTime -= Time.deltaTime;
            }
            if (startChargeTime < 0f)
            {
                isChargedUp = true;
                lineRend.enabled = true;
                lineRend.SetPosition(1, target.position);
                startChargeTime = chargingTime;
            }
        }
    }
    private void FixedUpdate()
    {
        if(isChargedUp)
        {
            StartCoroutine(Charge());
        }
    }

    private IEnumerator Charge()
    {
        direction = (target.transform.position - transform.position).normalized;
        rb.AddForce(direction * chargeSpeed);
        isChargedUp = false;
        yield return new WaitForSeconds(.3f);
        lineRend.enabled = false;
        yield return new WaitForSeconds(chargeStopTime);
        StopCharge();
    }

    private void StopCharge()
    {
        rb.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        pointOfCollision = collision.GetContact(0).point;
        if (collision.gameObject.CompareTag("Player"))
        {
            //play death effect.\
            SpawnOnDeath();
            CharginPlanetPool.Instance.ReturnToPool(this);
        }
        if(collision.gameObject.CompareTag("Meteor"))
        {
            //sparkles effect.
            SpawnOnTouch(pointOfCollision);
        }
    }

    private void SpawnOnDeath()
    {
        Instantiate(planetParticles,transform.position,Quaternion.identity);
    }
    private void SpawnOnTouch(Vector3 pointOfContact)
    {
        
    }
}
