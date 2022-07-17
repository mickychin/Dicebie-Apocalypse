using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivePlasitc : MonoBehaviour
{
    public float hp = 6;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //
        if (collision.gameObject.CompareTag("Dice"))
        {
            Dice die = collision.gameObject.GetComponent<Dice>();
            hp -= die.DieOutput;
            Debug.Log(hp);
            Destroy(collision.gameObject);
            if (hp <= 0)
            {
                if (die.DieOutput > 1)
                {
                    //get amount of Plastic base on the dice
                    FindObjectOfType<GameMaster>().Plastic += die.DieOutput;
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
