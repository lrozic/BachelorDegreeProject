using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grmljavinaGlavnog : MonoBehaviour
{
    private Rigidbody2D rigidbodyGrmljavine;
    public GameObject vitez;
    public GameObject glavni;
    public float brzinaGrmljavine;
    SpriteRenderer flipSpriteGrmljavine;

    void Start()
    {
        flipSpriteGrmljavine = GetComponent<SpriteRenderer>();
        rigidbodyGrmljavine = GetComponent<Rigidbody2D>();
        if (glavni.transform.gameObject.GetComponent<glavniIgre>().smjerGrmljavine == -1)
        {
            rigidbodyGrmljavine.velocity = new Vector2(-brzinaGrmljavine, 0f);
            flipSpriteGrmljavine.flipX = false;
        }
        else
        {
            rigidbodyGrmljavine.velocity = new Vector2(brzinaGrmljavine, 0f);
        }
    }

    void Update()
    {
        if (vitez.activeInHierarchy)
        {
            if (this.GetComponent<BoxCollider2D>().IsTouching(vitez.gameObject.GetComponent<BoxCollider2D>()))
            {
                vitez.gameObject.GetComponent<skriptaViteza>().smanjiZivotIgraca(1);
            }
        }

    }
}
