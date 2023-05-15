using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EffectTurret : MonoBehaviour
{
    public float rangeModifier = 1.15f;
    public float speedModifier = 1.25f;

    private void OnTriggerStay(Collider col)
    {
        if (col.tag == "Turret" && !col.isTrigger)
        {
            TurretScript turret = col.GetComponent<TurretScript>();
            if (turret != null)
            {
                turret.Buff(true, rangeModifier, speedModifier);
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Turret" && !col.isTrigger)
        {
            TurretScript turret = col.GetComponent<TurretScript>();
            if (turret != null)
            {
                turret.Buff(false, rangeModifier, speedModifier);
            }
        }
    }
}
