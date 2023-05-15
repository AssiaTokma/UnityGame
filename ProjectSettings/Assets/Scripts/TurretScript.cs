using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{

    public GameObject turretPivot;
    public GameObject gunPart;
    public GameObject bullet;
    public ParticleSystem fireEffect;
    public ParticleSystem buffParticle;
    GameObject target;

    public float fireRate = 0.25f;

    private bool isBuffed = false;
    private SphereCollider radiusRange;
    private float basicFireRate;
    private float basicRange;

    void Start()
    {
        SphereCollider[] coliders = GetComponents<SphereCollider>();
        foreach (SphereCollider col in coliders)
        {
            if (col.enabled && col.isTrigger)
            {
                radiusRange = col;
                break;
            }
        }

        if (radiusRange != null)
        {
            basicRange = radiusRange.radius;
        }

        basicFireRate = fireRate;
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
                if (fireEffect != null)
                {
                    fireEffect.Play();
                }
                if (gunPart != null)
                    Instantiate(bullet, gunPart.transform.position, gunPart.transform.rotation);
                else
                    Instantiate(bullet, turretPivot.transform.position, turretPivot.transform.rotation);
                yield return new WaitForSeconds(fireRate);
            }
            else
            {
                yield return new WaitForSeconds(fireRate);
            }
        }
    }

    public void Buff(bool state, float range, float speed)
    {
        if (state && !isBuffed)
        {
            isBuffed = true;
            if (buffParticle != null)
            {
                buffParticle.Play();
            }

            fireRate = basicFireRate / speed;
            if (radiusRange != null)
                radiusRange.radius = basicRange * range;
        } else if (!state)
        {
            isBuffed = false;
            if (buffParticle != null)
            {
                buffParticle.Stop();
            }

            fireRate = basicFireRate;
            if (radiusRange != null)
                radiusRange.radius = basicRange;
        }
    }
}
