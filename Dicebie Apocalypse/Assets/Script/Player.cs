using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;

    Rigidbody2D rb;
    public float moveSpeed;
    float Horizontal;
    float Vertical;
    Vector2 Movement;

    private Vector3 target;
    public GameObject dicePrefab;
    public float diceSpeed;
    public float fireRate;
    public bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get the Wasd or Arrow Key
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");

        //Set the movement to the key that press multiply by the fps on your computer and movespeed
        Movement = new Vector2(Horizontal * moveSpeed, Vertical * moveSpeed);
        rb.velocity = Movement;
        if(Horizontal != 0|| Vertical != 0)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }

        if(Horizontal > 0)
        {
            transform.localScale = new Vector2(-1.6f, 1.6f);
        }
        else
        {
            transform.localScale = new Vector2(1.6f, 1.6f);
        }


        //Target of the dice are at the mouse pos
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && canShoot == true)
        {
            //Shoot the dice
            Vector3 difference = target - transform.position;
            float rotationZ = Mathf.Atan2(difference.y, difference.x);
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();
            StartCoroutine(fireDice(direction, rotationZ));

            canShoot = false;
        }

    }

    IEnumerator fireDice(Vector2 direction, float rotationZZ)
    {
        GameObject d = Instantiate(dicePrefab) as GameObject;
        d.transform.position = transform.position;
        d.transform.rotation = Quaternion.Euler(0f, 0f, rotationZZ);
        d.GetComponent<Rigidbody2D>().velocity = direction * diceSpeed;

        yield return new WaitForSeconds(fireRate);

        canShoot = true;
    }
}
