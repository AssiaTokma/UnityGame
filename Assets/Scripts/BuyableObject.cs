using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyableObject : MonoBehaviour
{

    public int cost;
    UserStatsController usc;

    void Start()
    {
        usc = GameObject
            .FindGameObjectWithTag("StatsController")?
            .GetComponent<UserStatsController>();
    }

    public bool BuyAsset()
    {
        if (usc != null)
        {
            var currentCoins = usc.GetCoins();
            if (currentCoins >= cost)
            {
                usc.ChangeCoinsValue(-cost);
                return true;
            } else
            {
                return false;
            }
        } else
        {
            return false;
        }
    }

    public bool CanBuy()
    {
        if (usc != null)
        {
            var currentCoins = usc.GetCoins();
            if (currentCoins >= cost) return true;
            return false;
        } else
        {
            return false;
        }
    }
}
