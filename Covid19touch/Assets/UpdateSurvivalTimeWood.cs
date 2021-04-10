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
    public GameObject fastForwardButton;
    public GameObject currentTimeButton;
    public GameObject sliderForTemp;
    public GameObject sliderForHumid;
    public TextMeshPro textMeshPro;
    private int count = 0;
    public bool updateEveryFrame = true;
    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<TextMesh>().text = "Adnan";


        textForWood = GameObject.Find("SurvivalInfoWood");
        description = textForWood.gameObject.transform.GetChild(1).gameObject;

        //fast forward button
        fastForwardButton = textForWood.gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject;
        var fastForwardChange = fastForwardButton.GetComponent<Interactable>();
        fastForwardChange.OnClick.AddListener(() => updateContent());

        //set to current time button
        currentTimeButton = textForWood.gameObject.transform.GetChild(2).gameObject.transform.GetChild(2).gameObject;
        var currentTimeChange = fastForwardButton.GetComponent<Interactable>();
        currentTimeChange.OnClick.AddListener(() => resetToCurrentTime());
    }

    // Update is called once per frame
    void Update()
    {
        if (updateEveryFrame)
        {
            textMeshPro = description.GetComponent<TextMeshPro>();
            DateTime currentTime = DateTime.Now;
            textMeshPro.SetText(currentTime.ToString());
        }
    }

    void updateContent()
    {
        updateEveryFrame = false;
        TextMeshPro localText = description.GetComponent<TextMeshPro>();
        count++;

        textMeshPro.SetText("clicked" + count.ToString());
    }

    void resetToCurrentTime()
    {
        updateEveryFrame = true;
    }
}
