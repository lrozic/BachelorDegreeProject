using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class neprijateljKosturAI : MonoBehaviour
{

    public float moveSpeed;
    public int zivot;
    private Animator animacija;

    Rigidbody2D rigidKostura;

    Color originalnaBoja;
    bool udaren = false;
    bool unisten = false;

    bool jeliGledaVecDesno;

    public GameObject vitez;

    public GameObject particleEffect;

    bool izvodiNapad;
    bool stitiSe;
    private float pocetakSticenja;
    private float krajSticenja;

    public float pocetnaPozicijaX;
    public float pocetnaPozicijaY;

    private int puniZivot;

    public int exp;

    public GameObject efektZlatniKostur;
    public GameObject checkpoint;

    public bool besmrtan;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        rigidKostura = GetComponent<Rigidbody2D>();
        rigidKostura.velocity = new Vector2(-moveSpeed, 0f);
        animacija = GetComponent<Animator>();
        jeliGledaVecDesno = true;
        izvodiNapad = false;
        stitiSe = false;
        pocetakSticenja = 0f;
        krajSticenja = 1f;

        pocetnaPozicijaX = this.transform.position.x;
        pocetnaPozicijaY = this.transform.position.y;
        puniZivot = zivot;
        originalnaBoja = GetComponent<SpriteRenderer>().color;
        besmrtan = false;
    }


    private void Update()
    {
        if (checkpoint != null)
        {
            if (this.transform.tag == "protivnikZlatniKostur" && (vitez.GetComponent<skriptaViteza>().trenutniBrojZivota <= 0 || checkpoint.activeInHierarchy))
            {
                StartCoroutine("vratiZlatnog");
            }
        }

        //mac kostura je horizontalno postavljeni

        float udaljenostKosturaOdViteza = Vector2.Distance(transform.Find("tockaVektoraUdaljenosti").transform.position, vitez.gameObject.transform.position);
        if(zivot <= 0)
        {
            this.GetComponent<SpriteRenderer>().color = originalnaBoja;
            if (unisten == false)
            {
                this.transform.gameObject.GetComponent<AudioSource>().enabled = true;
                this.transform.Find("macKostura").gameObject.GetComponent<CapsuleCollider2D>().enabled = false;

                foreach (Transform eachChild in transform)
                {
                     if (eachChild.name == "hitBoxKostura")
                     {
                         eachChild.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                     }

                    if (eachChild.name == "macKostura")
                    {
                        eachChild.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                    }
                }
                unisten = true;
                vitez.GetComponent<skriptaViteza>().dodajExpBodove(exp);
                rigidKostura.gravityScale = 0;
                animacija.SetBool("hodaj", false);
                if(this.transform.gameObject.name == "NeprijateljZlatniKostur")
                {
                    animacija.SetTrigger("zlatniKosturUnisten");
                }
                else
                {
                    animacija.SetTrigger("zivotNaNuli");
                }
                if (this.transform.gameObject.name == "NeprijateljZlatniKostur")
                {
                    transform.GetChild(5).gameObject.SetActive(true);
                    transform.GetChild(6).gameObject.SetActive(true);
                    Vector2 pozicijaEfekta = new Vector2(transform.position.x, transform.position.y + 100);
                    var efektZlatniKosturSmrt = Instantiate(efektZlatniKostur, pozicijaEfekta, Quaternion.identity);
                    this.transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0.9712695f, 0.3443396f, 0.6235294f);
                    Destroy(this.gameObject, 4f);
                    Destroy(efektZlatniKostur, 4f);
                }
                else
                {
                    Invoke("deaktivirajNeprijatelja", 1.5f);
                }
            }
        }
        else if (stitiSe == true)
        {
            rigidKostura.velocity = new Vector2(0f, 0f);
            pocetakSticenja += Time.deltaTime;
            if (pocetakSticenja > krajSticenja)    //nikad nebumo koristili == jer nikad nemas tu preciznost, bolje koristit vece
            {
                stitiSe = false;
                pocetakSticenja = 0f;
            }
        }
        else
        {
            if (udaljenostKosturaOdViteza >= 15 || udaljenostKosturaOdViteza <= 3.7 && !izvodiNapad)
            {
                animacija.SetBool("hodaj", false);
                rigidKostura.velocity = new Vector2(0f, 0f);
            }

            else if (transform.position.x >= vitez.gameObject.transform.position.x && !izvodiNapad)
            //ako se vitez nalazi levo od kostura
            {
                if (jeliGledaVecDesno == true) //ako gleda desno a treba gledat levo jer je levo vitez
                {
                    jeliGledaVecDesno = false;
                    transform.localRotation = Quaternion.Euler(0, 180, 0);
                }

                rigidKostura.velocity = new Vector2(-moveSpeed, 0f);
                animacija.SetBool("hodaj", true);
            }

            else if (this.transform.position.x < vitez.transform.position.x && !izvodiNapad)
            {
                if (jeliGledaVecDesno == false) //ako gleda desno a treba gledat levo jer je levo vitez
                {
                    jeliGledaVecDesno = true;
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                }

                rigidKostura.velocity = new Vector2(moveSpeed, 0f);
                animacija.SetBool("hodaj", true);
            }

            if (udaljenostKosturaOdViteza < 3.7 && !izvodiNapad)
            {
                rigidKostura.velocity = new Vector2(0f, 0f);
                izvodiNapad = true;
                StartCoroutine("napadni");
            }
        }

        if (udaren == true)
        {
            udaren = false;
            //Invoke("vratiBoju", (float)0.35);
        }

        if (this.transform.Find("hitBoxKostura").GetComponent<PolygonCollider2D>().IsTouching(vitez.gameObject.GetComponent<BoxCollider2D>()))  //FindChild se ne koristi, nego samo Find, znat zakaj
        {
            if (vitez.gameObject.name == "Igrac" && zivot > 0)
            {
                vitez.gameObject.GetComponent<skriptaViteza>().smanjiZivotIgraca(1);
            }
        }
    }

    private IEnumerator vratiZlatnog()
    {
        yield return new WaitForSeconds(2f);
        zivot = puniZivot;
        besmrtan = false;
        transform.position = new Vector2(pocetnaPozicijaX, pocetnaPozicijaY);
    }

    private IEnumerator napadni()
    {
        animacija.SetTrigger("napada");
        yield return new WaitForSeconds(0.52f);
        if (zivot > 0)
        {
            this.transform.Find("macKostura").gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        }
        yield return new WaitForSeconds(0.16f);
        this.transform.Find("macKostura").gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.64f);

        izvodiNapad = false;
        if (zivot > 0)
        {
            if (transform.position.x >= vitez.gameObject.transform.position.x && !izvodiNapad)
            {
                if (jeliGledaVecDesno == true)
                {
                    jeliGledaVecDesno = false;
                    transform.localRotation = Quaternion.Euler(0, 180, 0);
                }

                rigidKostura.velocity = new Vector2(-moveSpeed, 0f);
                animacija.SetBool("hodaj", true);

            }

            else if (this.transform.position.x < vitez.transform.position.x && !izvodiNapad)
            {
                if (jeliGledaVecDesno == false)
                {
                    jeliGledaVecDesno = true;
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                }

                rigidKostura.velocity = new Vector2(moveSpeed, 0f);
                animacija.SetBool("hodaj", true);
            }
        }
    }
       

    private void vratiBoju()
    {
        if (zivot > 0)
        {
           // this.GetComponent<SpriteRenderer>().color = originalnaBoja;
        }
    }

    private IEnumerator transparentnost()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f);
        yield return new WaitForSeconds(1f);
        GetComponent<SpriteRenderer>().color = originalnaBoja;
        besmrtan = false;
    }

    public void smanjiZivot(int ozljeda)
    {
        if (transform.position.x >= vitez.gameObject.transform.position.x && jeliGledaVecDesno && zivot > 0) //ako kostur gleda desno a pozicija igraca je levo
        {
            //ne dobiva damage kostur zbog stita koji se nalazi na levoj strani
            zvukovi.pokreniZvuk("zvukUdarenogStita");
        }
        
        else if (transform.position.x < vitez.gameObject.transform.position.x && !jeliGledaVecDesno && zivot > 0)
        {
            zvukovi.pokreniZvuk("zvukUdarenogStita");
        }

        else if (transform.position.x >= vitez.gameObject.transform.position.x && !jeliGledaVecDesno && !izvodiNapad && zivot > 0)
        {
            animacija.SetBool("hodaj", false);
            animacija.SetTrigger("stitiSe");
            zvukovi.pokreniZvuk("zvukUdarenogStita");
            stitiSe = true;
        }

        else if(transform.position.x < vitez.gameObject.transform.position.x && jeliGledaVecDesno && !izvodiNapad && zivot > 0)
        {
            animacija.SetBool("hodaj", false);
            animacija.SetTrigger("stitiSe");
            zvukovi.pokreniZvuk("zvukUdarenogStita");
            stitiSe = true;
        }

        else if (zivot > 0 && besmrtan == false)
        {
            zivot = zivot - ozljeda;
            udaren = true;
            //this.GetComponent<SpriteRenderer>().color = new Color(0.85f, 0.12f, 0.12f);
            if (zivot > 0) {
                zvukovi.pokreniZvuk("zvukUdarenogKostura"); //tak da se ne čuje nakon kaj je zivot == il < od 0
                besmrtan = true;
                StartCoroutine(transparentnost());
            }
        }
    }

    public void vratiSeNaPocetnuPoziciju()
    {
        besmrtan = false;
        unisten = false;
        zivot = puniZivot;
        rigidKostura.gravityScale = 1f;
        foreach (Transform eachChild in transform)
        {
            if (eachChild.name == "hitBoxKostura")  //moral disableati jer dok umre, i dalje igrac more bit napadnut
            {
                eachChild.gameObject.GetComponent<PolygonCollider2D>().enabled = true;  //ovde si 29.7.2020. promenil na true
            }
            if (eachChild.name == "macKostura")
            {
                eachChild.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            }
        }
        this.transform.position = new Vector2(pocetnaPozicijaX, pocetnaPozicijaY);
    }

    private void deaktivirajNeprijatelja()
    {
        this.transform.gameObject.SetActive(false);
        this.transform.gameObject.GetComponent<AudioSource>().enabled = false;  //jer se zvuk čuje dok se respawna, a taj zvuk je dok umre, i ovo je dokaz da mu se kod izvrši i dok nije aktivan, jer se ne cuje audio
    }

}
