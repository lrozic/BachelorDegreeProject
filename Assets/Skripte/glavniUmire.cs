using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glavniUmire : MonoBehaviour
{
    public GameObject vitez;
    public GameObject glavnaGlazba;
    public GameObject vatraGameObject;

    public GameObject stvarMagije;
    public float brzinaGibanjaStvari;
    void Start()
    {
        vitez.GetComponent<skriptaViteza>().dodajExpBodove(800);
        StartCoroutine("smanjiGlazbuGlavnog");
        StartCoroutine("vatraGori");
    }

    void Update()
    {
       
    }

    private IEnumerator smanjiGlazbuGlavnog()
    {
        while (glavnaGlazba.transform.gameObject.GetComponent<AudioSource>().volume >= 0.1) {
            glavnaGlazba.transform.GetComponent<AudioSource>().volume -= 0.01f;
            yield return new WaitForSeconds(0.05f);
                }
        Destroy(glavnaGlazba);
    }

    private IEnumerator vatraGori()
    {
        yield return new WaitForSeconds(4);

        var malaVatra = new GameObject[5];
        for (int i = 0; i < 5; i++)
        {
            malaVatra[i] = Instantiate(vatraGameObject, this.transform.position, Quaternion.identity);
            malaVatra[i].transform.gameObject.transform.localPosition = new Vector2(this.transform.localPosition.x - (float)3.2 + i * (float)1.5, 
                this.transform.localPosition.y-(float)2.4);
        }

        malaVatra[1].transform.GetComponent<AudioSource>().enabled = true;
        malaVatra[1].transform.GetComponent<AudioSource>().volume = 0.8f;

        float transparentnost = 0;
        do
        {
            for (int i = 0; i < 5; i++)
            {
                malaVatra[i].transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, transparentnost);
            }

            transparentnost += 0.01f;
            Debug.Log(transparentnost);
            yield return new WaitForSeconds(0.01f);
        } while (transparentnost < (float)0.8);

        this.transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f);
        yield return new WaitForSeconds(3);

        transparentnost = 0.8f;

        do
        {
            for (int i = 0; i < 5; i++)
            {
                malaVatra[i].transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, transparentnost);
            }
            transparentnost -= 0.01f;
            Debug.Log(transparentnost);
            yield return new WaitForSeconds(0.01f);
        } while (transparentnost > 0);

        malaVatra[1].transform.GetComponent<AudioSource>().enabled = false;

        for (int i = 0; i < 5; i++)
        {
            Destroy(malaVatra[i]);
        }
        yield return new WaitForSeconds(3);

        StartCoroutine("premaDolje");     
    }

    private IEnumerator premaDolje()
    {
        float smanjivanjeBrzine = 0f;

        for (int i = 0; i < 100; i++)
        {
            stvarMagije.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -brzinaGibanjaStvari);
            yield return new WaitForSeconds(0.01f);
        }

        for (int i = 0; i < 100; i++)
        {
            stvarMagije.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -brzinaGibanjaStvari + smanjivanjeBrzine);
            smanjivanjeBrzine += 0.01f;
            yield return new WaitForSeconds(0.02f);
        }

        stvarMagije.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);

        yield return new WaitForSeconds(3);

        for (int i = 0; i < 200; i++)
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -brzinaGibanjaStvari+2);
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(3);



        Destroy(this.gameObject);

    }

}
