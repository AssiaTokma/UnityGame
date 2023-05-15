using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    public float damage = 25;
    public bool destroyAfterDamage = true;
    public GameObject particles;
    public string[] ignoreTagCollider = new string[] {"Turret"};

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (string tag in ignoreTagCollider)
        {
            if (other.tag == tag) return;
        }
        if (other != null)
        {
            if (particles != null)
            {
                GameObject currentParticles = Instantiate(particles, transform.position, transform.rotation);
                Destroy(currentParticles, 3f);
            }
            EntityHealth entityHealth = other.GetComponent<EntityHealth>();
            if (entityHealth != null)
                entityHealth.TakeDamage(damage);
            if (destroyAfterDamage)
            {
                Destroy(gameObject);
            }
        }
    }
}
