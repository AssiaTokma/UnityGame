using PathCreation.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class BaseHealth : MonoBehaviour
{
    public static UnityEvent baseDestroyed = new UnityEvent();
    public float healthMax = 100f;
    private float health;

    void Start()
    {
        health = healthMax;
        PathFollower.pathEnded.AddListener(ChangeHealth);
    }

    void OnDestroy()
    {
        PathFollower.pathEnded.RemoveListener(ChangeHealth);
    }
    private void ChangeHealth()
    {
        TakeDamage(10);
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

    public void RegenHealth(float regen)
    {
        health += Math.Abs(regen);
        health = Math.Clamp(health, 0f, healthMax);
    }

    private void Dead()
    {
        baseDestroyed?.Invoke();
    }
}
