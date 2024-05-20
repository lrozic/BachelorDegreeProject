using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class skriptaViteza : MonoBehaviour
{
    private Animator animacija;

    public float brzina;
    public float silaSkakanja;
    private float moveInput;

    private new Rigidbody2D rigidbody;

    public bool stojiNaZemlji;
    public Transform tloLika;
    public float provjeraRadiusa;
    public LayerMask stoJeTlo;

    private int brojSkakanja;
    public int brojSkakanjaVrijednost;

    bool dodirujeIspred;
    public Transform ispredTransform;
    bool sklizeSe;
    public float brzinaKlizanjaPoZidu;

    public int trenutniBrojZivota;
    public int ukupanBrojZivota;
    bool besmrtan;
    private bool mozeNapasti;
    public bool mozeNapastiNakonOzljedeVarijabla;

    public Image[] zivoti;
    public Sprite punoSrce;
    public Sprite praznoSrce;

    private int expBodovi;
    public int expZasljedeciLevel;

    public GameObject particleEffect;

    public GameObject levelUp;
    public GameObject checkpoint;

    private bool pricekajLevelUp;

    public GameObject magijaMaca;
    public float brzinaMagije;

    private bool imaStvarZaVodu;
    public float brzinaUVodi;
    public float silaSkakanjaUVodi;
    public GameObject kapljiceVode;
    public GameObject kapljiceVodePoVitezu;
    public GameObject kapljiceVodeCheckpointa;

    public Text textHP;
    public Text textATTACK;
    public Text textEXP;
    public Text textLEVEL;
    public Text textPostotakSoba;

    public GameObject slikaTextLetecihCizmi;
    public GameObject slikaTextVodeneMedalje;
    public GameObject slikaTextPandzaZaZid;
    public GameObject slikaTextMagicneEnergije;

    public GameObject trenutnaIkonaViteza;

    public GameObject neprijateljUnistavaMost;

    internal bool stoji()
    {
        return stojiNaZemlji;
    }

    private Dictionary<KeyCode, bool> gumbiOmoguceni = new Dictionary<KeyCode, bool>(); //služi za to da se nemože moonwalkati!
    private bool gledaDesno;
    public bool mozeNapastiMagijom;
    public bool imaStvarSkakanjaPoZidu;
    private bool mozeHodatZbogMagije;

    public float zadnjiCheckpointX;
    public float zadnjiCheckpointY;

    public GameObject Kamera;
    public GameObject vitezUmroSprite;

    public GameObject canvasIgra;
    //public GameObject canvasPauza;
    public GameObject canvasGameOver;

    public GameObject glavnaSoba;
    public GameObject gornjaSobaKamenje;
    public GameObject opcionalniDio;
    public GameObject savePrijeGlavnog;
    public GameObject saveVertikalni;
    public GameObject saveVodeni;
    public GameObject sobaDvostrukiSkok;
    public GameObject sobaMost;
    public GameObject sobaNakonMoci;
    public GameObject sobaPocetak;
    public GameObject sobaVodeniDio;
    public GameObject velikaVertikalnaSoba;
    public GameObject vertikalnaGlavni;
    public GameObject vertikalnaSobaKamenje;

    public PhysicsMaterial2D materijalZid;
    public PhysicsMaterial2D materijalPod;

    public GameObject drvo;
    public GameObject efektLisca;
    public int postotakSoba;

    public GameObject cheat1;
    public GameObject cheat2;
    public GameObject cheat3;
    public GameObject cheat4;
    public GameObject cheat5;
    public GameObject cheat6;

    void Start()
    {
        brojSkakanja = brojSkakanjaVrijednost;
        animacija = GetComponent<Animator>();   //to znači da koristimo Animator koji se nalazi u Unity-u preko skripte
        rigidbody = GetComponent<Rigidbody2D>();
        gumbiOmoguceni.Add(KeyCode.LeftArrow, true);
        gumbiOmoguceni.Add(KeyCode.RightArrow, true);

        besmrtan = false;
        mozeNapasti = true;
        expBodovi = 0;
        gledaDesno = true;
        pricekajLevelUp = false;
        mozeNapastiMagijom = false;
        imaStvarSkakanjaPoZidu = false;
        mozeHodatZbogMagije = true;
        imaStvarZaVodu = false;
        mozeNapastiNakonOzljedeVarijabla = true;

        zadnjiCheckpointX = this.transform.position.x;
        zadnjiCheckpointY = this.transform.position.y;

        postotakSoba = 7;
    }

    private void Update()
    {
        //cheat kodovi
        /*if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            this.transform.position = new Vector2(cheat1.transform.position.x, cheat1.transform.position.y);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            this.transform.position = new Vector2(cheat2.transform.position.x, cheat2.transform.position.y);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            this.transform.position = new Vector2(cheat3.transform.position.x, cheat3.transform.position.y);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            this.transform.position = new Vector2(cheat4.transform.position.x, cheat4.transform.position.y);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            this.transform.position = new Vector2(cheat5.transform.position.x, cheat5.transform.position.y);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            this.transform.position = new Vector2(cheat6.transform.position.x, cheat6.transform.position.y);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            trenutniBrojZivota = ukupanBrojZivota;
            zvukovi.pokreniZvuk("zvukPopuniSvaSrca");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            this.GetComponent<igracNapada>().snagaNapada = this.GetComponent<igracNapada>().snagaNapada + 1;
            zvukovi.pokreniZvuk("zvukGameOverMaca");
        }*/


        //dodirivanje drva
        if (this.transform.gameObject.GetComponent<BoxCollider2D>().IsTouching(drvo.GetComponent<Collider2D>()))
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || !stojiNaZemlji)
            {
                drvo.GetComponent<AudioSource>().enabled = true;
                var list = Instantiate(efektLisca, transform.position, efektLisca.transform.rotation);
                Destroy(list, 1.5f);

            }
            else
            {
                drvo.GetComponent<AudioSource>().enabled = false;
            }
        }
        else
        {
            drvo.GetComponent<AudioSource>().enabled = false;
        }

        if (dodirujeIspred || !stojiNaZemlji)
        {
            rigidbody.sharedMaterial = materijalZid;
        }
        else
        {
            rigidbody.sharedMaterial = materijalPod;
        }

        //levelUp
        if (expBodovi >= expZasljedeciLevel && !pricekajLevelUp)
        {
            pricekajLevelUp = true;
            levelUp.SetActive(true);
            StartCoroutine(levelUpEfekt());
            //kada je ukljuceno, možze se setActive(false) stavit, al ak je isključeno, onda ne
            //ono kaj se treba napraviti je napraviti sam GameObject, pospremiti dragAndDropom tam, i onda npr.
            //muzika SetActive

            this.transform.gameObject.GetComponent<AudioSource>().Play();
            Invoke("deaktivirajLvl", 4f);
            expZasljedeciLevel *= 2;
            this.GetComponent<igracNapada>().snagaNapada = this.GetComponent<igracNapada>().snagaNapada + 1;
        }

        //zivoti viteza
        for (int i = 0; i < zivoti.Length; i++)
        {
            if (i < trenutniBrojZivota)  //ak je prvo srce u polju manje od trenutnog sveukupnog zivota, puno srce će bit
            {
                zivoti[i].sprite = punoSrce;
            }
            else
            {
                zivoti[i].sprite = praznoSrce;
            }

            if (i < ukupanBrojZivota)
            {
                zivoti[i].enabled = true;
            }
            else
            {
                zivoti[i].enabled = false;
            }
        }


        if (trenutniBrojZivota > 0)
        {
            if(!(Input.GetKey(KeyCode.LeftArrow)) && !(Input.GetKey(KeyCode.RightArrow))){

            }

            //skakanje viteza
            if (Input.GetKeyDown(KeyCode.Space) && brojSkakanja > 0 && Time.timeScale != 0f)
            {
                //rigidbody.velocity = new Vector2(0f, 0f);
                Debug.Log(rigidbody.velocity.x);
                rigidbody.velocity = Vector2.up * silaSkakanja;
                brojSkakanja--;
                zvukovi.pokreniZvuk("skakanje");
            }

            else if (Input.GetKeyDown(KeyCode.Space) && brojSkakanja == 0 && stojiNaZemlji == true && mozeNapasti && Time.timeScale != 0f)
            {
                //rigidbody.velocity = new Vector2(0f, 0f);
                rigidbody.velocity = Vector2.up * silaSkakanja;
                zvukovi.pokreniZvuk("skakanje");
            }

            if (stojiNaZemlji == true)
            {
                brojSkakanja = brojSkakanjaVrijednost;
            }

            //napad viteza macem
            if (Input.GetKeyDown(KeyCode.Y) && stojiNaZemlji == true && mozeNapasti == true && Time.timeScale != 0f && mozeNapastiNakonOzljedeVarijabla == true)
            {
                mozeNapasti = false;
                animacija.SetTrigger("zamahniMacem");
                zvukovi.pokreniZvuk("napadMacem");
                Invoke("mozeNapastiBool", 0.24f);
                zvukHodanja.pokreniZvuk2("zvukTrcanja", false);
            }

            //napad viteza magijom maca
            if (Input.GetKeyDown(KeyCode.X) && stojiNaZemlji == true && mozeNapastiMagijom == true && Time.timeScale != 0f && mozeNapastiNakonOzljedeVarijabla == true)
            {
                mozeHodatZbogMagije = false;
                mozeNapastiMagijom = false;
                animacija.SetTrigger("zamahniMacem");
                zvukovi.pokreniZvuk("napadMacem");
                StartCoroutine("mozeNapastiMagijomBool");
                zvukHodanja.pokreniZvuk2("zvukTrcanja", false);
                var napadMagijomMaca = Instantiate(magijaMaca, this.transform.position, Quaternion.identity);
                napadMagijomMaca.transform.Translate(0f, 1f, 0f);
                zvukovi.pokreniZvuk("zvukMagijeMaca");
                if (gledaDesno == false)
                {
                    napadMagijomMaca.transform.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(brzinaMagije, 0f);
                }
                else
                {
                    napadMagijomMaca.transform.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-brzinaMagije, 0f);
                    napadMagijomMaca.transform.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                }
                Destroy(napadMagijomMaca, 2.5f);
            }

            //sklizanje i skakanje po zidu
            if (imaStvarSkakanjaPoZidu)
            {
                if (dodirujeIspred == true && stojiNaZemlji == false && (Input.GetKey(KeyCode.RightArrow) == true || Input.GetKey(KeyCode.LeftArrow) == true))   //4:51
                {
                    sklizeSe = true;
                    brojSkakanja = brojSkakanjaVrijednost;
                }

                else
                {
                    sklizeSe = false;
                }

                if (sklizeSe)
                {
                    rigidbody.velocity = new Vector2(rigidbody.velocity.x, Mathf.Clamp(rigidbody.velocity.y, -brzinaKlizanjaPoZidu, float.MaxValue)); //nismo brzinaKlizanjaPoZidu 5:26, brzina klizanja je minimalna po brzinaKlizanjaPoZidu,                                                                                                                                            
                                                                                                                                                      //max more bit kolko god se hoče, a prvo je početna vrednost    
                    float pozicijaX = 0;
                    if (gledaDesno == false)
                    {
                        pozicijaX = transform.position.x + (float)0.6;
                    }
                    else
                    {
                        pozicijaX = transform.position.x - (float)0.6;
                    }
                    float pozicijaY = transform.position.y - (float)0.3;
                    float pozicijaZ = transform.position.z;
                    Vector3 pozicija = new Vector3(pozicijaX, pozicijaY, pozicijaZ);

                    if (rigidbody.velocity.y < 0)
                    {
                        var slidanjeEffect = Instantiate(particleEffect, pozicija, Quaternion.identity);
                        Destroy(slidanjeEffect, (float)0.7);
                    }

                }
            }
        }
        //pauza menu
        textHP.text = $"HP: {trenutniBrojZivota}/{ukupanBrojZivota}";
        textATTACK.text = $"ATTACK: {this.GetComponent<igracNapada>().snagaNapada = this.GetComponent<igracNapada>().snagaNapada}";
        textEXP.text = $"EXP: {expBodovi}/{expZasljedeciLevel}";
        int trenutniLevel = this.GetComponent<igracNapada>().snagaNapada;   //moral napravit posebnu varijablu jer Unity to dvoje poveže
        trenutniLevel -= 1;
        textLEVEL.text = $"LEVEL: {trenutniLevel}";
        textPostotakSoba.text = $"{postotakSoba}%";

    }

    private void mozeNapastiBool()
    {
        mozeNapasti = true;
    }

    private IEnumerator mozeNapastiMagijomBool()
    {
        yield return new WaitForSeconds(0.24f);
        mozeHodatZbogMagije = true;
        yield return new WaitForSeconds(1.76f);
        mozeNapastiMagijom = true;
        zvukovi.pokreniZvuk("zvukSpremanZaMagiju");
        transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0f);
        float plavaBojaVrijednost = 0;
        for (int i = 0; i < 21; i++)
        {
            transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, plavaBojaVrijednost);
            plavaBojaVrijednost += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }

    }


    void FixedUpdate()
    {

        stojiNaZemlji = Physics2D.OverlapCircle(tloLika.position, provjeraRadiusa, stoJeTlo);
        dodirujeIspred = Physics2D.OverlapCircle(ispredTransform.position, provjeraRadiusa, stoJeTlo);

        moveInput = Input.GetAxis("Horizontal");    // desna strela = 1, leva strela = -1, GetAxisRow, velko slovo Horizontal

        rigidbody.velocity = new Vector2(moveInput * brzina, rigidbody.velocity.y);

        if (mozeNapasti == false || mozeHodatZbogMagije == false)
        {
            rigidbody.velocity = new Vector2(0, 0);
        }

        else if(trenutniBrojZivota > 0)
        {

            if (Input.GetKey(KeyCode.LeftArrow) && gumbiOmoguceni[KeyCode.LeftArrow])
            {

                gumbiOmoguceni[KeyCode.RightArrow] = false;
                gumbiOmoguceni[KeyCode.LeftArrow] = true;

                moveInput = -1;
                rigidbody.velocity = new Vector2(moveInput * brzina, rigidbody.velocity.y);

                if (stojiNaZemlji == true && sklizeSe == false)
                {
                    zvukHodanja.pokreniZvuk2("zvukTrcanja", true);
                    animacija.ResetTrigger("hodaNakonPada"); //nadodano
                }
                else if (stojiNaZemlji == false && sklizeSe == false)
                {
                    zvukHodanja.pokreniZvuk2("zvukTrcanja", false);
                }
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                gledaDesno = true;
                animacija.ResetTrigger("hodaNakonPada"); //nadodano
                animacija.SetBool("vitezHoda", true);

            }

            else if (Input.GetKey(KeyCode.RightArrow) && gumbiOmoguceni[KeyCode.RightArrow])
            {
                gumbiOmoguceni[KeyCode.RightArrow] = true;
                gumbiOmoguceni[KeyCode.LeftArrow] = false;

                moveInput = 1;
                    rigidbody.velocity = new Vector2(moveInput * brzina, rigidbody.velocity.y);

                if (stojiNaZemlji == true)
                {
                    zvukHodanja.pokreniZvuk2("zvukTrcanja", true);
                    animacija.ResetTrigger("hodaNakonPada"); //nadodano
                }
                else
                {
                    zvukHodanja.pokreniZvuk2("zvukTrcanja", false);
                }
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                gledaDesno = false;
                animacija.ResetTrigger("hodaNakonPada"); //nadodano
                animacija.SetBool("vitezHoda", true);

            }

            else
            {
                /*if(rigidbody.velocity.y > -0.1f && rigidbody.velocity.y < 0.1f)
                 {
                     Debug.Log("ulazi");
                     rigidbody.velocity = new Vector2(0f, 0f);
                 }*/
                animacija.ResetTrigger("hodaNakonPada"); //nadodano
                animacija.SetBool("vitezHoda", false);
            }

            if (!Input.GetKey(KeyCode.RightArrow))
            {
                gumbiOmoguceni[KeyCode.LeftArrow] = true;
            }
            if (!Input.GetKey(KeyCode.LeftArrow))
            {
                gumbiOmoguceni[KeyCode.RightArrow] = true;
            }

            if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                zvukHodanja.pokreniZvuk2("zvukTrcanja", false);
            }

            if (stojiNaZemlji == false && rigidbody.velocity.y <= 0)
            {
                animacija.SetTrigger("pada");
                animacija.ResetTrigger("skoci");
            }

            if (stojiNaZemlji == true && !(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
            {
                animacija.SetTrigger("hodaNakonPada"); //nadodano
                animacija.SetTrigger("padaZaStati");
                animacija.ResetTrigger("pada");
            }

            if (stojiNaZemlji == true && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
            {
                animacija.SetTrigger("hodaNakonPada");
                animacija.ResetTrigger("pada");
                animacija.ResetTrigger("padaZaStati");
            }

            if (stojiNaZemlji == false && rigidbody.velocity.y < 0 && Input.GetKeyDown(KeyCode.Space))
            {
                animacija.SetTrigger("skoci");
                animacija.ResetTrigger("hodaNakonPada");
                animacija.ResetTrigger("padaZaStati");
            }

            if (stojiNaZemlji == false && rigidbody.velocity.y > 0.4)   // prije je bilo veće od nule, sad veće od 0.4
            {
                animacija.SetTrigger("skoci");
                animacija.ResetTrigger("hodaNakonPada");
                animacija.ResetTrigger("padaZaStati");
            }

            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                zvukHodanja.pokreniZvuk2("zvukTrcanja", false);
            }
        }

        if (stojiNaZemlji)
        {
            rigidbody.gravityScale = 5f;
        }
        else
        {
            rigidbody.gravityScale = 2.5f;
        }
    }

    public void smanjiZivotIgraca(int smanjenZivot)
    {
        if (besmrtan == false)
        {
            mozeNapastiNakonOzljedeVarijabla = false;
            trenutniBrojZivota = trenutniBrojZivota - smanjenZivot;
            besmrtan = true;

            if (trenutniBrojZivota <= 0)
            {
                animacija.SetTrigger("ranjen");
                Invoke("idiNaGameOverScreen", 0.25f);
            }
            else
            {
                animacija.SetTrigger("ranjen");
                zvukovi.pokreniZvuk("zvukUdarenogViteza");
                StartCoroutine(transparentnost());
                Invoke("vratiSmrtnost", 1.5f);
                Invoke("mozeNapastiNakonOzljede", 0.30f);
            }
        }
    }

    private void idiNaGameOverScreen()
    {
        canvasIgra.SetActive(false);
        canvasGameOver.SetActive(true);

        Kamera.transform.gameObject.GetComponent<CinemachineBrain>().enabled = false;   //koristeci cinemachine imal problem da je gameover znal biti prelevo i tak
        Kamera.transform.position = new Vector3(-7.13f, 352.5f, -10f);
        //Follow = vitezUmroSprite.transform;
        vitezUmroSprite.SetActive(true);
        this.transform.position = new Vector2(zadnjiCheckpointX, zadnjiCheckpointY);
        besmrtan = false;
        this.transform.gameObject.SetActive(false);
    }

    private IEnumerator transparentnost()
    {
        yield return new WaitForSeconds(0.4f);

        for (int i = 0; i < 20; i++)
        {
            if ((i % 2) == 0)
            {
                this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f);
            }
            else
            {
                this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator levelUpEfekt()
    {
        double trenutnaBoja = 0;
        bool vratiBoju = false;

        for (int i = 0; i < 30; i++)
        {
            if (!vratiBoju)
            {
                levelUp.GetComponent<Text>().color = new Color(0f, (float)trenutnaBoja, 0f);
                trenutnaBoja += 0.1;
            }

            if (vratiBoju)
            {
                levelUp.GetComponent<Text>().color = new Color(0f, (float)trenutnaBoja, 0f);
                trenutnaBoja -= 0.1;
            }

            if (trenutnaBoja > 1)
            {
                vratiBoju = true;
            }

            if (trenutnaBoja < 0.2)
            {
                vratiBoju = false;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void vratiSmrtnost()
    {
        besmrtan = false;
    }

    private void mozeNapastiNakonOzljede()
    {
        mozeNapastiNakonOzljedeVarijabla = true;
    }

    public void dodajExpBodove(int bodovi)
    {
        expBodovi += bodovi;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(tloLika.position, provjeraRadiusa);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(ispredTransform.position, provjeraRadiusa);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "voda")
        {
            var efektVode = Instantiate(kapljiceVode, this.transform.position, Quaternion.identity);
            Destroy(efektVode, 1.5f);
            collision.transform.gameObject.GetComponent<AudioSource>().enabled = true;
            if (!imaStvarZaVodu)
            {
                brzina = brzina - brzinaUVodi;
                silaSkakanja = silaSkakanja - silaSkakanjaUVodi;
            }
        }

        if(collision.tag == "malaVoda")
        {
            var efektVode = Instantiate(kapljiceVodePoVitezu, this.transform.position, Quaternion.identity);
            efektVode.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 0.6f);
            Destroy(efektVode, 1f);
            zvukovi.pokreniZvuk("zvukVodeIzlazak");
        }

        if (collision.tag == "novoSrce")
        {
            ukupanBrojZivota += 1;
            trenutniBrojZivota = ukupanBrojZivota;
            Destroy(collision.gameObject);
            zvukovi.pokreniZvuk("zvukNovogSrca");
        }

        if (collision.tag == "led")
        {
            smanjiZivotIgraca(2);
        }

        if (collision.tag == "mac" || collision.tag == "macKostura")
        {
            smanjiZivotIgraca(2);
        }

        if (collision.tag == "stvarMagijaMaca")
        {
            slikaTextMagicneEnergije.SetActive(true);
            mozeNapastiMagijom = true;
            collision.transform.gameObject.GetComponent<AudioSource>().enabled = true;
            collision.transform.gameObject.GetComponent<AudioSource>().volume = 0.3f;
            collision.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(collision.gameObject, 2f);
        }

        if (collision.tag == "stvarDvostrukiSkok")
        {
            slikaTextLetecihCizmi.SetActive(true);
            brojSkakanjaVrijednost++;
            zvukovi.pokreniZvuk("zvukStvariSkakanje");
            Destroy(collision.gameObject);
        }


        if (collision.tag == "stvarSkakanjePoZidu")
        {
            slikaTextPandzaZaZid.SetActive(true);
            imaStvarSkakanjaPoZidu = true;
            zvukovi.pokreniZvuk("zvukStvariSkakanje");
            Destroy(collision.gameObject);
            neprijateljUnistavaMost.SetActive(true);
        }

        if (collision.tag == "stvarZaKretanjeUVodi")
        {
            slikaTextVodeneMedalje.SetActive(true);
            imaStvarZaVodu = true;
            zvukovi.pokreniZvuk("zvukStvariSkakanje");
            Destroy(collision.gameObject);
        }

        if (collision.tag == "checkpoint")
        {
            if (trenutniBrojZivota != ukupanBrojZivota)
            {
                trenutniBrojZivota = ukupanBrojZivota;
                zvukovi.pokreniZvuk("zvukPopuniSvaSrca");
            }
            checkpoint.SetActive(true);
            var efektVodeCheckpointa = Instantiate(kapljiceVodeCheckpointa, this.transform.position, Quaternion.identity);
            Destroy(efektVodeCheckpointa, 1.5f);
            collision.transform.gameObject.GetComponent<AudioSource>().enabled = false;
            collision.transform.gameObject.GetComponent<AudioSource>().enabled = true;
            zadnjiCheckpointX = collision.transform.position.x;
            zadnjiCheckpointY = collision.transform.position.y;
        }

        if (collision.transform.gameObject.name == "glavnaSobaC")
        {
            if(glavnaSoba.GetComponent<Image>().enabled == false)
            {
                postotakSoba += 7;
                glavnaSoba.GetComponent<Image>().enabled = true;
            }
            smjestiIkonu(glavnaSoba.transform.GetChild(0).gameObject);

        }

        if (collision.transform.gameObject.name == "gornjaSobaKamenjeC")
        {
            if (gornjaSobaKamenje.GetComponent<Image>().enabled == false)
            {
                postotakSoba += 9;
                gornjaSobaKamenje.GetComponent<Image>().enabled = true;
            }
            smjestiIkonu(gornjaSobaKamenje.transform.GetChild(0).gameObject);
        }

        if (collision.transform.gameObject.name == "opcionalniDioC")
        {
            if(opcionalniDio.GetComponent<Image>().enabled == false)
            {
                postotakSoba += 9;
                opcionalniDio.GetComponent<Image>().enabled = true;
            }
            smjestiIkonu(opcionalniDio.transform.GetChild(0).gameObject);
        }

        if (collision.transform.gameObject.name == "savePrijeGlavnogC")
        {
            if(savePrijeGlavnog.GetComponent<Image>().enabled == false)
            {
                postotakSoba += 3;
                savePrijeGlavnog.GetComponent<Image>().enabled = true;
            }
            smjestiIkonu(savePrijeGlavnog.transform.GetChild(0).gameObject);
        }

        if (collision.transform.gameObject.name == "saveVertikalniC")
        {
            if(saveVertikalni.GetComponent<Image>().enabled == false)
            {
                postotakSoba += 3;
                saveVertikalni.GetComponent<Image>().enabled = true;
            }
            smjestiIkonu(saveVertikalni.transform.GetChild(0).gameObject);
        }

        if (collision.transform.gameObject.name == "saveVodeniC")
        {
            if(saveVodeni.GetComponent<Image>().enabled == false)
            {
                postotakSoba += 3;
                saveVodeni.GetComponent<Image>().enabled = true;
            }
            smjestiIkonu(saveVodeni.transform.GetChild(0).gameObject);
        }

        if (collision.transform.gameObject.name == "sobaDvostrukiSkokC")
        {
            if(sobaDvostrukiSkok.GetComponent<Image>().enabled == false)
            {
                postotakSoba += 7;
                sobaDvostrukiSkok.GetComponent<Image>().enabled = true;
            }
            smjestiIkonu(sobaDvostrukiSkok.transform.GetChild(0).gameObject);
        }

        if (collision.transform.gameObject.name == "sobaMostC")
        {
            if(sobaMost.GetComponent<Image>().enabled == false)
            {
                postotakSoba += 9;
                sobaMost.GetComponent<Image>().enabled = true;
            }
            smjestiIkonu(sobaMost.transform.GetChild(0).gameObject);
        }

        if (collision.transform.gameObject.name == "sobaNakonMociC")
        {
            if(sobaNakonMoci.GetComponent<Image>().enabled == false)
            {
                postotakSoba += 10;
                sobaNakonMoci.GetComponent<Image>().enabled = true;
            }
            smjestiIkonu(sobaNakonMoci.transform.GetChild(0).gameObject);
        }

        if (collision.transform.gameObject.name == "sobaPocetakC")
        {
            sobaPocetak.GetComponent<Image>().enabled = true;
            if(trenutnaIkonaViteza == null)
            {
                trenutnaIkonaViteza = sobaPocetak.transform.GetChild(0).gameObject;
            }
            smjestiIkonu(sobaPocetak.transform.GetChild(0).gameObject);

        }

        if (collision.transform.gameObject.name == "sobaVodeniDioC")
        {
            if(sobaVodeniDio.GetComponent<Image>().enabled == false)
            {
                postotakSoba += 10;
                sobaVodeniDio.GetComponent<Image>().enabled = true;
            }
            smjestiIkonu(sobaVodeniDio.transform.GetChild(0).gameObject);

        }

        if (collision.transform.gameObject.name == "velikaVertikalnaSobaC")
        {
            if(velikaVertikalnaSoba.GetComponent<Image>().enabled == false)
            {
                postotakSoba += 11;
                velikaVertikalnaSoba.GetComponent<Image>().enabled = true;
            }
            smjestiIkonu(velikaVertikalnaSoba.transform.GetChild(0).gameObject);
        }

        if (collision.transform.gameObject.name == "vertikalnaGlavniC")
        {
            if(vertikalnaGlavni.GetComponent<Image>().enabled == false)
            {
                postotakSoba += 8;
                vertikalnaGlavni.GetComponent<Image>().enabled = true;
            }
            smjestiIkonu(vertikalnaGlavni.transform.GetChild(0).gameObject);
        }

        if (collision.transform.gameObject.name == "vertikalnaSobaKamenjeC")
        {
            if (vertikalnaSobaKamenje.GetComponent<Image>().enabled == false) 
            {
                postotakSoba += 4;
                vertikalnaSobaKamenje.GetComponent<Image>().enabled = true;
            }
            smjestiIkonu(vertikalnaSobaKamenje.transform.GetChild(0).gameObject);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "voda")
        {
            var efektVodePoVitezu = Instantiate(kapljiceVodePoVitezu, this.transform.position, Quaternion.identity);
            Destroy(efektVodePoVitezu, 1.5f);
            collision.transform.gameObject.GetComponent<AudioSource>().enabled = false;
            zvukovi.pokreniZvuk("zvukVodeIzlazak");
            if (!imaStvarZaVodu)
            {
                brzina = brzina + brzinaUVodi;
                silaSkakanja = silaSkakanja + silaSkakanjaUVodi;
            }
        }

        if (collision.tag == "malaVoda")
        {
            var efektVode = Instantiate(kapljiceVodePoVitezu, this.transform.position, Quaternion.identity);
            efektVode.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 0.6f);
            Destroy(efektVode, 1f);
            zvukovi.pokreniZvuk("zvukVodeIzlazak");
        }


            if (collision.tag == "checkpoint")
        {
            checkpoint.SetActive(false);
            var efektVodeCheckpointa = Instantiate(kapljiceVodeCheckpointa, this.transform.position, Quaternion.identity);
            Destroy(efektVodeCheckpointa, 1.5f);
            collision.transform.gameObject.GetComponent<AudioSource>().enabled = false;
            collision.transform.gameObject.GetComponent<AudioSource>().enabled = true;        
        }

    }

    private void OnTriggerStay2D(Collider2D collision)  //jer bi samo jednom damage dobil
    {
        if (collision.transform.gameObject.tag == "opasno")
        {
            smanjiZivotIgraca(1);

        }

        if (collision.transform.gameObject.tag == "vatra")
        {
            smanjiZivotIgraca(1);
        }
    }

    private void deaktivirajLvl()
    {
        levelUp.SetActive(false);
        pricekajLevelUp = false;        
    }

    private void smjestiIkonu(GameObject novaIkona)
    {
        trenutnaIkonaViteza.SetActive(false);
        trenutnaIkonaViteza = novaIkona;
        trenutnaIkonaViteza.SetActive(true);
    }

}
