using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; //za tekst 

public class skriptaZaPrijelaz : MonoBehaviour
{
    public GameObject tekstIzIgre;
    public Text tekstLoading;
    public GameObject glazba;
    public string nazivSceneSamogGameplaya;
    public Animator tranzicijaAnimacije;
    public bool moraIzaciIzIgre;
    public bool uIgri;
    private bool samoJednomIzadi;
    public GameObject zvukPokreniIgru;

    private void Start()
    {
        Cursor.visible = false;
        if (uIgri)
        {
            tekstLoading = tekstIzIgre.GetComponent<Text>();
        }
        moraIzaciIzIgre = false;
        AudioListener.volume = 1f;
        samoJednomIzadi = false;
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) && !uIgri)
        {
            tranzicijaAnimacije.SetTrigger("zadnjaAnimacija");
            zvukPokreniIgru.gameObject.GetComponent<AudioSource>().enabled = true;

            StartCoroutine(pokreniNovuScenu());
        }

        if (moraIzaciIzIgre && uIgri && !samoJednomIzadi)     //to je za izlaz dok si u igri
        {
            tekstIzIgre.SetActive(true);
            samoJednomIzadi = true;
            tranzicijaAnimacije.SetTrigger("zadnjaAnimacija");
            AudioListener.volume = 0f;
            StartCoroutine(pokreniNovuScenu());
            
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !uIgri)
        {         
            Application.Quit();
        }
    }

    private IEnumerator pokreniNovuScenu()
    {
        if (!uIgri)
        {
            yield return new WaitForSeconds(2);
            tekstLoading = tekstIzIgre.GetComponent<Text>();
        }
        else
        {
            yield return new WaitForSeconds(1);
        }
        tekstIzIgre.SetActive(true);
        tekstLoading = tekstIzIgre.GetComponent<Text>();
        tekstLoading.enabled = true;
        yield return new WaitForSeconds(1);
        tekstLoading.text = "Loading .";
        yield return new WaitForSeconds(1);
        tekstLoading.text = "Loading ..";
        yield return new WaitForSeconds(1);
        tekstLoading.text = "Loading ...";
        float glasnoca = 0.9f;
        if (uIgri == false)
        {
            glazba.GetComponent<AudioSource>().volume = 0.95f;
        }
        yield return new WaitForSeconds(1);
        tekstLoading.text = "Loading .";

            for (int i = 0; i < 20; i++)
            {
                if (!uIgri)
                {
                    glazba.GetComponent<AudioSource>().volume = glasnoca;
                    glasnoca -= 0.05f;
                }
                yield return new WaitForSeconds(0.2f);
                if (i == 4)
                {
                    tekstLoading.text = "Loading ..";
                }
                if (i == 9)
                {
                    tekstLoading.text = "Loading ...";
                }
                if (i == 14)
                {
                    tekstLoading.text = "Loading .";
                }
            }
        

        tekstLoading.enabled = false;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(nazivSceneSamogGameplaya);
    }


}
