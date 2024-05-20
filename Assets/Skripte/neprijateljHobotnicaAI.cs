using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class neprijateljHobotnicaAI : MonoBehaviour
{
    public float moveSpeed;
    public int zivot;
    private Animator animacija;

    Rigidbody2D rigidBodyHobotnice;

    Color originalnaBoja;
    bool udaren = false;
    bool unisten = false;

    private GameObject vitez;

    public GameObject particleEffect;

    public float pocetnaPozicijaX;
    public float pocetnaPozicijaY;

    private int puniZivot;

    public int exp;

    public bool idiLijevo;

    private void Start()
    {
        rigidBodyHobotnice = GetComponent<Rigidbody2D>();
        animacija = GetComponent<Animator>();
        vitez = GameObject.Find("Igrac");

        pocetnaPozicijaX = this.transform.position.x;
        pocetnaPozicijaY = this.transform.position.y;
        puniZivot = zivot;
        originalnaBoja = this.GetComponent<SpriteRenderer>().color;

        if (!idiLijevo)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }


    private void Update()
    {
        if (moveSpeed > 0)
        {
            if (gledaLiLijevo() && zivot > 0)
            {
                rigidBodyHobotnice.velocity = new Vector2(moveSpeed, 0f);
            }
            else if (!gledaLiLijevo() && zivot > 0)
            {
                rigidBodyHobotnice.velocity = new Vector2(-moveSpeed, 0f);
            }
            else
            {
                rigidBodyHobotnice.velocity = new Vector2(0f, 0f);
            }
        }
        else
        {
            rigidBodyHobotnice.velocity = new Vector2(moveSpeed, 0f);
        }
        if (zivot <= 0)
        {
            animacija.SetTrigger("zivotNaNuli");
            Invoke("deaktivirajNeprijatelja", 0.1f);

        }

        if (udaren == true)
        {
            udaren = false;
            Invoke("vratiBoju", (float)0.35);
        }

        foreach (Transform eachChild in transform)
        {
            if (eachChild.name == "ColliderHitBoxHobotnice")
            {
                if (eachChild.gameObject.GetComponent<BoxCollider2D>().IsTouching(vitez.gameObject.GetComponent<BoxCollider2D>()))
                {
                    vitez.gameObject.GetComponent<skriptaViteza>().smanjiZivotIgraca(1);
                }
            }
        }
    }

    private void vratiBoju()
    {
        this.GetComponent<SpriteRenderer>().color = originalnaBoja;
    }


    public bool gledaLiLijevo()
    {
        if (idiLijevo == true)
        {
            return transform.localScale.x < Mathf.Epsilon;
        }
        else
        {
            return transform.localScale.x > Mathf.Epsilon;
        }
    }


    public void smanjiZivot(int ozljeda)
    {
        zivot = zivot - ozljeda;
        udaren = true;

        if (zivot <= 0 && unisten == false)
        {
            this.GetComponent<SpriteRenderer>().color = originalnaBoja;
            zvukovi.pokreniZvuk("udarenNeprijatelj");
            zvukovi.pokreniZvuk("rakUmire");
            unisten = true;   //to sam napravil tak da se samo jenput čuje zvuk da umire rak i da jednom dobi vitez exp bodove

            vitez.GetComponent<skriptaViteza>().dodajExpBodove(exp);  //moral na početku instancirati viteza jer ovak bez njega ne zna na koji objekt se 
                                                                      //skriptaVitez odnosi, a odnosi se na GameObject Igrac

            var smrt = Instantiate(particleEffect, transform.position, Quaternion.identity);
            Destroy(smrt, (float)1.2);
        }
        else if (zivot > 0)
        {
            if (originalnaBoja.r < 1f)
            {
                this.GetComponent<SpriteRenderer>().color = new Color(0.3396226f, 0f, 0.05993346f);
            }
            else
            {
                this.GetComponent<SpriteRenderer>().color = new Color(0.85f, 0.12f, 0.12f);
            }
            zvukovi.pokreniZvuk("udarenNeprijatelj");
        }
    }

    public void vratiSeNaPocetnuPoziciju()
    {
        zivot = puniZivot;
        unisten = false;
        this.transform.position = new Vector2(pocetnaPozicijaX, pocetnaPozicijaY);
    }

    private void deaktivirajNeprijatelja()
    {
        this.transform.gameObject.SetActive(false);
    }

}

