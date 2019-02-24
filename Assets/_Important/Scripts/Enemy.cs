using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int color; //0: fire, 1: water, 2: earth, -1: none
    public CharacterController cc;
    float speed = 100; 
    public float health = 10, range = 20;
    public float[] shootAngles = new float[] { 0 };
    protected float lastShootTime;
    public float timeBetweenShoot = 1, angleDif;
    public MeshRenderer meshRenderer;

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!Move() && Time.time - lastShootTime > timeBetweenShoot)
        {
            foreach (float a in shootAngles)
            {
                GameObject go = ObjectPool.instance.GetProjectile(color);
                go.transform.position = transform.position + transform.forward * 0.25f;
                go.transform.rotation = transform.rotation;
                go.transform.RotateAround(transform.position, Vector3.up, a);
                go.SetActive(true);
            }
            transform.Rotate(Vector3.up, angleDif);
            lastShootTime = Time.time;
        }
    }

    protected virtual bool Move()
    {
        transform.LookAt(Inputs.instance.transform);
        if (Vector3.Distance(Inputs.instance.transform.position, transform.position) > range)
        {
            cc.SimpleMove((Inputs.instance.transform.position - transform.position).normalized * speed * Time.deltaTime);
            return true;
        }
        return false;
    }

    public void Hit(Projectile p)
    {
        if(p.color != color)
        {
            health -= p.damage;
            if (health <= 0)
            {
                MenuManager.instance.score += 2500;
                EnemySpawner.instance.RemoveEnemy(this);
                Destroy(gameObject);
            }
        }
    }
}
