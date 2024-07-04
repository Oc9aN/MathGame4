using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarongE : MonoBehaviour {

    public GameObject buliding;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Door")
        {
            buliding = col.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        buliding = null;
    }

    public void Enter()
    {
        if (buliding != null)
            transform.GetComponentInParent<WayGame>().FinshButton(buliding.transform.GetComponentInParent<SpriteRenderer>().sprite.name);
    }
}
