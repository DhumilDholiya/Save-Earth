using UnityEngine;
using System;

public class Meteor : MonoBehaviour
{
    public static event Action UpdateMeteorScore = delegate { };

    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject meteorParticles;
    [SerializeField]
    private GameObject meteroExplosion;

    private Collider2D col;
    private float touchRadius = 0.8f;
    private Transform target;
    private Rigidbody2D rb;
    private Vector3 pointOfContact;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        col = GetComponent<Collider2D>();
    }

    private void OnDisable()
    {
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            OnTouch();
        }
    }
    private void FixedUpdate()
    {
        if (target)
        {
            Vector2 dir = (target.transform.position - transform.position).normalized;
            rb.velocity = dir * speed * Time.deltaTime;
        }
    }

    private void OnTouch()
    {
        Touch[] touches = Input.touches;
        foreach (Touch t in touches)
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(t.position);
            if (t.phase == TouchPhase.Began)
            {
                Collider2D touchedCollider = Physics2D.OverlapCircle(touchPos, touchRadius);
                if (col == touchedCollider)
                {
                    //spwan effect.
                    SpawnOnDeath();
                    SpawnOnTouch();
                    UpdateMeteorScore();
                    MeteorPool.Instance.ReturnToPool(this);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        pointOfContact = collision.GetContact(0).point;
        if (collision.gameObject.CompareTag("Player"))
        {
            //sapwn effects.
            SpawnOnDeath();
            MeteorPool.Instance.ReturnToPool(this);
        }
    }

    private void SpawnOnDeath()
    {
        Instantiate(meteorParticles, transform.position, Quaternion.identity);
    }
    private void SpawnOnTouch()
    {
        Instantiate(meteroExplosion, transform.position, Quaternion.identity);
    }
}
