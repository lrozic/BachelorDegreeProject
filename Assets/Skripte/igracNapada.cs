using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class igracNapada : MonoBehaviour
{
    public float zapocetoVrijemeIzmeduNapada;   //igrac moze napasti svakih x sekundi
    public int snagaNapada;

    private float stopanjeVremena;
    public Transform pozicijaKrugaZaNapad;
    public float dometKrugaZaNapad;
    public LayerMask neprijatelj;
    private bool napadaKasni;

    void Start()
    {
        napadaKasni = false;
    }

    void Update()
    {
        if(stopanjeVremena <= 0)
        {

            if (Input.GetKeyDown(KeyCode.Y) && GetComponent<skriptaViteza>().stoji() == true && GetComponent<skriptaViteza>().mozeNapastiNakonOzljedeVarijabla == true)
            {
                stopanjeVremena = zapocetoVrijemeIzmeduNapada;
                napadaKasni = true;
            }    
            //oni neprijatelji koji su odgovarajućeg layerMaska budu osjetili taj collider
            
        }
        else if(stopanjeVremena <= 0.11 && napadaKasni == true && GetComponent<skriptaViteza>().mozeNapastiNakonOzljedeVarijabla == true)   //0.13
        {
            napadaKasni = false;
            Collider2D[] krugZaNapad = Physics2D.OverlapCircleAll(pozicijaKrugaZaNapad.position, dometKrugaZaNapad, neprijatelj);    //neprijatelji koji su unutar tog kruga budu dobili batine, zadnji je layerMask, dakle 
            for (int i = 0; i < krugZaNapad.Length; i++)
            {
                if (krugZaNapad[i].gameObject.tag == "protivnik")   //errore ti javlja jer taj neprijatelj ima i druge game.objekte, za hitboxe, detekciju tla i tak
                {
                    krugZaNapad[i].GetComponent<neprijateljRakAI>().smanjiZivot(snagaNapada);
                }
                else if (krugZaNapad[i].gameObject.tag == "protivnikLeti")
                {
                    krugZaNapad[i].GetComponent<neprijateljLetiAI>().smanjiZivot(snagaNapada);
                }
                else if (krugZaNapad[i].gameObject.tag == "protivnikHobotnica")
                {
                    krugZaNapad[i].GetComponent<neprijateljHobotnicaAI>().smanjiZivot(snagaNapada);
                }
                else if (krugZaNapad[i].gameObject.tag == "protivnikKostur")
                {
                    krugZaNapad[i].GetComponent<neprijateljKosturAI>().smanjiZivot(snagaNapada);
                }
                else if (krugZaNapad[i].gameObject.tag == "protivnikZlatniKostur")
                {
                    krugZaNapad[i].GetComponent<neprijateljKosturAI>().smanjiZivot(snagaNapada);
                }
                else if (krugZaNapad[i].gameObject.tag == "glavniIgre")
                {
                    krugZaNapad[i].GetComponent<glavniIgre>().smanjiZivot(snagaNapada);
                }
            }
            stopanjeVremena -= Time.deltaTime;
        }

        else
        {
            stopanjeVremena -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pozicijaKrugaZaNapad.position, dometKrugaZaNapad);
    }
}
