using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Phase { WAITING, CLEANING, SPAWNING }

public class WaveGenerator : MonoBehaviour
{
    public struct OneSpawnData
    {
        public Vector3 spawnLocation;
        public GameObject unitPrefab;
        public float time;

        public OneSpawnData(Transform spawn, GameObject prefab, float time)
        {
            spawnLocation = spawn.position;
            spawnLocation.y = 0f;
            unitPrefab = prefab;
            this.time = time;
        }
    };

    private float timeForNextRandom;
    public float timeBetweenRandom;
    public float delayFromStart;
    public Transform[] allSpawnPoints;
    public GameObject[] enemyPrefab;
    public int EnnemyPerWave;
    public int EnnemyAutorizedOnMap;
    /*public*/
    int k;
    public float delayBetweenWaves;
    public UnityEngine.UI.Text nbrVagueFinie;

    float timeSincebegining;

    OneSpawnData[] waveData;
    Phase currentPhase = Phase.WAITING;
    float waitTimer = 0.0f;
    int waveCount;
    int spawned = 0;

    int alive;
    public bool active;

    // Use this for initialization
    void Start()
    {
        timeForNextRandom = Time.time;
        // On mélange la liste des points de Spawn et on choisi les k premiers comme points de Spawn actifs pour cette partie.
        for (int i = 0; i < allSpawnPoints.Length; i++)
        {
            int r = Random.Range(0, allSpawnPoints.Length - 1);
            Transform temp = allSpawnPoints[i];
            allSpawnPoints[i] = allSpawnPoints[r];
            allSpawnPoints[r] = temp;
        }

        k = allSpawnPoints.Length;// Mathf.Clamp(k, 1, allSpawnPoints.Length);
        waveData = new OneSpawnData[EnnemyPerWave];
        float baseTime = 0;
        for (int i = 0; i < EnnemyPerWave; i++)
        {
            baseTime += Random.Range(0.4f, 1.2f);
            waveData[i] = new OneSpawnData(allSpawnPoints[Random.Range(0, k)], enemyPrefab[Random.Range(0, enemyPrefab.Length)], baseTime);
        }
        nbrVagueFinie.text = "Waves : 1";

    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            return;
        }
        /*
        if(currentPhase == Phase.SPAWNING)
        {
            timeSincebegining += Time.deltaTime;
            while (spawned < EnnemyAutorizedOnMap && timeSincebegining > waveData[spawned].time)//utile?
            {
                Vector3 offset = new Vector3(Random.value - 0.5f, 0f, Random.value - 0.5f) * 3f;
                GameObject last = Instantiate(waveData[spawned].unitPrefab, waveData[spawned].spawnLocation + offset, Quaternion.identity);
                spawned += 1;
                alive += 1;
            }
            currentPhase = Phase.CLEANING;

            /*if(alive <= EnnemyAutorizedOnMap)
            { 
                currentPhase = Phase.CLEANING;
            }*/
        //}*/

        /*else*/
        if (currentPhase == Phase.CLEANING)
        {
            Debug.Log("cleaning");
            if (timeForNextRandom <= Time.time)
            {
                if (spawned < EnnemyPerWave && alive < EnnemyAutorizedOnMap)
                {
                    Vector3 offset = new Vector3(Random.value - 0.5f, 0f, Random.value - 0.5f) * 3f;
                    GameObject last = Instantiate(waveData[spawned].unitPrefab, waveData[spawned].spawnLocation + offset, Quaternion.identity);
                    spawned += 1;
                    alive += 1;
                }
                timeForNextRandom = Time.time + timeBetweenRandom;
            }

            //si y'a actuellement - d'ennemis que les eenemis autorisés, et que spawned est inférieur à EnnemyPerWave, alors on pop un nouvel ennemi et spawned++
        }

        else if (currentPhase == Phase.WAITING)
        {

            waitTimer += Time.deltaTime;
            if (waveCount == 0 && waitTimer > delayFromStart || waitTimer > delayBetweenWaves)
            {
                StartWave();
            }
        }
        else
        {

        }
    }

    public void OnMinionDeath(Enemy e)
    {
        alive -= 1;
        Debug.Log("Ennemis restant : " + alive);
        if (spawned == EnnemyPerWave && alive == 0)
        {
            currentPhase = Phase.WAITING;
            nbrVagueFinie.text = "Vagues : " + (waveCount + 1);
            Debug.Log("Fin de la Vague");
        }
    }

    public void StartWave()
    {
        waveCount += 1;
        currentPhase = Phase.CLEANING; //Phase.SPAWNING;
        waitTimer = 0.0f;
        spawned = 0;
        timeSincebegining = 0;
        alive = 0;
        Debug.Log("Début de la vague " + waveCount);
    }
}
