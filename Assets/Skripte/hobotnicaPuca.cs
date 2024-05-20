using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hobotnicaPuca : MonoBehaviour
{
    public GameObject pucanje;
    public GameObject vitez;
    bool mozePucat;
    public float trajanjeProjektila;
    public float vremenskiRazmakSljececiProjektil;

    void Start()
    {
        vitez = GameObject.Find("Igrac");
        mozePucat = true;
    }


    void Update()
    {
        if (this.GetComponent<BoxCollider2D>().IsTouching(vitez.gameObject.GetComponent<BoxCollider2D>()))
        {
            if (vitez.gameObject.name == "Igrac")
            {
                if (this.gameObject.tag == "kamenaGlava" && mozePucat)
                {
                    pucajLaser();
                }
                else if (this.transform.parent.gameObject.tag == "protivnikHobotnica" && mozePucat)
                {
                    if (this.transform.parent != null)
                    {
                        if (this.transform.parent.GetComponent<neprijateljHobotnicaAI>().idiLijevo) //stvar je u tome da onaj Quaternion rotira sve pa je zato obrnuto ovaj drugi dio sljedeceg if-a, prvo si napisal ova dva ifa kod prvog else-a
                        {

                            if (this.transform.parent.position.x < vitez.transform.position.x && this.transform.parent.localScale.x < 0 && mozePucat)   // vitez se nalazi levo od hobotnice
                            {
                                pucajOtrov();
                            }
                            else if (this.transform.parent.position.x > vitez.transform.position.x && this.transform.parent.localScale.x >= 0 && mozePucat)
                            {
                                pucajOtrov();
                            }
                        }
                        else
                        {
                            if (this.transform.parent.position.x < vitez.transform.position.x && this.transform.parent.localScale.x >= 0 && mozePucat)   // vitez se nalazi levo od hobotnice
                            {
                                pucajOtrov();
                            }
                            else if (this.transform.parent.position.x > vitez.transform.position.x && this.transform.parent.localScale.x < 0 && mozePucat)
                            {
                                pucajOtrov();
                            }
                        }
                    }
                }
            }
        }
    }

    private void pucajOtrov()
    {
        mozePucat = false;
        var otrov = Instantiate(pucanje, this.transform.parent.position, Quaternion.identity);
        Destroy(otrov, trajanjeProjektila);
        zvukovi.pokreniZvuk("zvukHobotniceKojaPuca");
        Invoke("vremenskiRazmakPucanja", vremenskiRazmakSljececiProjektil);
    }

    private void pucajLaser()
    {
        mozePucat = false;
        var laser = Instantiate(pucanje, this.transform.position, Quaternion.identity);
        laser.transform.Translate(5f, -1.3f, 0f);
        laser.transform.gameObject.GetComponent<AudioSource>().enabled = true;
        Destroy(laser, trajanjeProjektila);
        Invoke("vremenskiRazmakPucanja", vremenskiRazmakSljececiProjektil);
    }

    private void vremenskiRazmakPucanja()
    {
        mozePucat = true;
    }
}
