using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillDie : MonoBehaviour
{
    private void Update()
    {
        transform.position = FindObjectOfType<Player>().transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(col.name);
        if (collision.gameObject.CompareTag("Dice"))
        {
            collision.gameObject.GetComponent<Dice>().BounceOffSMTH();
        }
    }
}
