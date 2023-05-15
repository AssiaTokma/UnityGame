using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    public float healthMax = 100f;
    private float health;

    void Start()
    {
        health = healthMax;
    }

    void Update()
    {
        
    }

    private void ChangeHealth(int damage)
    {
        TakeDamage(damage);
        Debug.Log(health);
    }

    public void TakeDamage(float damage)
    {
        health -= Math.Abs(damage);
        health = Math.Clamp(health, 0f, healthMax);
        if (health <= 0f)
        {
            Dead();
        }
    }

    public void AddHealth(float hp)
    {
        healthMax += Math.Abs(hp);
        health = healthMax;
        health += Math.Abs(hp);
    }

    public void RegenHealth(float regen)
    {
        health += Math.Abs(regen);
        health = Math.Clamp(health, 0f, healthMax);
    }

    private void Dead()
    {
        Destroy(gameObject);
    }
}
