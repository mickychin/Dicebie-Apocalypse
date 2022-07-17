using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveIron : MonoBehaviour
{
    public Zombie zombie;
    public float hp = 6;
    public bool SummonTree = false;
    public GameObject Particle;
    void start()
    {

        if (SummonTree)
        {
            Invoke("NotSUmmonTree", 1f);
        }
    }

    void NotSUmmonTree()
    {
        SummonTree = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("NoTC") && SummonTree)
        {
            SummonTree = false;
            transform.position = new Vector2(Random.Range(-100f, 100f), Random.Range(-100f, 100f));
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //
        if (collision.gameObject.CompareTag("Dice"))
        {
            Dice die = collision.gameObject.GetComponent<Dice>();
            hp -= die.DieOutput * die.DiceDamage;
            Instantiate(Particle,transform.position, Quaternion.identity);
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
                FindObjectOfType<GameMaster>().currentCar--;
                Destroy(gameObject);
            }
        }
    }
}
