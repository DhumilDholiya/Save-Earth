using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCube : MonoBehaviour
{
    public static event Action UpdateCollectedScore = delegate { };

    [SerializeField]
    private float lifeTime = 3f;
    [SerializeField]
    private ParticleSystem cubeParticles;

    private float lifeTimer;

    private void OnEnable()
    {
        lifeTimer = 0f;
        CubeEffect();  
    }
    private void OnDisable()
    {
    }

    private void Update()
    { 
        lifeTimer += Time.deltaTime;
        if(lifeTimer > lifeTime)
        {
            PowerCubePool.Instance.ReturnToPool(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //destroy itself with some cool effect.
            UpdateCollectedScore();
            PowerCubePool.Instance.ReturnToPool(this);
        }
    }

    private void CubeEffect()
    {
        cubeParticles.Play();
    }
}
