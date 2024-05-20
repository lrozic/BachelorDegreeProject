using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dometGlavnog : MonoBehaviour
{
    public GameObject vitez;
    private GameObject roditelj;

    void Start()
    {
        vitez = GameObject.Find("Igrac");
        roditelj = this.transform.parent.gameObject;
    }


    void Update()
    {
        if (vitez.activeInHierarchy)
        {
            if (this.GetComponent<BoxCollider2D>().IsTouching(vitez.gameObject.GetComponent<BoxCollider2D>()))
            {

                if (vitez.gameObject.name == "Igrac" && this.gameObject.transform.parent != null)
                {
                    transform.parent.gameObject.GetComponent<glavniIgre>().pozicijaIgracaDesno = true;
                    transform.parent.gameObject.GetComponent<glavniIgre>().pozicijaIgracaDesnoSredina = false;  //ak je jedno i drugo od sredine istina, glavni bude stal
                }
            }
            else if (this.gameObject.transform.parent != null)
            {
                transform.parent.gameObject.GetComponent<glavniIgre>().pozicijaIgracaDesnoSredina = true;
            }
        }

    }

    public void makniRoditelja()
    {
        this.gameObject.transform.parent = null;
        Invoke("dodajRoditelja", 0.01f); //ili to ili 0.01
    }

    public void dodajRoditelja()
    {
        this.transform.SetParent(roditelj.transform, true);
    }
}
