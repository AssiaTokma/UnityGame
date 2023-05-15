using PathCreation;
using PathCreation.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public static UnityEvent<int> newWave = new UnityEvent<int>();

    public GameObject enemy;
    public GameObject enemyMedium;
    public GameObject enemyHeavy;
    public GameObject enemyBoss;

    public PathCreator path;
    public int waveCount = 5;
    public int enemyWaveCount = 5;
    public float startWaitBetweenEnemySpawn = 1;
    public float intermission = 5;

    private GameObject startPosition;
    private UserStatsController usc;
    private int wave = 1;
    static public float coinsModifier = 1;

    void Start()
    {
        startPosition = GameObject.FindGameObjectWithTag("StartPosition");
        PathFollower.pathInsCreator = path;

        usc = GameObject
            .FindGameObjectWithTag("StatsController")?
            .GetComponent<UserStatsController>();

        newWave?.Invoke(0);
        StartCoroutine(AllWavesGenerator());
    }

    void Update()
    {
        if (usc == null)
            usc = GameObject
            .FindGameObjectWithTag("StatsController")?
            .GetComponent<UserStatsController>();
    }

    public static void SetModifier(float mod)
    {
        coinsModifier = mod;
    }

    public static float GetModifier()
    {
        return coinsModifier;
    }

    public static void AddModifier(float mod)
    {
        coinsModifier += mod;
        Debug.Log(coinsModifier);
    }
    public static void SubModifier(float mod)
    {
        coinsModifier = Math.Clamp(coinsModifier - mod, 1, float.MaxValue);
    }


    private IEnumerator WaveGenerator()
    {
        int colddown = 5;

        for (int i = 0; i < enemyWaveCount; i++)
        {
            yield return new WaitForSeconds(startWaitBetweenEnemySpawn);

            System.Random rndg = new System.Random();
            int rnd = rndg.Next(1, 100);

            if (rnd > 95 && rnd < 100 && colddown == 0 && wave > 2)
            {
                GameObject currentEnemy = Instantiate(enemyBoss, startPosition.transform);
                currentEnemy.GetComponent<EntityHealth>()?.AddHealth(700f * wave + (100f * wave));
                colddown += 15;
            } else if (rnd > 75 && rnd < 100)
            {
                GameObject currentEnemy = Instantiate(enemyHeavy, startPosition.transform);
                currentEnemy.GetComponent<EntityHealth>()?.AddHealth(170f * wave);
            } else if (rnd > 50 && rnd < 100)
            {
                GameObject currentEnemy = Instantiate(enemyMedium, startPosition.transform);
                currentEnemy.GetComponent<EntityHealth>()?.AddHealth(40f * wave);
            } else
            {
                GameObject currentEnemy = Instantiate(enemy, startPosition.transform);
                currentEnemy.GetComponent<EntityHealth>()?.AddHealth(20f * wave);
            }

            colddown = Math.Clamp(colddown - 1, 0, 150);
        }
        startWaitBetweenEnemySpawn = Math.Clamp(startWaitBetweenEnemySpawn / 1.2f, 0.2f, float.MaxValue);
    }

    private IEnumerator AllWavesGenerator()
    {
        yield return new WaitForSeconds(intermission / 6);
        for (int i = 0; i < waveCount; i++)
        {
            StartCoroutine(WaveGenerator());
            intermission = Math.Clamp(intermission / 1.1f, 10f, float.MaxValue);
            enemyWaveCount = Math.Clamp(enemyWaveCount + i, 1, 55);
            yield return new WaitForSeconds(intermission);
            newWave?.Invoke(i + 1);
            usc.ChangeCoinsValue((int)(Math.Clamp(4000 / (int)intermission, 25, 500) * coinsModifier));
            wave += 1;
        }
    }
}
