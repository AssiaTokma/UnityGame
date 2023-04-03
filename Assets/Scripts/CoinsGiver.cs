using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsGiver : MonoBehaviour
{

    UserStatsController usc;
    public int coinsOnDestroy = 50;

    void Start()
    {
        usc = GameObject
            .FindGameObjectWithTag("StatsController")?
            .GetComponent<UserStatsController>();
    }

    void OnDestroy()
    {
        if (usc != null)
        {
            usc.ChangeCoinsValue(coinsOnDestroy);
        }
    }
}
