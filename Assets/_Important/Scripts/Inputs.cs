using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inputs : MonoBehaviour
{
    public static Inputs instance;
    public float speed = 1, turnSpeed = 1, vertSpeed = 1;
    public CharacterController cc;
    public Camera cam;
    public Animator anim;
    public GameObject puff;
    public MeshRenderer hat, robe;
    public Color[] colors;
    public GameObject magicParticleSystem;
    ParticleSystem.MainModule mainPS;
    public Projectile fireProjectile, waterProjectile, earthProjectile;
    public float fireMana = 100, waterMana = 100, earthMana = 100;
    public float health = 100;
    public RectTransform fireManaSprite, waterManaSprite, earthManaSprite, healthSprite;
    public Text fps;
    float lastFPS;
    int fpsCount = 0;

    float lastShootTime = 0;
    int color;
    void Start()
    {
        lastFPS = Time.time;
        instance = this;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        hat.material.color = colors[color];
        robe.material.color = colors[color];
        mainPS = magicParticleSystem.GetComponent<ParticleSystem>().main;
        mainPS.startColor = colors[color];
    }
    void Update()
    {
        fpsCount++;
        if (lastFPS + 0.5f < Time.time)
        {
            lastFPS = Time.time;
            fps.text = (fpsCount * 2).ToString();
            fpsCount = 0;
        }
        bool walking = false;
        if (Input.GetKey(KeyCode.W))
        {
            cc.SimpleMove(transform.forward * Time.deltaTime * speed);
            walking = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            cc.SimpleMove(-transform.right * Time.deltaTime * speed);
            walking = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            cc.SimpleMove(-transform.forward * Time.deltaTime * speed);
            walking = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            cc.SimpleMove(transform.right * Time.deltaTime * speed);
            walking = true;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (color == 0)
            {
                color = colors.Length - 1;
            }
            else
            {
                color--;
            }
            hat.material.color = colors[color];
            robe.material.color = colors[color];
            mainPS.startColor = colors[color];
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (color == colors.Length - 1)
            {
                color = 0;
            }
            else
            {
                color++;
            }
            hat.material.color = colors[color];
            robe.material.color = colors[color];
            mainPS.startColor = colors[color];
        }
        transform.eulerAngles += new Vector3(0, (Input.GetAxisRaw("Mouse X")) * turnSpeed, 0);
        float x = cam.transform.eulerAngles.x;
        if (x > 180)
        {
            x -= 360;
        }
        /* allow player to look up and down
        if (x > -5 && Input.GetAxisRaw("Mouse Y") < 0)
        {
            cam.transform.RotateAround(transform.position, transform.right, Input.GetAxisRaw("Mouse Y") * vertSpeed);
        }
        if (x < 60 && Input.GetAxisRaw("Mouse Y") > 0)
        {
            cam.transform.RotateAround(transform.position, transform.right, Input.GetAxisRaw("Mouse Y") * vertSpeed);
        }
        */
        if (Input.GetMouseButtonDown(0))
        {
            DoMagic();
        }
        if (Input.GetMouseButtonUp(0))
        {
            StopMagic();
        }
        anim.SetBool("Walking", walking);
        if (anim.GetBool("Magic"))
        {
            Shoot();
        }
    }

    void DoMagic()
    {
        anim.SetBool("Magic", true);
        magicParticleSystem.SetActive(true);
    }
    void StopMagic()
    {
        magicParticleSystem.SetActive(false);
        anim.SetBool("Magic", false);
    }
    void Shoot()
    {
        switch (color)
        {
            case 0://fire
                if (Time.time - lastShootTime > fireProjectile.cooldown
                && fireMana >= fireProjectile.manaCost)
                {
                    lastShootTime = Time.time;
                    GameObject go = ObjectPool.instance.GetProjectile(color, true);
                    go.transform.position = transform.position + transform.forward * 0.25f;
                    go.transform.rotation = transform.rotation;
                    go.SetActive(true);
                    fireMana -= fireProjectile.manaCost;
                    SetSprites();
                }
                break;
            case 1://water
                if (Time.time - lastShootTime > waterProjectile.cooldown
                && waterMana >= waterProjectile.manaCost)
                {
                    lastShootTime = Time.time;
                    GameObject go = ObjectPool.instance.GetProjectile(color, true);
                    go.transform.position = transform.position + transform.forward * 0.25f;
                    go.transform.rotation = transform.rotation;
                    go.SetActive(true);
                    waterMana -= waterProjectile.manaCost;
                    SetSprites();
                }
                break;
            case 2://earth
                if (Time.time - lastShootTime > earthProjectile.cooldown
                && earthMana >= earthProjectile.manaCost)
                {
                    lastShootTime = Time.time;
                    GameObject go = ObjectPool.instance.GetProjectile(color, true);
                    go.transform.position = transform.position + transform.forward * 0.25f;
                    go.transform.rotation = transform.rotation;
                    go.SetActive(true);
                    earthMana -= earthProjectile.manaCost;
                    SetSprites();
                }
                break;
        }
    }

    void MovePuff()
    {
        puff.SetActive(false);
        puff.SetActive(true);
    }

    public void Hit(Projectile p)
    {
        if (p.color == color)
        {
            switch (color)
            {
                case 0:
                    fireMana += p.damage;
                    break;
                case 1:
                    waterMana += p.damage;
                    break;
                case 2:
                    earthMana += p.damage;
                    break;
            }
        }
        else
        {
            health -= p.damage;
            if (health <= 0)
            {
                Die();
            }
        }
        SetSprites();
    }

    void SetSprites()
    {
        fireManaSprite.anchoredPosition = new Vector2(0, fireMana / 2 - 50);
        fireManaSprite.sizeDelta = new Vector2(20, fireMana);
        waterManaSprite.anchoredPosition = new Vector2(0, waterMana / 2 - 50);
        waterManaSprite.sizeDelta = new Vector2(20, waterMana);
        earthManaSprite.anchoredPosition = new Vector2(0, earthMana / 2 - 50);
        earthManaSprite.sizeDelta = new Vector2(20, earthMana);
        healthSprite.anchoredPosition = new Vector2(0, health / 2 - 50);
        healthSprite.sizeDelta = new Vector2(20, health);
    }

    public void Die()
    {
        ObjectPool.instance.Reset();
    }
    public void Restart()
    {
        earthMana = 100;
        fireMana = 100;
        waterMana = 100;
        health = 100;
        SetSprites();
    }
}
