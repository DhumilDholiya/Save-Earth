using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingPlanetSpawner : MonoBehaviour
{

    [SerializeField]
    private Vector2 minMaxRadius = new Vector2(5f,10f);
    [SerializeField]
    private Vector2 minMaxSpeed = new Vector2(1f,3f);
    [SerializeField]
    private float spawnRate = 7f;

    private float maxSpawnRate = 5f;
    private float speed;
    private float radius;
    private float spawnTimer = 0f;
    private float timeCounter = 0f;

    private void OnEnable()
    {
        speed = Random.Range(minMaxSpeed.x, minMaxSpeed.y);
        radius = Random.Range(minMaxRadius.x,minMaxRadius.y);
    }

    private void Update()
    {
        CircularMotion();
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnRate)
        {
            spawnTimer = 0f;
            Spawn();
        }
    }


    private void Spawn()
    {
        var chargingPlanet = CharginPlanetPool.Instance.Get();
        chargingPlanet.transform.position = transform.position;
        chargingPlanet.transform.rotation = transform.rotation;
        chargingPlanet.gameObject.SetActive(true);
    }

    private void CircularMotion()
    {
        timeCounter += Time.deltaTime * speed;
        float x = Mathf.Cos(timeCounter) * radius;
        float y = Mathf.Sin(timeCounter) * radius;
        float z = 0;

        transform.position = new Vector3(x, y, z);
    }
}
