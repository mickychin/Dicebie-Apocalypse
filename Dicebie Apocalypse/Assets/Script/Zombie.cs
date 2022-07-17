using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    Rigidbody2D rb;
    public float ZombieHP;
    Animator animator;
    public float zombieSpeed;
    LayerMask mask;
    LayerMask Default;
    public float MaxDistance;

    public bool IsAttacking;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        IsAttacking = false;
        animator = GetComponent<Animator>();
        mask = LayerMask.GetMask("Player");
        Default = LayerMask.GetMask("Default");
    }

    // Update is called once per frame
    void Update()
    {

        if (ZombieHP <= 0)
        {
            while (rb.velocity.x != 0 || rb.velocity.y != 0)
            {
                rb.velocity = new Vector2(rb.velocity.x * 0.3f, rb.velocity.y * 0.3f);
            }
            return;
        }

        if(FindObjectOfType<Player>().transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-0.5f, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(0.5f, transform.localScale.y, transform.localScale.z);
        }

        if (Physics2D.Raycast(transform.position, FindObjectOfType<Player>().transform.position - transform.position, MaxDistance, mask))
        {
            //Debug.Log("might");
            if (!IsAttacking && !Physics2D.Raycast(transform.position, FindObjectOfType<Player>().transform.position - transform.position, MaxDistance, Default))
            {
                animator.SetBool("Walking", true);
                //Debug.Log("see player");
                //move toward player
                Vector3 difference = FindObjectOfType<Player>().transform.position - transform.position;
                float rotationZ = Mathf.Atan2(difference.y, difference.x);
                float distance = difference.magnitude;
                Vector2 direction = difference / distance;
                direction.Normalize();
                GetComponent<Rigidbody2D>().velocity = direction * zombieSpeed;
                if (Physics2D.Raycast(transform.position, FindObjectOfType<Player>().transform.position - transform.position, 1, mask))
                {
                    //atack
                    animator.SetTrigger("Attaking");
                }
            }
            else
            {
                animator.SetBool("Walking", false);
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
        }
        else
        {
            animator.SetBool("Walking", false);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }   
    }

    public void IsNotAttacking()
    {
        IsAttacking = false;
    }
    public void IsIsAttacking()
    {
        IsAttacking = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Dice"))
        {
            Dice die = collision.gameObject.GetComponent<Dice>();
            ZombieHP -= die.DieOutput * die.DiceDamage;
            collision.gameObject.GetComponent<Dice>().BounceOffSMTH();
            if(ZombieHP <= 0)
            {
                animator.SetTrigger("Dead");
            }
        }
    }
}
