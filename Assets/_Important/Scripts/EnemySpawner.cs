using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;
    List<Enemy> enemies = new List<Enemy>();
    float lastSpawn, spawnTime = 4;
    public GameObject[] enemyPrefabs;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastSpawn + spawnTime < Time.time && !MenuManager.instance.MainMenu) {
            lastSpawn = Time.time;
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        GameObject prefab = enemyPrefabs[Mathf.FloorToInt(Random.value * enemyPrefabs.Length)];
        Vector3 pos = new Vector3((Random.value - 0.5f), 0, (Random.value - 0.5f)).normalized * Random.value * 30;
        while(Vector3.Distance(Inputs.instance.transform.position, pos) < 10)
        {
            pos = new Vector3((Random.value - 0.5f), 0, (Random.value - 0.5f)).normalized * Random.value * 30;
        }
        pos += Vector3.up * 0.3f;
        GameObject go = Instantiate(prefab, pos, Quaternion.identity);
        enemies.Add(go.GetComponent<Enemy>());
    }

    public void Reset()
    {
        foreach(Enemy e in enemies)
        {
            Destroy(e.gameObject);
        }
        enemies = new List<Enemy>();
    }

    public void RemoveEnemy(Enemy e)
    {
        enemies.Remove(e);
    }
}
