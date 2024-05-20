using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skriptaZarulje : MonoBehaviour
{
    private Animator animacija;
    public GameObject metalZaStvoriti;
    void Start()
    {
        animacija = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "magijaMaca")
        {
            animacija.SetBool("ukljucen", true);
            this.GetComponent<AudioSource>().enabled = true;
            metalZaStvoriti.GetComponent<Rigidbody2D>().velocity = new Vector2(-1f, 0f);
            Invoke("Zaustavi", 8f);
            Destroy(this.GetComponent<BoxCollider2D>());
        }
    }

    private void Zaustavi()
    {
        metalZaStvoriti.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
    }
}
