using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameObject enemyPrefab;
    public GameObject barrelPrefab;

    void Start()
    {
        SpawnBarrel(new Vector2(0,-3.5f));
        CreateEnemies();
    }

    void Update()
    {
        
    }

    void CreateEnemies()
    {
        for (int i = 1; i < 10; i++)
        {
            SpawnEnemy(new Vector2(0, -3.5f + (i * 0.99f)));
        }
    }

    void SpawnEnemy(Vector2 position)
    {
        GameObject newEnemy = Instantiate(enemyPrefab);
        newEnemy.transform.position = position;
    }

    void SpawnBarrel(Vector2 position)
    {
        GameObject newBarrel = Instantiate(barrelPrefab);
        newBarrel.transform.position = position;
    }
}
