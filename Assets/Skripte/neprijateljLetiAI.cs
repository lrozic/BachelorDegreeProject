using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class neprijateljLetiAI : MonoBehaviour
{

    public int zivot;
    private Animator animacija;

    Color originalnaBoja;
    bool udaren = false;
    bool unisten = false;
    bool pada = false;
    int pad = 0;

    private GameObject vitez;

    private Rigidbody2D rigidbodyLeteceg;

    public GameObject particleEffect;

    public float pocetnaPozicijaX;
    public float pocetnaPozicijaY;

    private int puniZivot;

    public int exp;

    public GameObject voda;

    private void Start()
    {
        animacija = GetComponent<Animator>();
        vitez = GameObject.Find("Igrac");
        rigidbodyLeteceg = GetComponent<Rigidbody2D>();

        pocetnaPozicijaX = this.transform.position.x;
        pocetnaPozicijaY = this.transform.position.y;
        puniZivot = zivot;

        originalnaBoja = this.GetComponent<SpriteRenderer>().color;
    }


    private void Update()
    {

        if (zivot <= 0)
        {
            rigidbodyLeteceg.velocity = new Vector2(0f, 0f);
            animacija.SetTrigger("zivotNaNuli");
            Invoke("deaktivirajNeprijatelja", (float)1.2);
        }

        else if (pada == true && pad < 15)
        {
            rigidbodyLeteceg.velocity = new Vector2(5f, -20f);
            pad++;
        }
        else if (pada == true && pad >= 15 && pad < 30)
        {
            rigidbodyLeteceg.velocity = new Vector2(-5f, -20f);
            pad++;
            if(pad >= 30)
            {
                pad = 0;
            }
        }

        if (udaren == true)
        {
            udaren = false;
            Invoke("vratiBoju", (float)0.2);
        }

        if (pada)
        {
            if(this.name == "NeprijateljLeti (2)" && this.transform.GetComponent<BoxCollider2D>().IsTouching(voda.GetComponent<BoxCollider2D>()))
            {
                voda.GetComponent<AudioSource>().enabled = true;
                voda.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    private void vratiBoju()
    {
        this.GetComponent<SpriteRenderer>().color = originalnaBoja;
    }

    public void smanjiZivot(int ozljeda)
    {
        zivot = zivot - ozljeda;
        udaren = true;

        if (zivot <= 0 && unisten == false)
        {
            zvukovi.pokreniZvuk("udarenNeprijatelj");
            this.gameObject.GetComponent<AudioSource>().enabled = true;
            unisten = true;   //to sam napravil tak da se samo jenput čuje zvuk da umire rak i da jednom dobi vitez exp bodove

            vitez.GetComponent<skriptaViteza>().dodajExpBodove(exp);  //moral na početku instancirati viteza jer ovak bez njega ne zna na koji objekt se 
                                                                     //skriptaVitez odnosi, a odnosi se na GameObject Igrac
            var smrt = Instantiate(particleEffect, transform.position, Quaternion.identity);
            Destroy(smrt, (float)1);

        }
        else if (zivot > 0)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(0.85f, 0.12f, 0.12f);
            zvukovi.pokreniZvuk("udarenNeprijatelj");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
          if (this.transform.GetChild(1).GetComponent<BoxCollider2D>().IsTouching(collision))
         {
            if (collision.gameObject.name == "Igrac" && zivot > 0)
             {
                 collision.gameObject.GetComponent<skriptaViteza>().smanjiZivotIgraca(2);               
            }
         }

    }

    public void translacija()
    {
        if (pada == false)  //moral to staviti jer se više put pokrene zvuk u protivnom
        {
            Invoke("deaktivirajNeprijatelja", (float)5);
            animacija.SetTrigger("igracBlizu");
            zvukovi.pokreniZvuk("zvukLeteciPada");
            pada = true;
        }

    }

    public void vratiSeNaPocetnuPoziciju()
    {
        rigidbodyLeteceg.velocity = new Vector2(0f, 0f);
        pada = false;
        pad = 0;
        this.gameObject.GetComponent<AudioSource>().enabled = false;
        this.transform.gameObject.SetActive(false);
        unisten = false;

        rigidbodyLeteceg.velocity = new Vector2(0f, 0f);
        animacija.ResetTrigger("igracBlizu");
        zivot = puniZivot;
        this.transform.position = new Vector2(pocetnaPozicijaX, pocetnaPozicijaY);
    }

    private void deaktivirajNeprijatelja()
    {
        if (transform.position.x != pocetnaPozicijaX && transform.position.y != pocetnaPozicijaY)
        {
            rigidbodyLeteceg.velocity = new Vector2(0f, 0f);
            pada = false;
            pad = 0;
            this.gameObject.GetComponent<AudioSource>().enabled = false;
            this.transform.gameObject.SetActive(false);
        }
        if(zivot <= 0)
        {
            rigidbodyLeteceg.velocity = new Vector2(0f, 0f);
            pada = false;
            pad = 0;
            this.gameObject.GetComponent<AudioSource>().enabled = false;
            this.transform.gameObject.SetActive(false);
        }
    }

}
