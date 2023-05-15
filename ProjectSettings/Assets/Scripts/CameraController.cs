using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{

    public float speed = 30.0f;
    public float scrollSpeed = 10.0f;
    public Vector2 regionX = new Vector2(-25, 25);
    public Vector2 regionY = new Vector2(15, 45);
    public Vector2 regionZ = new Vector2(-20, 20);
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, regionY.y, 0);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float normal = Mathf.InverseLerp(regionY.x, regionY.y, transform.position.y);
        float drag = Mathf.Lerp(speed / 2, 0, normal);

        transform.Translate(Vector3.forward * Time.deltaTime * Input.GetAxis("Vertical") * (speed - drag));
        transform.Translate(Vector3.right * Time.deltaTime * Input.GetAxis("Horizontal") * (speed - drag));
        transform.Translate(Vector3.up * Time.deltaTime * Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * -55);

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, regionX.x, regionX.y),
            Mathf.Clamp(transform.position.y, regionY.x, regionY.y),
            Mathf.Clamp(transform.position.z, regionZ.x, regionZ.y)
        );
    }
}
