using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public float PaperDice = 3;
    public float IronDiceHave = 0;
    public float ScrapDiceHave = 0;
    public float WoodDiceHave = 0;
    public string currentDice;
    public TextMeshProUGUI PaperDiceCount;
    public TextMeshProUGUI IronDiceCount;
    public TextMeshProUGUI ScrapDiceCount;
    public TextMeshProUGUI WoodDiceCount;
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

    public Button[] CraftDieButt;
    public Button MakeIronButt;

    public GameObject Car;
    public bool isCar;

    public bool isWorkbench;

    public AudioSource source;
    public AudioClip walk;

    // Start is called before the first frame update
    void Start()
    {
        //currentDice = "paper";
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

        if (Input.GetMouseButtonDown(0))
        {
            if(isWorkbench == false)
            {
                if(IronDiceHave > 0)
                {
                    currentDice = "iron";
                    IronDiceHave--;
                }
                else if(ScrapDiceHave > 0)
                {
                    currentDice = "scrap";
                    ScrapDiceHave--;
                }
                else if (WoodDiceHave > 0)
                {
                    currentDice = "wood";
                    WoodDiceHave--;
                }
                else if (PaperDice > 0)
                {
                    currentDice = "paper";
                    PaperDice--;
                }
                else
                {
                    Debug.Log("You have no dice left go pick it up u **************");
                    return;
                }
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

        if(isCar)
        {
            Car.SetActive(true);
            moveSpeed = 20;
        }
        else
        {
            Car.SetActive(false);
            if(Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed = 10;
                GetComponent<Animator>().speed = 2.5f;
            }
            else
            {
                moveSpeed = 5; 
                GetComponent<Animator>().speed = 1.0f;
                source.PlayOneShot(walk);
            }
            
        }
        


    }

    void FixedUpdate()
    {
        PaperDiceCount.text = PaperDice.ToString();
        WoodDiceCount.text = WoodDiceHave.ToString();
        ScrapDiceCount.text = ScrapDiceHave.ToString();
        IronDiceCount.text = IronDiceHave.ToString();
    }


    IEnumerator fireDice(Vector2 direction, float rotationZZ)
    {
        GameObject d = Instantiate(dicePrefab) as GameObject;
        d.transform.position = transform.position;
        d.transform.rotation = Quaternion.Euler(0f, 0f, rotationZZ);
        d.GetComponent<Rigidbody2D>().velocity = direction * diceSpeed;
        d.GetComponent<Dice>().currentDice = currentDice;

        yield return new WaitForSeconds(fireRate);

        //canShoot = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Workbench"))
        {
            isWorkbench = true;
            //Debug.Log("work?");
            CraftDieButt[0].gameObject.SetActive(true);
            CraftDieButt[1].gameObject.SetActive(true);
            CraftDieButt[2].gameObject.SetActive(true);
        }
        if (col.CompareTag("Furnance"))
        {
            isWorkbench = true;
            //Debug.Log("work?");
            MakeIronButt.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Workbench"))
        {
            isWorkbench = false;
            CraftDieButt[0].gameObject.SetActive(false);
            CraftDieButt[1].gameObject.SetActive(false);
            CraftDieButt[2].gameObject.SetActive(false);
        }
        if (col.CompareTag("Furnance"))
        {
            isWorkbench = false;
            //Debug.Log("work?");
            MakeIronButt.gameObject.SetActive(false);
        }
    }

    public void MakeIron()
    {
        GameMaster gm = FindObjectOfType<GameMaster>();
        if(gm.CostToMakeIron <= gm.Scrap)
        {
            gm.Scrap -= gm.CostToMakeIron;
            gm.Iron += 1;
            gm.CostToMakeIron = Random.Range(1, 7);
        }
    }

    public void ChangeDiceTo(string diceto)
    {
        GameMaster gm = FindObjectOfType<GameMaster>();
        if (diceto == "wood" && gm.Wood >= 10)
        {
            gm.Wood -= 10;
            WoodDiceHave++;
        }
        //if(diceto == "plastic" && gm.Plastic >= 50)
        //{
        //    currentDice = diceto;
        //}
        if (diceto == "iron" && gm.Iron >= 20)
        {
            gm.Iron -= 20;
            IronDiceHave++;
        }
        if (diceto == "scrap" && gm.Scrap >= 15)
        {
            gm.Scrap -= 15;
            ScrapDiceHave++;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Dice"))
        {
            Debug.Log("work!!");
            Dice die = collision.gameObject.GetComponentInParent<Dice>();
            if (die.currentDice == "scrap")
            {
                ScrapDiceHave++;
            }
            if (die.currentDice == "iron")
            {
                IronDiceHave++;
            }
            if (die.currentDice == "wood")
            {
                WoodDiceHave++;
            }
            if (die.currentDice == "paper")
            {
                PaperDice++;
            }
            Destroy(die.gameObject);
        }
    }
}
