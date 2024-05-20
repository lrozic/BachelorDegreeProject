using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class neprijateljproba : MonoBehaviour
{
    public float duljinaLinije;
    public float brzina;
    int naCemuSeHoda;

    private bool kreceSeDesno = true;

    public Transform tloRaka;



    void Start()
    {
    }


    void Update()
    {
        int naCemuSeHoda = 8;
        transform.Translate(Vector2.left * brzina * Time.deltaTime); //to je dakle de se mora kretat, kojom brzinom i

        RaycastHit2D groundCheck = Physics2D.Raycast(tloRaka.position, Vector2.down, duljinaLinije, naCemuSeHoda);   // Raycast je kao recimo nevidljiva linija koja se stavlja ispred neprijatelja (dakle izvor se nalazi
                                                                                                                     // malo ispred neprijatelja, samo pogledaj romb tloNeprijatelja u Unity-u), ta linija od tog romba ide
                                                                                                                     // prema dolje, dakle da detektira tlo i duljina te linije neka je broj 2 tipa float, dakle udaljenost = 2f npr.
                                                                                                                     // varijablu udaljenost smo stavili tako da ne trebamo hard-kodirati to ovdje, nego preko unity-a samo
        if (groundCheck.collider == false)
        {
            if (kreceSeDesno == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);    // to je da se okrene lik i hoda na levu stranu
                transform.localRotation = Quaternion.Euler(0, 0, 0);    // ovo se koristi za sam sprite da se obrne
                kreceSeDesno = false;
            }

            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                kreceSeDesno = true;
            }
        }
        
        

    }

   /* {
            RaycastHit2D wallInfoleft = Physics2D.Raycast(wallDetection.position, Vector2.left, distance2);
    RaycastHit2D wallInforight = Physics2D.Raycast(wallDetection.position, Vector2.left, distance2);
            if (wallInfoleft.collider || wallInforight.collider == true)
                movingRight = !movingRight;
        }*/



}
