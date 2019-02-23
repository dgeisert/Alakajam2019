using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    public GameObject fpe, fpp, wpe, wpp, epe, epp;
    GameObject[][] projectiles;
    int[] counts;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        counts = new int[6];
        projectiles = new GameObject[6][];
        projectiles[0] = SpawnProjectiles(fpe, 100);
        projectiles[1] = SpawnProjectiles(wpe, 100);
        projectiles[2] = SpawnProjectiles(epe, 100);
        projectiles[3] = SpawnProjectiles(fpp, 100);
        projectiles[4] = SpawnProjectiles(wpp, 100);
        projectiles[5] = SpawnProjectiles(epp, 100);
    }

    GameObject[] SpawnProjectiles(GameObject prefab, int count)
    {
        GameObject[] projs = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            projs[i] = Instantiate(prefab);
            projs[i].SetActive(false);
        }
        return projs;
    }

    public GameObject GetProjectile(int type, bool is_player = false)
    {
        int i = type + (is_player ? 3 : 0);
        counts[i]++;
        if (counts[i] >= 100)
        {
            counts[i] = 0;
        }
        projectiles[i][counts[i]].SetActive(false);
        return projectiles[i][counts[i]];
    }

    public void Reset()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                projectiles[i][j].SetActive(false);
            }
        }
    }
}
