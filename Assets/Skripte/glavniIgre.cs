using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class glavniIgre : MonoBehaviour
{

    public float moveSpeed;
    private float novaBrzina;
    public int zivot;
    private int punZivot;
    private Animator animacija;

    Rigidbody2D rigidGlavnog;

    Color crvena = new Color(0.85f, 0.12f, 0.12f);
    Color bijela = new Color(1f, 1f, 1f);
    bool udaren = false;

    bool promjenjenaStranaDesno;    //dal je prvi put promenil stranu, bitno jer onda se dole ulazi samo jednom u if koji glavnog preokrene samo jednom
    bool promjenjenaStranaLijevo;

    bool prvaPromjenaLijeveStrane;
    bool prvaPromjenaDesneStrane;

    public GameObject vitez;

    public GameObject particleEffect;

    public bool pozicijaIgracaDesno;    //collideri detektiraju na kojoj strani se nalazi
    internal bool pozicijaIgracaDesnoSredina;   //sa generate metodom to napravil, taj internal bool kod druge skripte
    internal bool pozicijaIgracaLijevoSredina;

    private bool izvodiNapad;
    private bool bjezi;
    private bool kotrljaSe;
    private float akceleracija;

    private int premaDesnojStraniGleda;
    public int smjerGrmljavine;

    public GameObject grmljavina;
    public GameObject vatra;
    public GameObject led;
    public GameObject dogadajiNakonSmrti;

    private bool neStitiSe;

    public float pocetnaPozicijaX;
    public float pocetnaPozicijaY;

    private void Start()
    {
        rigidGlavnog = GetComponent<Rigidbody2D>();
        animacija = GetComponent<Animator>();
        pozicijaIgracaDesno = true;
        promjenjenaStranaLijevo = false;

        promjenjenaStranaDesno = true;
        izvodiNapad = false;
        bjezi = false;
        kotrljaSe = false;
        smjerGrmljavine = 1;

        akceleracija = 1;
        premaDesnojStraniGleda = 1;
        punZivot = zivot;
        novaBrzina = moveSpeed + (float)2.3;    //linija 167, tam se isto povecava brzina
        neStitiSe = true;

        pocetnaPozicijaX = this.transform.position.x;
        pocetnaPozicijaY = this.transform.position.y;

    }


    private void Update()
    {
        if(rigidGlavnog.velocity.x < 0.1f && rigidGlavnog.velocity.x > -0.1f)
        {
            animacija.SetBool("hodaj", false);
        }

        if (pozicijaIgracaDesno && zivot > 0 && !izvodiNapad && vitez.activeInHierarchy)
        {
            if (promjenjenaStranaDesno == false) //od tud
            {
                promjenjenaStranaDesno = true;
                premaDesnojStraniGleda = 1;
                transform.Translate(-2f, 0, 0);
                GameObject.Find("dometGlavnogDesno").GetComponent<dometGlavnog>().makniRoditelja();
                GameObject.Find("dometGlavnogLijevo").GetComponent<DometGlavnogLijevo>().makniRoditelja();
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                StartCoroutine("nasumicniNapadi", premaDesnojStraniGleda);
            }


            promjenjenaStranaLijevo = false;        
            rigidGlavnog.velocity = new Vector2(moveSpeed, 0f);
            animacija.SetBool("hodaj", true);

        }
        else if (!pozicijaIgracaDesno && zivot > 0 && !izvodiNapad && vitez.activeInHierarchy)
        {
            if (promjenjenaStranaLijevo == false)
            {
                promjenjenaStranaLijevo = true;
                premaDesnojStraniGleda = -1;
                transform.Translate(-2f, 0, 0);
                GameObject.Find("dometGlavnogLijevo").GetComponent<DometGlavnogLijevo>().makniRoditelja();
                GameObject.Find("dometGlavnogDesno").GetComponent<dometGlavnog>().makniRoditelja();
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                StartCoroutine("nasumicniNapadi", premaDesnojStraniGleda);
            }

            promjenjenaStranaDesno = false;
            rigidGlavnog.velocity = new Vector2(-moveSpeed, 0f);
            animacija.SetBool("hodaj", true);

        }
        if (pozicijaIgracaLijevoSredina && pozicijaIgracaDesnoSredina && !izvodiNapad)
        {
            rigidGlavnog.velocity = new Vector2(0f, 0f);
            animacija.SetBool("hodaj", false);
        }

        if (!vitez.activeInHierarchy)
        {
            rigidGlavnog.velocity = new Vector2(0f, 0f);
            animacija.SetBool("hodaj", false);
        }

        if (udaren == true)
        {
            udaren = false;
            Invoke("vratiBoju", (float)0.7);
        }

        if(vitez.activeInHierarchy){
            if (GameObject.Find("ColliderHitBoxGlavnog").GetComponent<BoxCollider2D>().IsTouching(vitez.gameObject.GetComponent<BoxCollider2D>()))
            {
                if (vitez.gameObject.name == "Igrac" && zivot > 0)
                {
                    vitez.gameObject.GetComponent<skriptaViteza>().smanjiZivotIgraca(1);
                }
            }
        }

        if (bjezi && novaBrzina != moveSpeed)
        {
            rigidGlavnog.velocity = new Vector2(moveSpeed * akceleracija * premaDesnojStraniGleda, 0f);
            akceleracija += (float)0.01;
        }
        else if (bjezi && novaBrzina == moveSpeed)
        {
            rigidGlavnog.velocity = new Vector2(moveSpeed * akceleracija * premaDesnojStraniGleda, 0f);
            akceleracija += (float)0.013;
        }

        else if (kotrljaSe)
        {
            if (moveSpeed != novaBrzina)
            {
                rigidGlavnog.velocity = new Vector2(moveSpeed * (float)3 * premaDesnojStraniGleda, 0f);
            }
            else
            {
                rigidGlavnog.velocity = new Vector2(moveSpeed * (float)2.5 * premaDesnojStraniGleda, 0f);
            }
            akceleracija = 1;
        }

        if (zivot <= (punZivot / 2) && novaBrzina != moveSpeed){
            moveSpeed = novaBrzina;
        }

        if (this.transform.Find("dometMaca").gameObject.GetComponent<BoxCollider2D>().IsTouching(vitez.gameObject.GetComponent<BoxCollider2D>()) 
            && !izvodiNapad) {
            izvodiNapad = true;
            animacija.SetTrigger("napadniMacem");
            this.transform.Find("macGlavnog").gameObject.GetComponent<BoxCollider2D>().enabled = true;
            StartCoroutine("napadMacem", premaDesnojStraniGleda);
            }
    }

    private void vratiBoju()
    {
        if (novaBrzina != moveSpeed)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = new Color(0.85f, 0.12f, 0.12f);

        }
    }


    public void smanjiZivot(int ozljeda)
    {
        if (zivot > 0 && neStitiSe)
        {
            zvukovi.pokreniZvuk("zvukOzljedeniGlavni");
            zivot = zivot - ozljeda;
            udaren = true;
            this.GetComponent<SpriteRenderer>().color = new Color(0.655f, 0f, 0f);
        }
        else if (!neStitiSe && premaDesnojStraniGleda == 1 && pozicijaIgracaDesno)
        {
            zvukovi.pokreniZvuk("zvukUdarenogStita");
        }
        else if(!neStitiSe && premaDesnojStraniGleda == -1 && !pozicijaIgracaDesno)
        {
            zvukovi.pokreniZvuk("zvukUdarenogStita");
        }
        else
        {
            zvukovi.pokreniZvuk("zvukOzljedeniGlavni");
            zivot = zivot - ozljeda;
            udaren = true;
            if (novaBrzina != moveSpeed)
            {
                this.GetComponent<SpriteRenderer>().color = new Color(0.85f, 0.12f, 0.12f);
            }
            else
            {
                this.GetComponent<SpriteRenderer>().color = new Color(0.3396226f, 0f, 0.05993346f);
            }
        }
        if (zivot <= 0)
        {
            dogadajiNakonSmrti.SetActive(true);
            dogadajiNakonSmrti.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 1);
            if (premaDesnojStraniGleda == -1)
            {
                dogadajiNakonSmrti.transform.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            Destroy(this.gameObject);
        }
    }


    private IEnumerator nasumicniNapadi(int trenutniBroj)
    {
        if (vitez.activeInHierarchy)
        {
            yield return new WaitForSeconds(0.1f);
            float nasumicniBrojCekanja = UnityEngine.Random.Range(1f, 2f);
            yield return new WaitForSeconds(nasumicniBrojCekanja);
            int nasumicniBroj = (int)UnityEngine.Random.Range(1f, 4f);
                if (trenutniBroj == premaDesnojStraniGleda && !izvodiNapad)
                {
                    izvodiNapad = true;
                    switch (nasumicniBroj)
                    {
                        case 1:
                            StartCoroutine(napadKotrljanja(trenutniBroj));
                            break;

                        case 2:
                            StartCoroutine(napadStitom(trenutniBroj));
                            break;

                        case 3:
                            if (novaBrzina != moveSpeed)
                            {
                                StartCoroutine(napadStitom(trenutniBroj));
                            }
                            else
                            {
                                StartCoroutine(napadPozivanjaLeda(trenutniBroj));
                            }
                            break;
                    }
                }           
        }
    }

    private IEnumerator napadMacem(int trenutniBroj)
    {
        yield return new WaitForSeconds(0.35f);

        this.transform.Find("macGlavnog").gameObject.GetComponent<BoxCollider2D>().enabled = false;
        animacija.SetTrigger("stani");
        izvodiNapad = false;
        yield return new WaitForSeconds(0.1f);
        if (trenutniBroj == premaDesnojStraniGleda) //to se koristi tak da zna jel se igrač i dalje nalazi na onoj stranu u koju ovaj lik gleda
        {
            StartCoroutine("nasumicniNapadi", premaDesnojStraniGleda);
        }
    }

    private IEnumerator napadKotrljanja(int trenutniBroj)
    {
        //izvodiNapad = true;
    
        yield return new WaitForSeconds(0.7f);
        var napadPocetnaVatra = Instantiate(vatra, this.transform.position, Quaternion.identity);
        napadPocetnaVatra.gameObject.GetComponent<AudioSource>().enabled = true;
        napadPocetnaVatra.gameObject.transform.localRotation = Quaternion.Euler(0, 0, -90);
        napadPocetnaVatra.gameObject.transform.Translate(0f, -2f, 0f);
        StartCoroutine("povecajVatru", napadPocetnaVatra.gameObject);
        Destroy(napadPocetnaVatra, 3.5f);

        bjezi = true;
        animacija.SetTrigger("bjezi");

        yield return new WaitForSeconds(1f);
        bjezi = false;
        animacija.SetBool("bjezi", false);
        kotrljaSe = true;
        this.transform.Find("vatraKodKotrljanja").gameObject.GetComponent<SpriteRenderer>().enabled = true;
        this.transform.Find("vatraKodKotrljanja").gameObject.GetComponent<AudioSource>().enabled = true;
        this.transform.Find("vatraKodKotrljanja").gameObject.GetComponent<Animator>().enabled = true;
        animacija.SetTrigger("kotrljajSe");

        yield return new WaitForSeconds(2f);
        kotrljaSe = false;

        this.transform.Find("vatraKodKotrljanja").gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.transform.Find("vatraKodKotrljanja").gameObject.GetComponent<AudioSource>().enabled = false;
        this.transform.Find("vatraKodKotrljanja").gameObject.GetComponent<Animator>().enabled = false;
        animacija.SetTrigger("stani");
        rigidGlavnog.velocity = new Vector2(0f, 0f);

        izvodiNapad = false;
        yield return new WaitForSeconds(0.1f);
        if (trenutniBroj == premaDesnojStraniGleda)
        {
            StartCoroutine("nasumicniNapadi", premaDesnojStraniGleda);
        }

    }

    private IEnumerator povecajVatru(GameObject vatra)
    {
        while (vatra.transform.localScale.x < 2.5) {
            vatra.transform.localScale = new Vector2(vatra.transform.localScale.x+0.1f, vatra.transform.localScale.y);
            yield return new WaitForSeconds(0.08f);
                }
    }

    private IEnumerator napadStitom(int trenutniBroj)
    {
       // izvodiNapad = true;
        smjerGrmljavine = trenutniBroj;

        zvukovi.pokreniZvuk("zvukPripremeZaGrmljavinu");
        yield return new WaitForSeconds(1.2f);
        rigidGlavnog.velocity = new Vector2(0f, 0f);
        animacija.SetBool("koristiStit", true);
        neStitiSe = false;

        var napadGrmljavina = Instantiate(grmljavina, this.transform.position, Quaternion.identity);

        Destroy(napadGrmljavina, 4f);
        zvukovi.pokreniZvuk("zvukGrmljavine");

        yield return new WaitForSeconds(2f);
        animacija.SetTrigger("stani");


        neStitiSe = true;
        izvodiNapad = false;
        yield return new WaitForSeconds(0.1f);
        if (trenutniBroj == premaDesnojStraniGleda)
        {
            StartCoroutine("nasumicniNapadi", premaDesnojStraniGleda);
        }
    }

    private IEnumerator napadPozivanjaLeda(int trenutniBroj)
    {
        //izvodiNapad = true;

        this.transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 82f, 1f);

        animacija.SetTrigger("pozoviMecavu");
        yield return new WaitForSeconds(0.5f);
        rigidGlavnog.velocity = new Vector2(0f, 0f);
        animacija.SetBool("hodaj", false);  //nadodano
        var nizLed = new GameObject[20];

        for (int i = 0; i < 20; i++)
        {
            float nasumicnaVisinaLeda = UnityEngine.Random.Range(-3f, 3f);
            nizLed[i] = Instantiate(led, this.transform.position, Quaternion.identity);
            nizLed[i].transform.gameObject.transform.localPosition = new Vector2(this.transform.localPosition.x - 50 + (float)5.5 * i, this.transform.localPosition.y+ 20 + nasumicnaVisinaLeda);
            Destroy(nizLed[i], 3.5f);
        }
        nizLed[10].transform.gameObject.GetComponent<AudioSource>().enabled = true;
        StartCoroutine("glasnocaZvukaLeda", nizLed[10].transform.gameObject);


        yield return new WaitForSeconds(2f);
        this.GetComponent<SpriteRenderer>().color = new Color(0.85f, 0.12f, 0.12f);
        animacija.SetTrigger("stani");


        izvodiNapad = false;
        yield return new WaitForSeconds(0.1f);
        if (trenutniBroj == premaDesnojStraniGleda)
        {
            StartCoroutine("nasumicniNapadi", premaDesnojStraniGleda);
        }
    }

    private IEnumerator glasnocaZvukaLeda(GameObject led)
    {
        led.gameObject.GetComponent<AudioSource>().volume = 0.05f;
        yield return new WaitForSeconds(0.5f);
        led.gameObject.GetComponent<AudioSource>().volume = 0.1f;
        yield return new WaitForSeconds(0.5f);
        led.gameObject.GetComponent<AudioSource>().volume = 0.15f;
        yield return new WaitForSeconds(0.5f);
        led.gameObject.GetComponent<AudioSource>().volume = 0.1f;
        yield return new WaitForSeconds(0.5f);
        led.gameObject.GetComponent<AudioSource>().volume = 0.02f;
        yield return new WaitForSeconds(0.5f);
    }

    public void vratiSeNaPocetnuPoziciju()
    {
        pozicijaIgracaDesno = true;
        promjenjenaStranaLijevo = false;

        promjenjenaStranaDesno = true;
        izvodiNapad = false;
        bjezi = false;
        kotrljaSe = false;
        smjerGrmljavine = 1;
        akceleracija = 1;
        premaDesnojStraniGleda = 1;
        neStitiSe = true;

        if (moveSpeed == novaBrzina)
        {
            moveSpeed -= (float)2.3;
        }

        this.transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);

        this.transform.Find("macGlavnog").gameObject.GetComponent<BoxCollider2D>().enabled = false;
        zivot = punZivot;
        this.transform.position = new Vector2(pocetnaPozicijaX, pocetnaPozicijaY);
    }
}
