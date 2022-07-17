using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public float Wood = 0f;
    public Text WoodText;
    public float Iron = 0f;
    public Text IronText;
    public float Plastic = 0f;
    public Text PlasticText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WoodText.text = Wood.ToString();
        IronText.text = Iron.ToString();
        PlasticText.text = Plastic.ToString();
    }
}
