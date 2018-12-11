using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int nbrSpawnPerWave = 50;
    public int nbrWave = 5;
    public float delayBtwWaves = 10;

    // Use this for initialization
    void Start () {
            StartCoroutine("spawn");
	}

    IEnumerator spawn()
    {
        for (int i = 0; i < nbrWave; i++)
        {
            for (int j = 0; j < nbrSpawnPerWave; j++)
            {
                GameObject enemy = Instantiate(enemyPrefab, transform.position + new Vector3(Random.value - 0.5f, 0f, Random.value - .5f) * 3f, Quaternion.identity);
            }
            yield return new WaitForSeconds(delayBtwWaves);
        }
    }

}
