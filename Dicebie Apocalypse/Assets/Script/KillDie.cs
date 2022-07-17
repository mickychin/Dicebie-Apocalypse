using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillDie : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col.name);
        if (col.CompareTag("Dice"))
        {
            Destroy(col.gameObject);
        }
    }
}
