using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageRotator : MonoBehaviour
{
    public float rotateSpeed;

    private void Update()
    {
        transform.Rotate(transform.forward, rotateSpeed * Time.deltaTime);
    }
}
