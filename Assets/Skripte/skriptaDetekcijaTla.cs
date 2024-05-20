using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skriptaDetekcijaTla : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (!(this.transform.gameObject.name == "ColliderDetektiranjeNeprijatelja") && collision.transform.tag == "tlo")
        {
            this.transform.parent.localScale = new Vector2(-this.transform.parent.localScale.x, this.transform.parent.localScale.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.transform.gameObject.tag == "protivnik" || collision.transform.gameObject.tag == "protivnikHobotnica") && this.transform.gameObject.name == "ColliderDetektiranjeNeprijatelja")
        {
            this.transform.parent.localScale = new Vector2(-this.transform.parent.localScale.x, this.transform.parent.localScale.y);
        }
    }

}

