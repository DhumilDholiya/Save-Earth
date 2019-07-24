using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public static event Action OnCollectingCube = delegate { };
    public static event Action OnPoweredUpCollision = delegate { };

    [SerializeField]
    private GameObject earthParticles;
    [SerializeField]
    private GameObject explosionParticles;
    [SerializeField]
    private ParticleSystem cubeEffect;

    private Vector3 collisonPoint;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisonPoint = collision.GetContact(0).point;
        if (!Player.isPowerdUp)
        {
            CheckForNormal(collision);
        }
        else if(Player.isPowerdUp)
        {
            CheckForPowerdUp(collision);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Power"))
        {
            OnCollectingCube();
            //add cool effect on player for collecting power cube.
            OnCollectionCube();
        }
    }

    private void CheckForNormal(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Meteor"))
        {
            //add particles effects.
            SpawnOnDestruction(collisonPoint);
            this.gameObject.SetActive(false);
            GameManager.Instance.GameOver();
        }
        if (collision.gameObject.CompareTag("Planet"))
        {
            //add particles effect.
            SpawnOnDestruction(collisonPoint);
            this.gameObject.SetActive(false);
            GameManager.Instance.GameOver();
        }
    }

    private void CheckForPowerdUp(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Meteor"))
        {
            MeteorPool.Instance.ReturnToPool(collision.gameObject.GetComponent<Meteor>());
            OnPoweredUpCollision();
        }
        if (collision.gameObject.CompareTag("Planet"))
        {
            CharginPlanetPool.Instance.ReturnToPool(collision.gameObject.GetComponent<Charging>());
            OnPoweredUpCollision();
        }
    }

    private void SpawnOnDestruction(Vector3 collisonPoint)
    {
        Instantiate(earthParticles,transform.position,Quaternion.identity);
        Instantiate(explosionParticles,collisonPoint,Quaternion.identity);
    }
    private void OnCollectionCube()
    {
        cubeEffect.Play();
    }
}
