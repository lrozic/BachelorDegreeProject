using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animacijaStvariDvostrukogSkoka : MonoBehaviour
{
    float pocetakVremena;
    float brzinaGibanjaStvari;

    private Rigidbody2D rigidBodyStvari;

    private bool premaGoreSeGibaj;
    private bool pokrenutaAnimacija;

    private void Start()
    {
        pocetakVremena = 0f;
        premaGoreSeGibaj = true;
        rigidBodyStvari = GetComponent<Rigidbody2D>();
        brzinaGibanjaStvari = 0;
    }

    void Update()
    {
        if (premaGoreSeGibaj && !pokrenutaAnimacija)
        {
            StartCoroutine("premaGore");
        }

        if (!premaGoreSeGibaj && !pokrenutaAnimacija)
        {
            StartCoroutine("premaDolje");
        }

    }

    private IEnumerator premaGore()
    {
        brzinaGibanjaStvari = 0f;
        pokrenutaAnimacija = true;
        pocetakVremena = 0f;
        for (int i = 0; i < 100; i++)
        {
            pocetakVremena += Time.deltaTime;
            rigidBodyStvari.velocity = new Vector2(0f, brzinaGibanjaStvari);
            brzinaGibanjaStvari += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }

        for (int i = 0; i < 50; i++)
        {
            pocetakVremena += Time.deltaTime;
            rigidBodyStvari.velocity = new Vector2(0f, brzinaGibanjaStvari);
            brzinaGibanjaStvari -= 0.02f;
            yield return new WaitForSeconds(0.02f);
        }
        premaGoreSeGibaj = false;
        pokrenutaAnimacija = false;
    }

    private IEnumerator premaDolje()
    {
        brzinaGibanjaStvari = 0f;
        pocetakVremena = 0f;
        pokrenutaAnimacija = true;
        for (int i = 0; i < 100; i++)
        {
            pocetakVremena += Time.deltaTime;
            rigidBodyStvari.velocity = new Vector2(0f, -brzinaGibanjaStvari);
            brzinaGibanjaStvari += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }

        for (int i = 0; i < 50; i++)
        {
            rigidBodyStvari.velocity = new Vector2(0f, -brzinaGibanjaStvari);
            brzinaGibanjaStvari -= 0.02f;
            yield return new WaitForSeconds(0.02f);
        }
        premaGoreSeGibaj = true;
        pokrenutaAnimacija = false;
    }

}
