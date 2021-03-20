using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UpdateInfectivityRateForWood : MonoBehaviour
{
    GameObject textForWood;
    public GameObject description;
    public TextMeshPro textMeshPro;
    private bool tempUpdated = false;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<TextMesh>().text = "Adnan";
        textForWood = GameObject.Find("InformationForWoodText");
        description = textForWood.gameObject.transform.GetChild(1).gameObject;
        textMeshPro = description.GetComponent<TextMeshPro>();
        textMeshPro.SetText("hi");
        //textForWood.GetComponent<TextMesh>.textForWood = "Adnan";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateOnUserChange()
    {

    }

    double formulaForRate()
    {
        return 0.0;
    }
}
