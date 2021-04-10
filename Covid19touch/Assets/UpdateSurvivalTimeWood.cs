using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using Microsoft;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit;
using System;


public class UpdateSurvivalTimeWood : MonoBehaviour
{
    GameObject textForWood;
    public GameObject description;
    public UnityEvent OnButtonClicked;
    public GameObject tempButton;
    public GameObject sliderForTemp;
    public GameObject sliderForHumid;
    public TextMeshPro textMeshPro;
    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<TextMesh>().text = "Adnan";

        textForWood = GameObject.Find("SurvivalInfoWood");
        description = textForWood.gameObject.transform.GetChild(1).gameObject;
        tempButton = textForWood.gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject;

        var tempButtonToChange = tempButton.GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        textMeshPro = description.GetComponent<TextMeshPro>();
        DateTime currentTime = DateTime.Now;
        textMeshPro.SetText(currentTime.ToString());

    }
}
