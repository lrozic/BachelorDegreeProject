using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DometGlavnogLijevo : MonoBehaviour
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
                    transform.parent.gameObject.GetComponent<glavniIgre>().pozicijaIgracaDesno = false;
                    transform.parent.gameObject.GetComponent<glavniIgre>().pozicijaIgracaLijevoSredina = false;
                }
            }
            else if (this.gameObject.transform.parent != null)
            {
                transform.parent.gameObject.GetComponent<glavniIgre>().pozicijaIgracaLijevoSredina = true;
            }
        }

    }

    public void makniRoditelja()
    {
        this.gameObject.transform.parent = null;
        Invoke("dodajRoditelja", 0.01f);    //ovde si menjal i kod drugog isto
    }

    public void dodajRoditelja()
    {
        this.transform.SetParent(roditelj.transform, true);
    }
}
