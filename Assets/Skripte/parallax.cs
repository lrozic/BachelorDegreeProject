using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class parallax : MonoBehaviour
{

    private float duljina;
    private float startnaPozicija;

    public GameObject kamera;

    public float parallaxEffect;

    void Start()
    {
        startnaPozicija = transform.position.x;
        duljina = GetComponent<SpriteRenderer>().size.x; //mozda bez bounds
    }

    // Update is called once per frame
    void Update()  
    {
        float udaljenost = kamera.transform.position.x * parallaxEffect;  //to je koliko daleko se krećemo u worldspace

        transform.position = new UnityEngine.Vector3(startnaPozicija + udaljenost, transform.position.y, -20);  //moral unity engine koristit jer ga ne kuži zbog... nezz
    }
}
