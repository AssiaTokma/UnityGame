using PathCreation;
using PathCreation.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public static UnityEvent<int> newWave = new UnityEvent<int>();

    public GameObject enemy;
    public GameObject enemyMedium;
    public GameObject enemyHeavy;

    public PathCreator path;
    public int waveCount = 5;
    public int enemyWaveCount = 5;
    public float startWaitBetweenEnemySpawn = 1;
    public float intermission = 5;

    private GameObject startPosition;

    void Start()
    {
        startPosition = GameObject.FindGameObjectWithTag("StartPosition");
        PathFollower.pathInsCreator = path;

        newWave?.Invoke(0);
        StartCoroutine(AllWavesGenerator());
    }

    void Update()
    {
        
    }

    private IEnumerator WaveGenerator()
    {
        for (int i = 0; i < enemyWaveCount; i++)
        {
            yield return new WaitForSeconds(startWaitBetweenEnemySpawn);

            int rnd = UnityEngine.Random.Range(1, 25);
            if (rnd > 22)
            {
                Instantiate(enemyHeavy, startPosition.transform);
            } else if (rnd > 13)
            {
                Instantiate(enemyMedium, startPosition.transform);
            } else
            {
                Instantiate(enemy, startPosition.transform);
            }
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
            enemyWaveCount += i;
            yield return new WaitForSeconds(intermission);
            newWave?.Invoke(i + 1);
        }
    }
}
