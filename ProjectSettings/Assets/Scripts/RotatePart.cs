using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePart : MonoBehaviour
{
    public GameObject rotator;
    public float speed = 20f;

    // Update is called once per frame
    void Update()
    {
        rotator.transform.Rotate(0, 0, speed * Time.deltaTime);
    }
}
