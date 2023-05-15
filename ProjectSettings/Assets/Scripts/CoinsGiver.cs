using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsGiver : MonoBehaviour
{

    UserStatsController usc;
    public int coinsOnDestroy = 50;
    public float modifier = 0;
    public bool isPassive = false;

    void Start()
    {
        usc = GameObject
            .FindGameObjectWithTag("StatsController")?
            .GetComponent<UserStatsController>();

        if (isPassive)
        {
            GameController.AddModifier(modifier);
        }
    }

    void OnDestroy()
    {
        if (usc != null)
        {
            usc.ChangeCoinsValue(coinsOnDestroy);
        }
    }
}
