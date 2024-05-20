using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zvukovi : MonoBehaviour
{

    public static AudioClip zvukSkakanja, zvukMaca, zvukUmiranjaRaka, zvukUdarenogNeprijatelja, zvukUdarenogViteza,
        zvukLeteciPada, zvukSlidanjaPoZidu, zvukHobotniceKojaPuca, zvukNovogSrca, zvukLevelUp, zvukNoveStvari, zvukHobotnice,
        zvukGrmljavine, zvukPripremeZaGrmljavinu, zvukUdarenogStita, zvukOzljedeniGlavni, zvukUdarenogKostura, zvukMagijeMaca, zvukDrakule, zvukMetroida,
        zvukSpremanZaMagiju, zvukStvariMagijeMaca, zvukStvariSkakanje, zvukVodeUlazak, zvukVodeIzlazak, zvukPopuniSvaSrca, zvukGameOverMaca, zvukPapira;

    static AudioSource izvorSoundEffecta;

    void Start()
    {
        zvukSkakanja = Resources.Load<AudioClip>("docekatiSeNaTlo");
        zvukMaca = Resources.Load<AudioClip>("napadMacem");
        zvukUmiranjaRaka = Resources.Load<AudioClip>("rakUmire");
        zvukUdarenogNeprijatelja = Resources.Load<AudioClip>("udarenNeprijatelj");
        zvukUdarenogViteza = Resources.Load<AudioClip>("zvukUdarenogViteza");
        zvukLeteciPada = Resources.Load<AudioClip>("leteciPada");
        zvukSlidanjaPoZidu = Resources.Load<AudioClip>("wallJump");
        zvukHobotniceKojaPuca = Resources.Load<AudioClip>("zvukHobotniceKojaPuca");
        zvukNovogSrca = Resources.Load<AudioClip>("zvukNovogSrca");
        zvukLevelUp = Resources.Load<AudioClip>("levelUp");
        zvukNoveStvari = Resources.Load<AudioClip>("novaStvar");
        zvukHobotnice = Resources.Load<AudioClip>("zvukHobotnice");
        zvukGrmljavine = Resources.Load<AudioClip>("grmljavina");
        zvukPripremeZaGrmljavinu = Resources.Load<AudioClip>("pripremaZagrmljavinu");
        zvukUdarenogStita = Resources.Load<AudioClip>("udarenStit"); 
        zvukOzljedeniGlavni = Resources.Load<AudioClip>("ozljedeniGlavni");
        zvukUdarenogKostura = Resources.Load<AudioClip>("zvukUdarenogKostura");
        zvukMagijeMaca = Resources.Load<AudioClip>("soundEffectMagijeMaca");
        zvukDrakule = Resources.Load<AudioClip>("zvukDrakule");
        zvukSpremanZaMagiju = Resources.Load<AudioClip>("soundEffectPunMagije");
        zvukStvariSkakanje = Resources.Load<AudioClip>("zvukStvariSkakanje");
        zvukVodeUlazak = Resources.Load<AudioClip>("zvukVode1");
        zvukVodeIzlazak = Resources.Load<AudioClip>("zvukVode2");
        zvukPopuniSvaSrca = Resources.Load<AudioClip>("popuniSvaSrca");
        zvukGameOverMaca = Resources.Load<AudioClip>("gameOverMac");
        zvukPapira = Resources.Load<AudioClip>("zvukPapira");
        zvukMetroida = Resources.Load<AudioClip>("zvukMetroida");

        izvorSoundEffecta = GetComponent<AudioSource>();
        izvorSoundEffecta.loop = false;
    }

    public static void pokreniZvuk(string naziv)
    {
        izvorSoundEffecta.pitch = 1f;
        izvorSoundEffecta.volume = 1f;

        switch (naziv)
        {         
            case "skakanje":
                izvorSoundEffecta.PlayOneShot(zvukSkakanja);
                break;

            case "napadMacem":
                izvorSoundEffecta.PlayOneShot(zvukMaca);
                break;

            case "rakUmire":
                izvorSoundEffecta.PlayOneShot(zvukUmiranjaRaka);
                break;

            case "udarenNeprijatelj":
                izvorSoundEffecta.PlayOneShot(zvukUdarenogNeprijatelja);
                break;

            case "zvukUdarenogViteza":
                izvorSoundEffecta.PlayOneShot(zvukUdarenogViteza);
                break;

            case "zvukLeteciPada":
                izvorSoundEffecta.PlayOneShot(zvukLeteciPada);
                break;

            case "zvukSlidanjaPoZidu":
                izvorSoundEffecta.PlayOneShot(zvukSlidanjaPoZidu);
                break;

            case "zvukHobotniceKojaPuca":
                izvorSoundEffecta.PlayOneShot(zvukHobotniceKojaPuca);
                break;

            case "zvukNovogSrca":
                izvorSoundEffecta.PlayOneShot(zvukNovogSrca);
                break;

            case "zvukLevelUp":
                izvorSoundEffecta.PlayOneShot(zvukLevelUp);
                break;

            case "zvukNoveStvari":
                izvorSoundEffecta.PlayOneShot(zvukNoveStvari);
                break;

            case "zvukHobotnice":
                izvorSoundEffecta.PlayOneShot(zvukHobotnice);
                break;

            case "zvukGrmljavine":
                izvorSoundEffecta.PlayOneShot(zvukGrmljavine);
                break;

            case "zvukPripremeZaGrmljavinu":
                izvorSoundEffecta.PlayOneShot(zvukPripremeZaGrmljavinu);
                break;

            case "zvukUdarenogStita":
                izvorSoundEffecta.PlayOneShot(zvukUdarenogStita);
                break;

            case "zvukOzljedeniGlavni":
                izvorSoundEffecta.PlayOneShot(zvukOzljedeniGlavni);
                break;

            case "zvukUdarenogKostura":
                izvorSoundEffecta.PlayOneShot(zvukUdarenogKostura);
                break;

           case "zvukMagijeMaca":
                izvorSoundEffecta.PlayOneShot(zvukMagijeMaca);
                break;

            case "zvukDrakule":
                izvorSoundEffecta.PlayOneShot(zvukDrakule);
                break;

            case "zvukSpremanZaMagiju":
                izvorSoundEffecta.PlayOneShot(zvukSpremanZaMagiju);
                break;

            case "zvukStvariSkakanje":
                izvorSoundEffecta.PlayOneShot(zvukStvariSkakanje);
                break;

            case "zvukVodeUlazak":
                izvorSoundEffecta.PlayOneShot(zvukVodeUlazak);
                break;

            case "zvukVodeIzlazak":
                izvorSoundEffecta.PlayOneShot(zvukVodeIzlazak);
                break;

            case "zvukPopuniSvaSrca":
                izvorSoundEffecta.PlayOneShot(zvukPopuniSvaSrca);
                break;

            case "zvukGameOverMaca":
                izvorSoundEffecta.PlayOneShot(zvukGameOverMaca);
                break;

            case "zvukPapira":
                izvorSoundEffecta.PlayOneShot(zvukPapira);
                break;

            case "zvukMetroida":
                izvorSoundEffecta.PlayOneShot(zvukMetroida);
                break;
        }
    }

}
