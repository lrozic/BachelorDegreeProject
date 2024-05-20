using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class neprijateljRakAI : MonoBehaviour
{
    public float moveSpeed; 
    public int zivot;
    private Animator animacija;

    Rigidbody2D rigidBodyRaka;

    Color originalnaBoja;
    bool udaren = false;
    bool unisten = false;
    public bool idiDesno;

    private GameObject vitez;

    public GameObject particleEffect;

    public float pocetnaPozicijaX;
    public float pocetnaPozicijaY;

    private int puniZivot;
    public int exp;

    public GameObject dijeloviRakaEfektDesno;
    public GameObject dijeloviRakaEfektLijevo;

    private void Start()
    {
        rigidBodyRaka = GetComponent<Rigidbody2D>();
        rigidBodyRaka.velocity = new Vector2(-moveSpeed, 0f);
        animacija = GetComponent<Animator>();
        vitez = GameObject.Find("Igrac");
        pocetnaPozicijaX = this.transform.position.x;
        pocetnaPozicijaY = this.transform.position.y;
        puniZivot = zivot;
        originalnaBoja = GetComponent<SpriteRenderer>().color;
        if (!idiDesno) { 
            transform.localRotation = Quaternion.Euler(0, -180, 0);
        }

}


    private void Update()
    {
        if (IsFacingRight() && zivot > 0)
        {
            rigidBodyRaka.velocity = new Vector2(moveSpeed, 0f);
        }
        else if (!IsFacingRight() && zivot > 0)
        {
            rigidBodyRaka.velocity = new Vector2(-moveSpeed, 0f);
        }
        else
        {
            rigidBodyRaka.velocity = new Vector2(0f, 0f);
        }

        if (zivot <= 0)
        {
            animacija.SetTrigger("zivotNaNuli");
            //Destroy(gameObject,(float)0.28);
            Invoke("deaktivirajNeprijatelja", 0.1f);
        }

        if (udaren == true)
        {
            udaren = false;
            Invoke("vratiBoju", (float)0.2);
        }

        foreach (Transform eachChild in transform)
        {
            if(eachChild.name == "ColliderHitBox")
            {
                if (eachChild.gameObject.GetComponent<BoxCollider2D>().IsTouching(vitez.gameObject.GetComponent<BoxCollider2D>()))
                {
                    if (this.transform.name == "NeprijateljRakCrni")
                    {
                        vitez.gameObject.GetComponent<skriptaViteza>().smanjiZivotIgraca(3);
                    }
                    else
                    {
                        vitez.gameObject.GetComponent<skriptaViteza>().smanjiZivotIgraca(1);
                    }
                }
            }
        }

    }

    private void vratiBoju()
    {
        this.GetComponent<SpriteRenderer>().color = originalnaBoja;
    }


    public bool IsFacingRight() 
    {
        if (idiDesno == true)
        {
            return transform.localScale.x > Mathf.Epsilon;
        }
        else
        {
            return transform.localScale.x < Mathf.Epsilon;
        }
    }


    public void smanjiZivot(int ozljeda)
    {
        zivot = zivot - ozljeda;
        udaren = true;

        if (zivot<= 0 && unisten == false)
        {
            if (this.name == "NeprijateljRakCrni")
            {
                this.GetComponent<PolygonCollider2D>().enabled = false;
                this.gameObject.GetComponent<AudioSource>().enabled = true;
            }
            else
            {
                foreach (Collider col in GetComponents<Collider>())
                {
                    col.enabled = false;
                }
            }
            zvukovi.pokreniZvuk("udarenNeprijatelj");
            zvukovi.pokreniZvuk("rakUmire");
            unisten = true;   //to sam napravil tak da se samo jenput čuje zvuk da umire rak i da jednom dobi vitez exp bodove

            vitez.GetComponent<skriptaViteza>().dodajExpBodove(exp);  //moral na početku instancirati viteza jer ovak bez njega ne zna na koji objekt se 
                                                                    //skriptaVitez odnosi, a odnosi se na GameObject Igrac

            var smrt = Instantiate(particleEffect, transform.position, Quaternion.identity);

            Vector3 pozicijaEfekata = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);

            if (transform.position.x >= vitez.gameObject.transform.position.x) {
                var dijeloviRaka = Instantiate(dijeloviRakaEfektDesno, pozicijaEfekata, Quaternion.identity);
                    Destroy(dijeloviRaka, (float)5f);
            }
            else
            {
                var dijeloviRaka = Instantiate(dijeloviRakaEfektLijevo, pozicijaEfekata, Quaternion.identity);
                    Destroy(dijeloviRaka, (float)5f);
            }

            Destroy(smrt, (float)1);
        }
        else if(zivot > 0)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(0.85f, 0.12f, 0.12f);
            zvukovi.pokreniZvuk("udarenNeprijatelj");
        }
    }

    public void vratiSeNaPocetnuPoziciju()
    {
        if (!(this.name == "NeprijateljRakCrni"))
        {
            foreach (Collider col in GetComponents<Collider>())
            {
                col.enabled = false;
            }
        }
        zivot = puniZivot;
        unisten = false;
        this.transform.position = new Vector2(pocetnaPozicijaX, pocetnaPozicijaY);
    }

    private void deaktivirajNeprijatelja()
    {
        if (this.name == "NeprijateljRakCrni")
        {
            Destroy(this.gameObject);
        }
        else
        {
            this.transform.gameObject.SetActive(false);
        }
    }

}

