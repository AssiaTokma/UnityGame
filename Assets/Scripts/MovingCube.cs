using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class MovingCube : MonoBehaviour
{
    public int speed = 10;
    public int rotSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed * Input.GetAxis("Vertical"));
        transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * rotSpeed * Input.GetAxis("Horizontal"));
    }
}
