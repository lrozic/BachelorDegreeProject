using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zvukHodanja: MonoBehaviour
{

    public static AudioClip zvukTrcanja, zvukSlidanjaPoZidu, zvukLisca;
    public GameObject vitez;
    static AudioSource izvorSoundEffecta2;

    void Start()
    {
        zvukTrcanja = Resources.Load<AudioClip>("trcanje");
        zvukSlidanjaPoZidu = Resources.Load<AudioClip>("wallJump");
        zvukLisca = Resources.Load<AudioClip>("zvukLisca");

        vitez = GameObject.Find("Igrac");
        izvorSoundEffecta2 = GetComponent<AudioSource>();      
    }

    void Update()
    {
    }

    public static void pokreniZvuk2(string naziv, bool pritisnut)
    {
        switch (naziv)
        {
            case "zvukTrcanja":
                izvorSoundEffecta2.loop = true;
                if (pritisnut == true)
                {
                    if (!izvorSoundEffecta2.isPlaying)
                    {          

                        izvorSoundEffecta2.PlayOneShot(zvukTrcanja);
                    }
                }
                else
                {
                    izvorSoundEffecta2.Stop();
                }
                break;

            case "zvukWallJumpanja":
                izvorSoundEffecta2.loop = false;
                if (!izvorSoundEffecta2.isPlaying)
                {
                    izvorSoundEffecta2.PlayOneShot(zvukSlidanjaPoZidu);
                }
                break;
        }
    }
}
