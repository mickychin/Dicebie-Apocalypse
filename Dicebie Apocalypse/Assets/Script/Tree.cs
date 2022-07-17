using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tree : MonoBehaviour
{
    public float hp = 6;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //
        if (collision.gameObject.CompareTag("Dice"))
        {
            Dice die = collision.gameObject.GetComponent<Dice>();
            hp -= die.DieOutput * die.DiceDamage;
            Debug.Log(hp);
            collision.gameObject.GetComponent<Dice>().BounceOffSMTH();
            if (hp <= 0)
            {
                if (die.DieOutput > 2)
                {
                    //get amount of wood base on the dice
                    FindObjectOfType<GameMaster>().Wood += die.DieOutput * die.DiceDamage;
                }
                else
                {
                    //well too bad you got 1 so summon zombie
                }
                Destroy(gameObject);
            }
        }
    }
}
