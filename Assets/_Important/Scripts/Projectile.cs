using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime, damage, manaCost, speed, cooldown;
    public GameObject deathParticle;
    public int color; //0: fire, 1: water, 2: earth, -1: none
    float SpawnTime;

    void OnEnable()
    {
        deathParticle.SetActive(false);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (SpawnTime + lifetime > Time.time)
        {
            Die();
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
        deathParticle.transform.SetParent(null);
        deathParticle.SetActive(false);
        deathParticle.SetActive(true);
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.collider.name);
        Enemy e = col.collider.GetComponent<Enemy>();
        if (e != null)
        {
            e.Hit(this);
        }
        else
        {
            Inputs i = col.collider.GetComponent<Inputs>();
            if (i != null)
            {
                i.Hit(this);
            }
        }
        deathParticle.transform.SetParent(null);
        deathParticle.SetActive(true);
        gameObject.SetActive(false);
    }
}
