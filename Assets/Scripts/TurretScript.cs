using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{

    public GameObject turretPivot;
    public GameObject bullet;
    GameObject target;

    public float fireRate = 0.25f;

    void Start()
    {
        StartCoroutine(Shooting());
    }

    void Update()
    {
        if (target != null)
        {
            turretPivot.transform.LookAt(target.transform);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        foreach (Collider col in other.GetComponentsInChildren<Collider>())
        {
            if (col.tag == "Enemy")
            {
                target = col.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            target = null;
        }
    }

    private IEnumerator Shooting()
    {
        while (true)
        {
            if (target != null)
            {
                Instantiate(bullet, turretPivot.transform.position, turretPivot.transform.rotation);
                yield return new WaitForSeconds(fireRate);
            }
            else
            {
                yield return new WaitForSeconds(fireRate);
            }
        }
    }
}
