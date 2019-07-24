using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCubeSpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnTime = 4f;

    private float spawnTimer;
    private Vector2 bounds;

    private void OnEnable()
    {
        bounds = Camera.main.ScreenToWorldPoint(new Vector3
           (Screen.width, Screen.height, transform.position.z));
        spawnTimer = 0f;
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer > spawnTime)
        {
            spawnTimer = 0f;
            Vector2 randPosInBounds = RandPos();
            transform.position = randPosInBounds;
            Spawn();
        }
    }

    private Vector2 RandPos()
    {
        Vector2 randPos = new Vector2(Random.Range(bounds.x*-1,bounds.x),
            Random.Range(bounds.y * -1,bounds.y));
        return randPos;
    }
    private void Spawn()
    {
        var powerCube = PowerCubePool.Instance.Get();
        powerCube.transform.position = transform.position;
        powerCube.transform.rotation = transform.rotation;
        powerCube.gameObject.SetActive(true);
    }
}
