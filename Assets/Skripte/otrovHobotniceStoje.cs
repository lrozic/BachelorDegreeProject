using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class otrovHobotniceStoje : MonoBehaviour
{
    private Rigidbody2D rigidbodyProjektila;
    public GameObject vitez;
    public float BrzinaProjektila;
    public GameObject tlo;

    public bool stranaVitezaDesno;

    void Start()
    {
        vitez = GameObject.Find("Igrac");
        rigidbodyProjektila = GetComponent<Rigidbody2D>();
        if (this.transform.tag == "efektZlatniKostur")
        {
            this.transform.Rotate(0f, 0f, 90f);
            rigidbodyProjektila.velocity = new Vector2(0f, -50f);
        }
        else
        {
            if (vitez.transform.position.x < this.transform.position.x)
            {
                rigidbodyProjektila.velocity = new Vector2(-BrzinaProjektila, 0f);
            }
            else
            {
                rigidbodyProjektila.velocity = new Vector2(BrzinaProjektila, 0f);
            }
        }
    }

    void Update()
    {
        if (this.GetComponent<BoxCollider2D>().IsTouching(vitez.gameObject.GetComponent<BoxCollider2D>()))
        {
            if (this.transform.tag == "laser")
            {
                vitez.gameObject.GetComponent<skriptaViteza>().smanjiZivotIgraca(3);
            }
            else
            {
                vitez.gameObject.GetComponent<skriptaViteza>().smanjiZivotIgraca(2);
            }
        }
    }
}
