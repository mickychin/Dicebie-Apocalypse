using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public float DiceDamage;
    public string currentDice;
    public Sprite[] PaperDice;
    public Sprite[] IronDice;
    public Sprite[] WoodenDice;
    public Sprite[] PlasticDice;
    public Sprite[] scrapDice;
    SpriteRenderer render;
    Rigidbody2D rb;
    public float DieOutput;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        DieOutput = Random.Range(1, 7);
        if(currentDice == "paper")
        {
            render.sprite = PaperDice[(int)DieOutput - 1];
        }
        if (currentDice == "iron")
        {
            render.sprite = IronDice[(int)DieOutput - 1];
        }
        if (currentDice == "wood")
        {
            render.sprite = WoodenDice[(int)DieOutput - 1];
        }
        if (currentDice == "plastic")
        {
            render.sprite = PlasticDice[(int)DieOutput - 1];
        }
        if (currentDice == "scrap")
        {
            render.sprite = scrapDice[(int)DieOutput - 1];
        }
    }

    public void BounceOffSMTH()
    {
        Debug.Log("work");
        while(rb.velocity.x != 0|| rb.velocity.y != 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * 0.3f, rb.velocity.y * 0.3f);
        }
    }
}
