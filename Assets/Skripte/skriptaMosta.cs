using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skriptaMosta : MonoBehaviour
{

    Rigidbody2D rigidBodyMosta;

    public GameObject efektKapljicaVode;
    public GameObject prasineMostaEfekti;

    void Start()
    {
        rigidBodyMosta = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "protivnikLeti")
        {
            zvukovi.pokreniZvuk("zvukVodeUlazak");
            prasineMostaEfekti.SetActive(true);
            this.transform.gameObject.GetComponent<AudioSource>().enabled = true;
            this.transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            rigidBodyMosta.velocity = new Vector2(0f, -30f);
            Invoke("padMostaUVodu", 0.3f);
            Destroy(this.gameObject, 2.5f);
            Destroy(collision.gameObject, 2.5f);
        }
    }

    private void padMostaUVodu()
    {
        efektKapljicaVode.SetActive(transform);
    }
}
