using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveIron : MonoBehaviour
{
    public Zombie zombie;
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
                if (die.DieOutput > 1)
                {
                    //get amount of scrap base on the dice
                    FindObjectOfType<GameMaster>().Scrap += die.DieOutput * die.DiceDamage;
                }
                else
                {
                    //well too bad you got 1 so summon zombie
                    Zombie spawn = Instantiate(zombie, transform.position, Quaternion.identity);
                }
                Destroy(gameObject);
            }
        }
    }
}
