using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magijaUdaraNeprijatelje : MonoBehaviour
{
    string tipNeprijatelja;

    private void Start()
    {
        tipNeprijatelja = transform.gameObject.tag;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.gameObject.tag == "magijaMaca")
        {
            switch (tipNeprijatelja)
            {
                case "protivnikRakMagija":
                    this.transform.parent.gameObject.GetComponent<neprijateljRakAI>().smanjiZivot(3);
                    Destroy(collision.gameObject);
                    break;
                case "protivnikLeteciMagija":
                    this.transform.parent.gameObject.GetComponent<neprijateljLetiAI>().smanjiZivot(3);
                    Destroy(collision.gameObject);
                    break;
                case "protivnikHobotnicaMagija":
                    this.transform.parent.gameObject.GetComponent<neprijateljHobotnicaAI>().smanjiZivot(3);
                    Destroy(collision.gameObject);
                    break;
                case "protivnikKosturMagija":
                    this.transform.parent.gameObject.GetComponent<neprijateljKosturAI>().smanjiZivot(3);
                    Destroy(collision.gameObject);
                    break;
            }
        }
    }
}
