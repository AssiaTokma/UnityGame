using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UserStatsController : MonoBehaviour
{

    public static UnityEvent<int> coinsUpdate = new UnityEvent<int>();
    private int coins = 400;

    void Start()
    {
        coinsUpdate.Invoke(coins);
    }

    public int GetCoins()
    {
        return coins;
    }

    public void SetCoins(int coins)
    {
        this.coins = coins;
        coinsUpdate.Invoke(this.coins);
    }

    public void ChangeCoinsValue(int coins)
    {
        this.coins += coins;
        coinsUpdate.Invoke(this.coins);
    }
}
