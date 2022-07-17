using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameMaster : MonoBehaviour
{
    public Zombie zombie;

    public float currentTree;
    public float currentCar;
    public GameObject woodPrefabs;
    public GameObject carPrefabs;

    public float CostToMakeIron;
    public Image[] HPIM;
    public float PlayerHP;

    public float Wood = 0f;
    public TextMeshProUGUI WoodText;
    public float Scrap = 0f;
    public TextMeshProUGUI ScrapText;
    public float Iron = 0f;
    public TextMeshProUGUI IronText;
    public float Plastic = 0f;
    public TextMeshProUGUI PlasticText;
    public GameObject CostOfIronIm;
    public Sprite[] numberDice;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SummonZombie", Random.Range(5, 10));
        Invoke("SummonZombie", Random.Range(5, 10));
        Invoke("GenerateStuff", Random.Range(5, 10));
        Invoke("GenerateStuff", Random.Range(5, 10));
        CostToMakeIron = Random.Range(1, 7);
    }
    void GenerateStuff()
    {
        if (currentTree < 80)
        {
            currentTree++;
            GameObject wood = Instantiate(woodPrefabs);
            wood.transform.position = new Vector2(Random.Range(-100f, 100f), Random.Range(-100f, 100f));
            wood.GetComponent<Tree>().SummonTree = true;
        }
        if (currentCar < 80)
        {
            //stone is car
            currentCar++;
            GameObject stone = Instantiate(carPrefabs);
            stone.transform.position = new Vector2(Random.Range(-100f, 100f), Random.Range(-100f, 100f));
            stone.GetComponent<GiveIron>().SummonTree = true;
        }

        Invoke("GenerateStuff", Random.Range(5, 10));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SummonZombie()
    {
        Zombie zombo = Instantiate(zombie);
        if (Random.Range(1, 3) == 1)
        {
            zombo.transform.position = new Vector2(FindObjectOfType<Player>().transform.position.x + Random.Range(10, 15), transform.position.y);
        }
        else
        {
            zombo.transform.position = new Vector2(FindObjectOfType<Player>().transform.position.x + Random.Range(-10, -15), transform.position.y);
        }
        if (Random.Range(1, 3) == 1)
        {
            zombo.transform.position = new Vector2(zombo.transform.position.x, FindObjectOfType<Player>().transform.position.y + Random.Range(10, 15));
        }
        else
        {
            zombo.transform.position = new Vector2(zombo.transform.position.x, FindObjectOfType<Player>().transform.position.y + Random.Range(-10, -15));
        }
        Invoke("SummonZombie", Random.Range(5, 10));
    }

    public void PlayerLoseHp(float HpLoses)
    {
        PlayerHP -= HpLoses;

        for (int i = 3; i > PlayerHP; i--)
        {
            //Debug.Log(i - 1);
            HPIM[i - 1].gameObject.SetActive(false);
        }
        if (PlayerHP <= 0)
        {
            Time.timeScale = 0f;
        }
        ScrapText.text = Scrap.ToString();
        WoodText.text = Wood.ToString();
        IronText.text = Iron.ToString();
        PlasticText.text = Plastic.ToString();
        //Debug.Log(CostToMakeIron - 1);
        CostOfIronIm.GetComponent<Image>().sprite = numberDice[(int)CostToMakeIron - 1];
    }
}
