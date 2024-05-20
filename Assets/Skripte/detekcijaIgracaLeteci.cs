using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detekcijaIgracaLeteci : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Igrac")
        {
            Debug.Log("Igrac je u dometu neprijatelja");
            GameObject roditelj = this.transform.parent.gameObject;
            roditelj.GetComponent<neprijateljLetiAI>().translacija();
        }
    }
}
