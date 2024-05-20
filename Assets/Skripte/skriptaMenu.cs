using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skriptaMenu : MonoBehaviour
{
    public static bool igraJePauzirana = false;

    public GameObject menuPauziran;

    bool mapa;
    bool postavke;
    bool glavniMenu;
    bool nastavi;
    bool izadiVan;

    public GameObject glavniMenuGameObject;
    public GameObject mapaGameObject;
    public GameObject postavkeGameObject;
    public GameObject canvasGameOver;
    public GameObject prijelazIzmeduScena;

    public GameObject tekstNastavi;
    public GameObject tekstIzadi;
    public GameObject tekstNastaviPodebljano;
    public GameObject tekstIzadiPodebljano;

    private AudioSource[] sviAudio;
    public AudioSource glavnaGlazba;
    float[] sviAudioVolume;

    bool stisavaSe;

    private void Start()
    {
        mapa = false;
        postavke = false;
        glavniMenu = false;
        nastavi = true;
        izadiVan = false;
        stisavaSe = false;
    }

    void Update()
    {
        if (!canvasGameOver.activeInHierarchy || izadiVan)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (igraJePauzirana)
                {
                    int i = 0;
                    foreach (AudioSource aud in sviAudio)
                    {
                        if (aud != null)
                        {
                            aud.volume = sviAudioVolume[i];
                        }
                        i++;

                    }

                    zvukovi.pokreniZvuk("zvukPapira");
                    nastaviSIgrom();
                }
                else
                {
                    if(glavnaGlazba.volume < 0.1)
                    {
                        stisavaSe = true;
                    }
                    else
                    {
                        stisavaSe = false;
                    }

                    AudioSource[] audios = FindObjectsOfType(typeof(AudioSource)) as AudioSource[]; //sve se audie stisa, pospremi se koliko su bili glasni, posle se opet aktiviraju s tom glasnocom
                    sviAudio = audios;
                    int broj = int.Parse(audios.Length.ToString());
                    float[] audioVolume = new float[broj];
                    int i = 0;
                    foreach (AudioSource aud in audios)
                    {
                        audioVolume[i] = aud.volume;
                        aud.volume = 0.1f;
                        i++;

                    }
                    if (stisavaSe)
                    {
                        glavnaGlazba.volume = 0f; ;
                    }
                    sviAudioVolume = audioVolume;

                    zvukovi.pokreniZvuk("zvukPapira");
                    Pauziraj();
                }
            }

            if (igraJePauzirana && glavniMenu)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    zvukovi.pokreniZvuk("zvukPapira");
                    glavniMenu = false;
                    mapa = true;
                }
                else if (igraJePauzirana && glavniMenu)
                {
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        zvukovi.pokreniZvuk("zvukPapira");
                        glavniMenu = false;
                        postavke = true;
                    }
                }
            }

            if (igraJePauzirana && mapa)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    zvukovi.pokreniZvuk("zvukPapira");
                    glavniMenu = true;
                    mapa = false;
                }
            }

            if (igraJePauzirana && postavke)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    zvukovi.pokreniZvuk("zvukPapira");
                    glavniMenu = true;
                    postavke = false;
                }
                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    zvukovi.pokreniZvuk("zvukGameOverMaca");
                    nastavi = !nastavi;
                    if (nastavi)
                    {
                        tekstNastavi.SetActive(false);
                        tekstNastaviPodebljano.SetActive(true);
                        tekstIzadiPodebljano.SetActive(false);
                        tekstIzadi.SetActive(true);
                    }

                    if (!nastavi)
                    {
                        tekstNastavi.SetActive(true);
                        tekstNastaviPodebljano.SetActive(false);
                        tekstIzadiPodebljano.SetActive(true);
                        tekstIzadi.SetActive(false);
                    }
                }
                if(Input.GetKeyDown(KeyCode.Return))
                {
                    if (nastavi)
                    {
                        int i = 0;
                        foreach (AudioSource aud in sviAudio)
                        {
                            if (aud != null)
                            {
                                aud.volume = sviAudioVolume[i];
                            }
                            i++;

                        }

                        zvukovi.pokreniZvuk("zvukPapira");

                        nastaviSIgrom();
                    }
                    else
                    {
                        mapa = false;
                        postavke = false;
                        glavniMenu = false;
                        menuPauziran.SetActive(false);
                        igraJePauzirana = false;
                        izadiVan = false;
                        AudioListener.volume = 0f;
                        Time.timeScale = 1f;                      
                        prijelazIzmeduScena.GetComponent<skriptaZaPrijelaz>().moraIzaciIzIgre = true;
                    }
                }
            }

            if (glavniMenu)
            {
                glavniMenuGameObject.SetActive(true);
                mapaGameObject.SetActive(false);
                postavkeGameObject.SetActive(false);
            }

            if (mapa)
            {
                glavniMenuGameObject.SetActive(false);
                mapaGameObject.SetActive(true);
                postavkeGameObject.SetActive(false);
            }

            if (postavke)
            {
                glavniMenuGameObject.SetActive(false);
                mapaGameObject.SetActive(false);
                postavkeGameObject.SetActive(true);
            }
        }

    }

    private void nastaviSIgrom()
    {
        mapa = false;
        postavke = false;
        glavniMenu = false;
        menuPauziran.SetActive(false);
        Time.timeScale = 1f;
        igraJePauzirana = false;
    }
    private void Pauziraj()
    {
        glavniMenu = true;
        menuPauziran.SetActive(true);
        Time.timeScale = 0f;
        igraJePauzirana = true;
    }

}
