using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    public GameObject fpe, fpp, wpe, wpp, epe, epp;
    public int userCount, enemyCounnt;
    GameObject[][] projectiles;
    int[] counts;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        counts = new int[6];
        projectiles = new GameObject[6][];
        projectiles[0] = SpawnProjectiles(fpe, enemyCounnt);
        projectiles[1] = SpawnProjectiles(wpe, enemyCounnt);
        projectiles[2] = SpawnProjectiles(epe, enemyCounnt);
        projectiles[3] = SpawnProjectiles(fpp, userCount);
        projectiles[4] = SpawnProjectiles(wpp, userCount);
        projectiles[5] = SpawnProjectiles(epp, userCount);
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
        if (counts[i] >= projectiles[i].Length)
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
            for (int j = 0; j < projectiles[i].Length; j++)
            {
                projectiles[i][j].SetActive(false);
            }
        }
    }
}
