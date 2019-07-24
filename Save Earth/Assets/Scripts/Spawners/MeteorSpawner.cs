using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField]
    private Vector2 minMaxRadius = new Vector2(9f, 15f);
    [SerializeField]
    private Vector2 minMaxSpeed = new Vector2(1f, 3f);
    [SerializeField]
    private float spawnRate = 4f;

    private float speed;
    private float radius;
    private float spawnTimer = 0f;
    private float timeCounter = 0f;
    private float pOfAbruptChangesWithPower = 0.7f;
    private float pOfAbruptChangesWithoutPower = 0.3f;

    private void OnEnable()
    {
        speed = Random.Range(minMaxSpeed.x, minMaxSpeed.y);
        radius = Random.Range(minMaxRadius.x, minMaxRadius.y);
    }
    private void Update()
    {
        CircularMotion();
        spawnTimer += Time.deltaTime;
        if(spawnTimer >= spawnRate)
        {
            spawnTimer = 0f;
            Spawn();
        }
        if (PowerButton.isPowerButtonActive)
        {
            StartCoroutine(AbruptChangesWithPower());
        }
        else
        {
            StartCoroutine(AbruptChangesWithoutPower());
        }
    }
    private IEnumerator AbruptChangesWithPower()
    {
        float p = Random.Range(0, 1);
        if (p < pOfAbruptChangesWithPower)
        {
            spawnRate = 0.3f;
            yield return new WaitForSeconds(2f);
            spawnRate = 7f;
        }
    }
    private IEnumerator AbruptChangesWithoutPower()
    {
        float p = Random.Range(0, 1);
        if (p < pOfAbruptChangesWithoutPower)
        {
            spawnRate = 1.7f;
            yield return new WaitForSeconds(6f);
            spawnRate = 7f;
        }
    }
    private void Spawn()
    {
        var meteor = MeteorPool.Instance.Get();
        meteor.transform.position = transform.position;
        meteor.transform.rotation = transform.rotation;
        meteor.gameObject.SetActive(true);
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
