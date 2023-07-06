using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    void Start()
    {
        transform.Rotate(0, Random.value * 360, 0);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 20);
        if (transform.position.x < -2500 || transform.position.x > 2500 || transform.position.z < -2500 || transform.position.z > 2500)
        {
            transform.Rotate(0, 180, 0);
        }
    }
}
